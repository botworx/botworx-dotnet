using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class Expression : Definition
    {
        public static Expression Nil = new Expression(AstNodeKind.Nil, TokenInstance.CSharp.NIL);
        //
        public Token Binding;
        public bool Negated;
        public List<ClauseExpr> PropertyExprs = new List<ClauseExpr>();
        //
        public Expression(AstNodeKind kind, Token token = null)
            : base(kind, token)
        {
        }
        public bool HasBinding { get { return Binding != null; } }
        public bool IsClauseExpr { get { return NodeKind == AstNodeKind.ClauseExpr; } }
        public virtual void CollectVariables(List<Var> vars) { }
        public virtual PredicateDef ToPredicate()
        {
            return null;
        }
        public void AddPropertyExpr(ClauseExpr expr)
        {
            PropertyExprs.Add(expr);
        }
    }
    public class ExprSeq : Expression
    {
        public Expression this[int ndx]
        {
            get
            {
                return (Expression)Children[ndx];
            }
        }
        //
        public ExprSeq()
            : base(AstNodeKind.ExprSeq)
        {
        }
    }
}
