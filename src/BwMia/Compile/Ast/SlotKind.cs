using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public enum SlotKind
    {
        //Dynamic = -1,//Bad Idea.  Cast to enum value type elsewhere.
        Subject = 0,
        Predicate,
        Object
    }
}
