using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    public class NameVisit<T, N> : NameVisit<T, N, NodeVisit>
        where T : Transpiler, INodeVisitor
        where N : Name
    {
    }
    public class NameVisit<T, N, C> : NodeVisit<T, N, C>
        where T : Transpiler, INodeVisitor
        where N : Name
        where C : NodeVisit
    {
        public override void DoVisit(N n)
        {
            string prefix = "";
            if (n.IsEntityName)
                prefix = "Ent_";
            string name = n.Name;
            t.Write(prefix + name);
        }
    }
}
