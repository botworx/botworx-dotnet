using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy.Rhs
{
    public class SnippetEffectVisit<T, N> : NodeVisit<T, N>
        where T : Transpiler
        where N : AstNode
    {
        public override void DoVisit(N n)
        {
            t.WriteLine(n.Name);
        }
    }

    public class HaltDefVisit<T, N> : NodeVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : AstNode
    {
        public override void DoVisit(N n)
        {
            t.WriteLine("bwxTask.Process.Halt();");
        }
    }

    public class YieldDefVisit<T, N> : NodeVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : AstNode
    {
        public override void DoVisit(N n)
        {
            string action = "";
            switch (n.NodeKind)
            {
                case AstNodeKind.SucceedDef:
                    action = "Succeed";
                    break;
                case AstNodeKind.FailDef:
                    action = "Fail";
                    break;
                case AstNodeKind.ThrowDef:
                    action = "Throw";
                    break;
                case AstNodeKind.ReturnDef:
                    action = "Return";
                    break;
            }
            t.WriteLine("yield return {0}(bwxTask.Process, bwxTask.Message);", action);
        }
    }
}
