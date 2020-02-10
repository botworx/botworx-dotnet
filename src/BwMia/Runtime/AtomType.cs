using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Runtime
{
    public class AtomType : Entity
    {
        public AtomType BaseType;
        public AtomFlag MemberFlags;
        public bool IsIntegral = false;
        //
        public AtomType(string name, AtomType baseType, AtomFlag atomTypeFlags)
            : this(name, baseType)
        {
            MemberFlags = atomTypeFlags;
            IsIntegral = true;
        }
        public AtomType(string name, AtomType baseType = null)
            : base(name)
        {
            BaseType = baseType;
            if (baseType != null)
                MemberFlags = baseType.MemberFlags;
            IsIntegral = false;
        }
    }
}
