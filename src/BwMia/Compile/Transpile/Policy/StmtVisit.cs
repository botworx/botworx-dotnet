using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    public class StmtVisit<T, N> : NodeVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : Stmt
    {
        public override void BeginVisit(N n)
        {
            t.PushStmt(n);
            base.BeginVisit(n);
        }
        public override void EndVisit(N n)
        {
            base.EndVisit(n);
            t.PopStmt();
        }
        public override void DoVisit(N n)
        {
            t.Visit(n.ExprSeq);
            base.DoVisit(n);
        }
    }

}
