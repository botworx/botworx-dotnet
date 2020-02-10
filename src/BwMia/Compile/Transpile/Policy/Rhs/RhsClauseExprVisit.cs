using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy.Rhs
{
    public class RhsClauseExprVisit : ClauseExprVisit<ClauseExpr, RhsMsgEffectVisit>
    {
        public override void DoVisit(ClauseExpr n, RhsMsgEffectVisit cb)
        {
            //cb.Visitback(n, () => base.DoVisit(n)); //Stupid compiler bug.  
            if (n.IsProperty)
            {
                t.WriteIndent();
                BaseDoVisit(n);
                t.WriteStatementEnd();
            }
            else
                cb.Visitback(n, () => BaseDoVisit(n));
        }
        void BaseDoVisit(ClauseExpr n)
        {
            base.DoVisit(n);
        }
    }
}
