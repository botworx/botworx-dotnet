using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy.Trigger
{
    public class TriggerNameVisit<T, N> : NameVisit<T, N>
        where T : Transpiler, INodeVisitor
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
            t.Write("new MessagePattern(MessageKind.{0}, ", n.MessageTag.MessageKind);
            t.Write("new ClausePattern({0}, null, null, null, MatchFlag.SubjectX | MatchFlag.PredicateX | MatchFlag.ObjectX)", t.Translate(n.AtomTypeExpr));
            t.Write(")");
        }
    }
}
