using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy.Lhs
{
    public class ClauseCondStmtVisit<T, N> : CondStmtVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : LhsCondStmt
    {
        public override void DoVisit(N n)
        {
            base.DoVisit(n);
        }
    }
}
