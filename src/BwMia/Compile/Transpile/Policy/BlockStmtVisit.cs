using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    public class BlockStmtVisit<T, N> : NodeVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : AstNode
    {
        public override void BeginVisit(N n)
        {
            WriteHeader(n);
            t.StartBlock(n);
        }
        public override void EndVisit(N n)
        {
            t.EndBlock(n);
            WriteFooter(n);
            if (n.NeedsExit)
                t.WriteLine(n.GenerateExitLabel());
        }
        protected virtual void WriteHeader(N n)
        {
        }
        protected virtual void WriteFooter(N n)
        {
        }
    }
}
