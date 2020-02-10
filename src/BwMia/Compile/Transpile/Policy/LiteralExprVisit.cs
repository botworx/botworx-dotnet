using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    public class LiteralExprVisit<T, N> : NodeVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : LiteralExpr
    {
        public override void DoVisit(N n)
        {
            t.Write(n.Value.ToString());
        }
    }
}
