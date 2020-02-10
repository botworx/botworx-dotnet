using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class EntityDef : AtomDef
    {
        public EntityDef Value { get { return this; } }
        //
        public bool IsBuiltin = false;
        //
        public EntityDef(Token token, bool isBuiltIn = false)
            : this(AstNodeKind.EntityDef, token, isBuiltIn)
        {
        }
        public EntityDef(AstNodeKind kind, Token token, bool isBuiltIn = false)
            : base(kind, token)
        {
            IsBuiltin = isBuiltIn;
        }
    }
}
