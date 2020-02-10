using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy.Context
{
    public class ContextClauseExprVisit<N> : ClauseExprVisit<N>
        where N : ClauseExpr
    {
        public override void DoVisit(N n)
        {
            t.WriteIndent();
            if (n.HasBinding)
                t.Write("{0} = ", n.Binding.Translate());
            if (n.IsProperty)
                base.DoVisit(n);
            else
            {
                t.Write("_bwxContext.Add(");
                base.DoVisit(n);
                t.Write(")");
            }
            t.WriteStatementEnd();
        }
    }
}
