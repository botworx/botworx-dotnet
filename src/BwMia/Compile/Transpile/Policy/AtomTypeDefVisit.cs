using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    public class AtomTypeDefVisit<T, N> : NodeVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : AtomTypeDef
    {
        public override void DoVisit(N n)
        {
            if (n.IsBuiltin)
                return;
            //else
            if (n.BaseType == null)
                t.WriteLine("public static AtomType Ent_{0} = new AtomType(\"{0}\", null);", n.Name);
            else
                t.WriteLine("public static AtomType Ent_{0} = new AtomType(\"{0}\", {1});", n.Name, t.Translate(n.AtomTypeExpr));
        }
    }
}
