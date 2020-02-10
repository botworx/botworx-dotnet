using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class StmtBlock : Stmt
    {
        public StmtBlock() : base(AstNodeKind.Block){}
        public StmtBlock(AstNodeKind kind) : base(kind) { }
        public StmtBlock(AstNodeKind kind, Token token) : base(kind, token){}
        //
        public override bool IsExitable { get { return true; } }
        bool needsExit = false;
        public override bool NeedsExit
        {
            get { return needsExit; }
            set
            {
                if (IsExitable)
                    needsExit = true;
                else
                    Parent.NeedsExit = value;
            }
        }
    }
    public class DoStmt : StmtBlock
    {
        public DoStmt() : base(AstNodeKind.DoStmt) { }
    }
    public class ParallelDef : StmtBlock
    {
        public ParallelDef() : base(AstNodeKind.ParallelDef) { }
    }
}
