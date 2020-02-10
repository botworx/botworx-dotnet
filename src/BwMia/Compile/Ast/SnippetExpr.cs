using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class SnippetExpr : Expression
    {
        public SnippetExpr(Token name)
            : base(AstNodeKind.Snippet, name)
        {
        }
    }
}
