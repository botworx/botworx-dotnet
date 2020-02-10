using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    public class TaskDefVisit<T, N> : NodeVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : TaskDef
    {
        public override void BeginVisit(N n)
        {
            n.ParameterString = n.CreateParameterString(t, true);
            n.CallString = n.CreateParameterString(t, false);
            //
            base.BeginVisit(n);
            t.StartScope(n);
        }
        public override void EndVisit(N n)
        {
            t.EndScope(n);
            base.EndVisit(n);
        }
    }
}
