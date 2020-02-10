using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    public class ContextDecoratorDefVisit<T, N> : NodeVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : ContextDecoratorDef
    {
        public override void BeginVisit(N n)
        {
            base.BeginVisit(n);
            t.PushContextDef(n.ContextDef);
        }
        public override void EndVisit(N n)
        {
            t.PopContextDef();
            base.EndVisit(n);
        }
    }
}
