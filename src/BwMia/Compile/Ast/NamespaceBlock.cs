using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class NamespaceBlock : StmtBlock
    {
        public NamespaceBlock(Token token) : base(AstNodeKind.NamespaceDef, token) {}
        public NamespaceBlock(AstNodeKind kind, Token token) : base(kind, token) { }
    }
    //
    public class UsingDef : AstNode
    {
        public UsingDef(Token token) : base(AstNodeKind.UsingDef){}
    }
}
