using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    public class RootBlockVisit<T, N> : NodeVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : RootBlock
    {
        public override void DoVisit(N n)
        {
            t.WriteLine("using System;");
            t.WriteLine("using System.Collections.Generic;");
            t.WriteLine("using System.Linq;");
            t.WriteLine("using System.Text;");
            t.WriteLine("using System.IO;");
            t.WriteLine();
            t.WriteLine("using Botworx.Mia;");
            t.WriteLine("using Botworx.Mia.Runtime;");
            t.WriteLine();

            foreach (KeyValuePair<string, EntityDef> kvp in n.EntityDictionary)
            {
                n.BrainDef.AddChild(kvp.Value);
            }
            base.DoVisit(n);
        }
    }
}
