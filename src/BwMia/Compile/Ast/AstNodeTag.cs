using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class AstNodeTag
    {
        public AstNode Node;
        //
        public AstNodeTag(AstNode node)
        {
            Node = node;
        }
    }
    public class MessageTag : AstNodeTag
    {
        public MessageKind MessageKind;
        public bool IsProposal;
        public Token ContextToken;
        //public Token Sender = TokenInstance.CSharp.BWXTASK;
        public AstNode Sender = Expression.Nil;
        //public Token Result = TokenInstance.CSharp._BWXRESULT;
        public AstNode Result = Expression.Nil;
        public bool Wait = true;
        public TaskDef TaskDef;
        //
        public MessageTag(AstNode node)
            : base(node)
        {
        }
    }
}
