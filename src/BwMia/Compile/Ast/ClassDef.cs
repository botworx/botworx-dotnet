using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class ClassDef : NamespaceBlock
    {
        public Token BaseToken;
        public string BaseName
        {
            get { return (string)BaseToken.Value; }
        }
        //
        public ClassDef(Token token, Token baseToken) : base(AstNodeKind.ClassDef, token)
        {
            BaseToken = baseToken;
        }
    }
}
