using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Runtime
{
    public struct MessagePattern
    {
        public static MessagePattern NilMessagePattern = new MessagePattern(MessageKind.Nil);
        //
        public MessageKind Kind;
        public ClausePattern ClausePattern;
        //
        public MessagePattern(MessageKind kind)
        {
            Kind = kind;
            ClausePattern = new ClausePattern();
        }
        public MessagePattern(MessageKind kind, ClausePattern clausePattern)
        {
            Kind = kind;
            ClausePattern = clausePattern;
        }
        //
        public bool Match(Message msg)
        {
            if (Kind != msg.Kind)
                return false;
            if (Kind == MessageKind.Nil)
                return true;
            if (!ClausePattern.Match(msg.Clause))
                return false;
            return true;
        }
    }
}
