using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    public class CaseStmtVisit<T, N> : StmtVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : CaseStmt
    {
        public override void DoVisit(N n)
        {
            t.PushLhsPolicy();
            base.DoVisit(n);
            t.PopPolicy();
        }
    }
}
