using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy.Rhs
{
    /*public class RhsNameVisit<T, N> : NameVisit<T, N>
        where T : Transpiler, IVisitor
        where N : Name
    {
        public override void DoVisit(N n)
        {
            if (t.ParentNode is ClauseExpr || !n.IsVarName)
            {
                base.DoVisit(n);
                return;
            }
            //else
            t.Write("(Clause){0}", n.Name);
        }
    }*/
    public class RhsNameVisit : NameVisit<Transpiler, Name, RhsMsgEffectVisit>
    {
        public override void DoVisit(Name n, RhsMsgEffectVisit cb)
        {
            if (t.ParentNode is ClauseExpr || !n.IsVarName)
            {
                base.DoVisit(n);
                return;
            }
            //else
            //t.Write("(Clause){0}", n.Name);
            cb.Visitback(n, () => t.Write("(Clause){0}", n.Name));
        }
        /*public override void DoVisit(Name n, RhsMsgEffectVisit cb)
        {
            //cb.Visitback(n, () => base.DoVisit(n)); //Stupid compiler bug.  
            cb.Visitback(n, () => BaseDoVisit(n));
        }
        void BaseDoVisit(Name n)
        {
            base.DoVisit(n);
        }*/
    }

}
