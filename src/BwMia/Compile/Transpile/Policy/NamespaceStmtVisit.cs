using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    public class NamespaceStmtVisit<T, N> : BlockStmtVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : AstNode
    {
        protected override void WriteHeader(N n)
        {
            t.WriteLine("namespace {0}", n.Name);
        }
    }
    public class UsingStmtVisit<T, N> : NodeVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : AstNode
    {
        public override void DoVisit(N n)
        {
            t.WriteLine("using {0};", n.Name);
        }
    }
    public class ClassStmtVisit<T, N> : NamespaceStmtVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : ClassDef
    {
        protected override void WriteHeader(N n)
        {
            if (n.BaseName == "")
                t.WriteLine("public class {0}", n.Name);
            else
                t.WriteLine("public class {0} : {1}", n.Name, n.BaseName);
        }
    }
}
