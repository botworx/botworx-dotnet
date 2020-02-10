using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy.Rhs
{
    public class RhsMsgEffectVisit : TranspilerVisit<MessageEffect>
    {
        public override void DoVisit(MessageEffect n)
        {
            //t.Visit(n.Expression);
            //base.DoVisit(n);
            //base.VisitChildren(n, this);
            t.Visit(n.ExprSeq, this);
        }
        public void Visitback(AstNode n, Action cps)
        {
            if (n.MessageTag.TaskDef != null)
            {
                List<Var> vars = new List<Var>();
                t.CurrentScope.CollectVariables(vars);
                n.MessageTag.TaskDef.Parameters = vars;
                //(MessageCallback)
                var code = "(__bwxContext, __bwxExpert, __bwxMessage) => { ";
                code += "Method __bwxTask = new Method(__bwxContext, __bwxMessage); ";
                code += "__bwxContext.ScheduleTask(__bwxTask, ";
                code += string.Format("{0}({1}));", n.MessageTag.TaskDef.ProcedureDef.Name, n.MessageTag.TaskDef.CreateParameterString(t, false));
                code += " }";
                //TODO:There must be a problem here.
                ClauseExpr nClause = (ClauseExpr)n;
                nClause.Object.Token = new Token(TokenKind.Snippet, code);
                nClause.Type = nClause.Predicate.ToPredicate().Spec;
            }

            if (n.MessageTag.Wait && n.MessageTag.Result.NotNil)
                t.WriteLine("_bwxResult = new TaskResult(bwxTask);");

            t.WriteIndented("_bwxMsg = ");

            if (n.IsNil)
            {
                t.Write("Message.NilMessage");
                t.Write(")");
                t.WriteStatementEnd();
                return;
            }
            //else
            t.Write("new Message(MessageKind.{0}, {1}, {2}, ", n.MessageTag.MessageKind, t.Translate(n.MessageTag.Sender), t.Translate(n.MessageTag.Result));
            //t.Visit(n);
            cps();
            //
            t.Write(")");
            t.WriteStatementEnd();

            //
            WriteSend(n);
            //
            if (n.MessageTag.Wait && n.MessageTag.Result.NotNil)
            {
                t.WriteLine("yield return TaskStatus.Suspended;");
                t.WriteLine("if(_bwxResult.Status != TaskStatus.Succeeded)");
                t.Indent();
                t.WriteLine("yield return Fail(bwxTask.Process, bwxTask.Message);");
                t.Dedent();
            }
        }
        public void WriteSend(AstNode n)
        {
            if(n.MessageTag.IsProposal)
                WritePropose(n);
            else
                WritePost(n);
        }
        public void WritePost(AstNode n)
        {
            t.WriteLine("Post(bwxTask.Process, _bwxMsg);");
        }
        public void WritePropose(AstNode n)
        {
            t.WriteLine("Propose(bwxTask.Process, _bwxMsg, null);");
        }
    }
}
