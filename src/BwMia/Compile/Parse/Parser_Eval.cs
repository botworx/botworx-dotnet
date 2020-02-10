//#define DEBUG_EXPRESSION_PARSING

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Parse
{
    public partial class Parser
    {
        Stack<Expression> ExprStack = new Stack<Expression>();
        //
        public void PushExpr(Expression expr)
        {
            ExprStack.Push(expr);
        }
        public Expression PopExpr()
        {
            return ExprStack.Pop();
        }
        public Expression PeekExpr()
        {
            return ExprStack.Peek();
        }
        public Expression CurrentExpr { get { return PeekExpr(); } }
        //
        //
        public Expression Eval(List<Token> ops)
        {
            foreach (var op in ops)
            {
                CurrentDialect.GetAction(op)(op);
            }

            //Console.ReadKey();
            //Console.WriteLine();

            if (ExprStack.Count == 0)
                return null;
            //else
#if DEBUG_EXPRESSION_PARSING
            Debug.WriteLine("EvalStack Dump");
            Debug.WriteLine("----------------");
            foreach (var op in ExprStack)
            {
                Debug.Write(op.ToString());
                Debug.Write("\t\t\t");
                Debug.Write(op.NodeKind);
                Debug.Write(" : ");
                Debug.WriteLine(op.NodeKind);
            }
            Debug.WriteLine("");
#endif
            Expression expr = ExprStack.Pop();
            return expr;
        }
        //
        public void EvalNop(Token op)
        {
        }
        public void EvalNil(Token op)
        {
            PushExpr(Expression.Nil);
        }
        public void EvalSubjectMacro(Token op)
        {
            PushExpr(Subject);
        }
        public void EvalError(Token op)
        {
            throw new Exception();
        }
        public void EvalVariable(Token op)
        {
            PushExpr(new Name(op));
        }
        public void EvalEntity(Token op)
        {
            PushExpr(new Name(op));
        }
        public void EvalLiteral(Token op)
        {
            PushExpr(new LiteralExpr(op));
        }
        public void EvalSnippet(Token op)
        {
            PushExpr(new SnippetExpr(op));
        }
        public void EvalType(Token op)
        {
            Expression expr = PeekExpr();
            expr.AtomTypeExpr = new Name(op);
        }
        public void EvalPredicate(Token op)
        {
            EvalClause(op);
        }
        public void EvalProperty(Token op)
        {
            Expression argument = PopExpr();
            Expression subject = PopExpr();
            Expression predicate = new Name(op);
            ClauseExpr expr = new ClauseExpr(predicate, argument);
            subject.AddPropertyExpr(expr);
            PushExpr(subject);
        }
        public ClauseExpr EvalClause(Token op)
        {
            Expression argument = PopExpr();
            Expression subject = PopExpr();
            Expression klassExpr = null;
            if (subject == Expression.Nil)
            {
                subject = BuiltinDefs.Self.CreateName();
                klassExpr = BuiltinDefs.Perform.CreateName();
            }
            else if (subject.IsClauseExpr)
            {
                subject = new Name(TokenInstance.CSharp._BWXSUBJECT);
            }
            Expression predicate = new Name(op);
            ClauseExpr expr = new ClauseExpr(subject, predicate, argument);

            expr.AtomTypeExpr = klassExpr;

            PushExpr(expr);
            //
            ExprSeq.AddChild(expr);
            return expr;
        }
        public void EvalLineList(Token op)
        {
            CreateState((TokenList)op.Value);
            Subject = PeekExpr();
            Subject.Binding = TokenInstance.CSharp._BWXSUBJECT;
            Expression expr = ParseExpression();
            PopState();
        }

        public void EvalAttempt(Token op)
        {
            AstNode subject = PeekExpr();
            subject.MessageTag.MessageKind = MessageKind.Attempt;
        }
        public void EvalWaitNot(Token op)
        {
            AstNode subject = PeekExpr();
            subject.MessageTag.Wait = false;
        }
        public void EvalPropose(Token op)
        {
            AstNode subject = PeekExpr();
            subject.MessageTag.IsProposal = true;
        }
        public void EvalAssert(Token op)
        {
            AstNode subject = PeekExpr();
            subject.MessageTag.MessageKind = MessageKind.Add;
        }
        public void EvalRetract(Token op)
        {
            AstNode subject = PeekExpr();
            subject.MessageTag.MessageKind = MessageKind.Remove;
        }
        public void EvalAssignRight(Token op)
        {
            Expression argument = PopExpr();
            Expression subject = PeekExpr();
            subject.Binding = argument.Token;
        }
        public void EvalNegate(Token op)
        {
            Expression subject = PeekExpr();
            subject.Negated = true;
        }
        public void EvalNotEqual(Token op)
        {
            Expression argument = PopExpr();
            Expression subject = PopExpr();
            Expression expr = Create.NotEqual(op, subject, argument);
            ExprSeq.AddChild(expr);
            PushExpr(expr);
        }
        public void EvalEqual(Token op)
        {
            Expression argument = PopExpr();
            Expression subject = PopExpr();
            Expression expr = Create.Equal(op, subject, argument);
            ExprSeq.AddChild(expr);
            PushExpr(expr);
        }
    }
}
