using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy.Trigger
{
    public class TriggerMessageExprVisit<T, N> : TriggerClauseExprVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : ClauseExpr
    {
        public override void DoVisit(N n)
        {
            t.Write("new MessagePattern(MessageKind.{0}, ", n.MessageTag.MessageKind);
            base.DoVisit(n);
            t.Write(")");
        }
    }
}
