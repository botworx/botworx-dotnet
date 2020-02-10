using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class LiteralExpr : Expression
    {
        public object Value;
        //
        public LiteralExpr(Token token)
            : this(AstNodeKind.LiteralExpr, token.Value)
        {
        }
        public LiteralExpr(AstNodeKind kind, object value)
            : base(kind)
        {
            Value = value;
        }
    }
}
