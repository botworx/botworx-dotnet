using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Runtime;

namespace Botworx.Mia.Compile.Ast
{
    class SnippetEffect : AstNode
    {
        public SnippetEffect(Token token): base(AstNodeKind.SnippetEffect, token){}
    }

    class HaltDef : AstNode
    {
        public HaltDef() : base(AstNodeKind.HaltDef) { }
    }

    class SucceedDef : AstNode
    {
        public SucceedDef() : base(AstNodeKind.SucceedDef) { }
    }
    class FailDef : AstNode
    {
        public FailDef() : base(AstNodeKind.FailDef) { }
    }
    class ThrowDef : AstNode
    {
        public ThrowDef() : base(AstNodeKind.ThrowDef) { }
    }
    class ReturnDef : AstNode
    {
        public ReturnDef() : base(AstNodeKind.ReturnDef) { }
    }
}
