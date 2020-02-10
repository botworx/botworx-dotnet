using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy.Lhs
{
    //TODO:Remember condition blocks are terminated at the end by cleanup lambdas.
    public class CondStmtVisit<T, N> : StmtVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : CondStmt
    {
        public override void DoVisit(N n)
        {
            base.DoVisit(n);
            t.StartBlock(n);
        }
    }

    public class LhsSnippetExprVisit<T, N> : NodeVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : SnippetExpr
    {
        public override void DoVisit(N n)
        {
            t.WriteLine("if(" + n.Name + ")");
            base.DoVisit(n);
        }
    }

}
