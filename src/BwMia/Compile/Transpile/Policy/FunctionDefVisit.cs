using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    public class TaskStubDefVisit<T, N> : BlockStmtVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : TaskStubDef
    {
        protected override void WriteHeader(N n)
        {
            t.WriteLine("public static void {0}(Process bwxProcess, Expert bwxExpert, Message bwxMsg)", n.Name);
        }
        public override void DoVisit(N n)
        {
            if (n.TaskDef.TriggerDef != null && n.TaskDef.TriggerDef.ExprSeq != null)
                WriteReceive(n.TaskDef.TriggerDef.ExprSeq[0]);
            t.WriteLine("MentalTask bwxTask = new Method(bwxProcess, bwxMsg);");
            t.WriteLine("bwxProcess.ScheduleTask(bwxTask, (({0})bwxExpert).{1}({2}));", n.Parent.Parent.Name, n.Parent.Name + "Proc", n.TaskDef.CallString);
        }
        public void WriteReceive(AstNode n_)
        {
            if (n_.IsVariable)
            {
                t.WriteLine("Atom {0} = bwxMsg.Clause;", t.Translate(n_));
                return;
            }
            //else
            ClauseExpr n = ((ClauseExpr)n_);

            if (n.HasBinding)
                t.WriteLine("Atom {0} = bwxMsg.Clause;", n.Binding.Translate());
            if (n.Subject != null && n.Subject.IsVariable)
            {
                t.CurrentScope.CreateVar(n.Subject.Token);
                t.WriteLine("Atom {0} = bwxMsg.Clause.Subject;", t.Translate(n.Subject));
            }
            if (n.Object != null && n.Object.IsVariable)
            {
                t.CurrentScope.CreateVar(n.Object.Token, n.Object.Type);
                t.WriteLine("{0} {1} = ({0})bwxMsg.Clause.Object;", n.Object.Type, t.Translate(n.Object));
            }

            foreach (ClauseExpr propExpr in n.PropertyExprs)
            {
                AstNode propPred = propExpr.Predicate;
                AstNode propObj = propExpr.Object;
                Token propSpec = propExpr.Type;
                if (propObj.Token.Kind == TokenKind.Variable)
                {
                    t.CurrentScope.CreateVar(propObj.Token);
                    t.WriteLine("{0} {1} = ({0})bwxMsg.Clause[{2}];", propSpec, t.Translate(propObj), t.Translate(propPred));
                }
            }
        }
    }
    public class TaskProcedureDefVisit<T, N> : RhsStmtVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : TaskProcedureDef
    {
        protected override void WriteHeader(N n)
        {
            t.WriteLine("public IEnumerator<TaskStatus> {0}({1})", n.Name, n.TaskDef.ParameterString);
        }
        public override void BeginVisit(N n)
        {
            base.BeginVisit(n);
            n.TaskDef.InjectParameters(t.CurrentScope);
            t.CurrentScope.PushCleanup(() => t.WriteLine("yield return Succeed(bwxTask.Process, bwxTask.Message);"));
            //TODO:Don't generate these unless they are needed.
            //Also, anything marked bwx* is a reusable variable.
            t.WriteLine("Task _bwxTask = null;");
            t.WriteLine("Context _bwxContext = null;");
            t.WriteLine("Message _bwxMsg;");
            t.WriteLine("Clause _bwxClause = null;");
            t.WriteLine("Atom _bwxSubject = null;");
            t.WriteLine("TaskResult _bwxResult = null;");
            t.WriteLine("Begin(bwxTask);");
        }
    }
    public class ExpertStubDefVisit<T, N> : TaskStubDefVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : ExpertStubDef
    {
        public override void DoVisit(N n)
        {
            TriggerStmt triggerDef = n.TaskDef.TriggerDef;
            if (triggerDef != null && triggerDef.ExprSeq != null)
                WriteReceive(triggerDef.ExprSeq[0]);
            t.WriteLine("{0} _bwxExpert = new {0}();", n.Parent.Parent.Name);
            t.WriteLine("bwxProcess.PushExpert(_bwxExpert);", n.Parent.Parent.Name);
            t.WriteLine("MentalTask bwxTask = new Method(bwxProcess, bwxMsg);");
            t.WriteLine("bwxProcess.ScheduleTask(bwxTask, _bwxExpert.{0}({1}));", n.Name + "Proc", n.TaskDef.CallString);
        }
    }
    public class ExpertConstructorDefVisit<T, N> : BlockStmtVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : ExpertConstructorDef
    {
        protected override void WriteHeader(N n)
        {
            t.WriteLine("public {0}()", n.Name);
        }
        public override void BeginVisit(N n)
        {
            base.BeginVisit(n);
            t.WriteLine("Boot = Create;");
            WriteTriggerDefs(n);
        }
        public override void DoVisit(N n)
        {
        }
        public void WriteTriggerDefs(N n)
        {
            foreach (TriggerStmt triggerDef in ((ExpertDef)n.Parent).TriggerDefs)
            {
                t.WriteIndent();
                t.Write("AddTrigger(");
                t.Visit(triggerDef);
                t.WriteStatementEnd(1);
            }
        }
    }

}
