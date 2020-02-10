using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Runtime;

namespace Botworx.Mia.Compile.Ast
{
    public class AtomTypeDef : EntityDef
    {
        public AtomTypeDef BaseType;
        public AtomFlag MemberFlags;
        //
        public AtomTypeDef(Token name, AtomTypeDef baseType, bool isBuiltIn = false)
            : base(AstNodeKind.AtomTypeDef, name, isBuiltIn)
        {
            BaseType = baseType;
            if(baseType != null)
                MemberFlags = baseType.MemberFlags;
        }
        public AtomTypeDef(Token name, AtomTypeDef baseType, AtomFlag memberFlags)
            : base(AstNodeKind.AtomTypeDef, name)
        {
            BaseType = baseType;
            MemberFlags = memberFlags;
            IsBuiltin = true;
        }
        //
        public bool IsBelief { get { return (MemberFlags & AtomFlag.Goal) != AtomFlag.Goal; } }
        //
    }
}
