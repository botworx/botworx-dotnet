using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    //
    public class ClauseExprVisit : ClauseExprVisit<ClauseExpr, NodeVisit>
    {
    }
    public class ClauseExprVisit<N> : ClauseExprVisit<N, NodeVisit>
        where N : ClauseExpr
    {
    }
    public class ClauseExprVisit<N, C> : TranspilerVisit<N, C>
        where N : ClauseExpr
        where C : NodeVisit
    {
        public override void DoVisit(N n)
        {
            Token Spec = n.Predicate.ToPredicate().Spec;
            if (n.IsProperty)
            {
                //t.Write("{0}[{1}] = ({2}){3}", t.Translate(n.Subject), t.Translate(n.Predicate), Spec, t.Translate(n.Object));
                t.Write("{0}[{1}] = {3}", t.Translate(n.Subject), t.Translate(n.Predicate), t.Translate(n.Object));
                return;
            }
            //else
            t.Write("new Clause({0}, ", t.Translate(n.AtomTypeExpr));
            //t.Write("{0}, {1}, ({2})({3})", t.Translate(n.Subject), t.Translate(n.Predicate), Spec, t.Translate(n.Object));
            t.Write("{0}, {1}, {2}", t.Translate(n.Subject), t.Translate(n.Predicate), t.Translate(n.Object));
            t.Write(")");
            if (n.PropertyExprs.Count != 0)
            {
                WriteProperties(n);
            }
        }
        public void WriteProperties(N n)
        {
            t.Write("{{ ");
            foreach (var propExpr in n.PropertyExprs)
            {
                t.Write("{{ ");
                t.Write("{0}, {1}", t.Translate(propExpr.Predicate), t.Translate(propExpr.Object));
                t.Write(" }} ");
            }
            t.Write("}}");
        }
    }
}
