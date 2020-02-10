using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class OperatorExpr : ClauseExpr
    {
        public OperatorExpr(AstNodeKind kind, Token token)
            : base(kind, token)
        {
        }
    }
    public class BinaryOp : OperatorExpr
    {
        public BinaryOp(AstNodeKind kind, Token token, Expression left, Expression right)
            : base(kind, token)
        {
            Subject = left;
            //Predicate = BuiltinDefs.NotEqual.CreateName();
            Predicate = new Name(token);
            Object = right;
        }
        public override bool IsBinary
        {
            get { return true; }
        }
    }
    public static partial class Create
    {
        public static BinaryOp NotEqual(Token token, Expression left, Expression right)
        {
            return new BinaryOp(AstNodeKind.NotEqualExpr, token, left, right);
        }
        public static BinaryOp Equal(Token token, Expression left, Expression right)
        {
            return new BinaryOp(AstNodeKind.EqualExpr, token, left, right);
        }
    }
}
