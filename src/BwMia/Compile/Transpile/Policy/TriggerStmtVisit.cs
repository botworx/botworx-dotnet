using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    public class TriggerStmtVisit<T, N> : StmtVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : TriggerStmt
    {
        public override void BeginVisit(N n)
        {
            t.PushTriggerPolicy();
            base.BeginVisit(n);
        }
        public override void EndVisit(N n)
        {
            base.EndVisit(n);
            t.PopPolicy();
        }
        public override void DoVisit(N n)
        {
            //TODO:hmmm...
            //string fnName = FindPath(ExpertDef.Parent, MethodDef);
            string fnName = n.TaskDef.Parent.Name + "." + n.TaskDef.Name;
            t.Write("new Trigger({0}, ", fnName);
            if (n.ExprSeq == null)
                t.Write("MessagePattern.NilMessagePattern");
            else
                t.Visit(n.ExprSeq[0]);
            t.Write(")");
        }
    }
}
