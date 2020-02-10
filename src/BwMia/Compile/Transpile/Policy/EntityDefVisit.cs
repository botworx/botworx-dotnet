using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    public class EntityDefVisit<T, N> : NodeVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : EntityDef
    {
        public override void DoVisit(N n)
        {
            if (n.IsBuiltin)
                return;
            //else
            string name = t.FixName(n.Name);
            if (n.AtomTypeExpr == null)
                t.WriteLine("public static Entity Ent_{0} = new Entity(\"{0}\");", name);
            else
                t.WriteLine("public static Entity Ent_{0} = new Entity(\"{0}\", {1});", name, t.Translate(n.AtomTypeExpr));
        }
    }
}
