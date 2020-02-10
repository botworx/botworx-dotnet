using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy.Context
{
    public class ContextNameVisit<T, N> : NameVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : Name
    {
        public override void DoVisit(N n)
        {
            if (t.ParentNode is ClauseExpr)
            {
                base.DoVisit(n);
                return;
            }
            //else
        }
    }
}
