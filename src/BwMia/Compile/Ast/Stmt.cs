using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class Stmt : AstNode
    {
        public ExprSeq ExprSeq;
        public Expression Expression { get { return ExprSeq[0]; } }
        public bool IsIterator = false;
        //
        public Stmt(AstNodeKind kind) : base(kind) { }
        public Stmt(AstNodeKind kind, Token token) : base(kind, token) { }
        //
        public override void Resolve()
        {
            if(ExprSeq != null)
                ExprSeq.Resolve();
            base.Resolve();
        }
    }
}
