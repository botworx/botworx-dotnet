using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Runtime;

namespace Botworx.Mia.Compile.Ast
{
    public class AtomDef : Definition
    {
        public AtomDef(AstNodeKind kind)
            : this(kind, null)
        {
        }
        public AtomDef(Token token)
            : this(AstNodeKind.AtomDef, token)
        {
        }
        public AtomDef(AstNodeKind kind, Token token)
            : base(kind, token)
        {
            AtomTypeExpr = new Name(TokenInstance.CSharp.TYPE_ENTITY);
        }
    }
}
