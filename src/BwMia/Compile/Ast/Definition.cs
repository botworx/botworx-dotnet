using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class Definition : AstNode
    {
        public Expression AtomTypeExpr { get; set; }
        public Token Type { get; set; } //Used for specialization.  This is the T in Atom<T>
        public Definition(AstNodeKind kind, Token token = null)
            : base(kind, token)
        {
            Type = TokenInstance.CSharp.ELEMENT;
        }
        public override void Resolve()
        {
            base.Resolve();
            if(AtomTypeExpr != null)
                AtomTypeExpr.Resolve();
        }
        public virtual Name CreateName()
        {
            return new Name(this);
        }
    }
}
