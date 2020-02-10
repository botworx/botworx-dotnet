using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile
{
    public class Var : AstNode
    {
        public Token TypeToken;
        //
        public Var(Token token) : this(token, null)
        {
            TypeToken = TokenInstance.CSharp.ELEMENT;
        }
        public Var(Token token, Token typeToken)
            : base(AstNodeKind.Var, token)
        {
            TypeToken = typeToken;
        }
    }
}
