using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    public class NilExprVisit<T, N> : NodeVisit<T, N>
        where T : Transpiler
        where N : Expression
    {
        public override void DoVisit(N n)
        {
            t.Write("null");
        }
    }
}
