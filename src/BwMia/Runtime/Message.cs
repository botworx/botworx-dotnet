using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Runtime
{
    public delegate void MessageDelegate(Message msg);
    //
    public struct Message
    {
        public static Message NilMessage = new Message(MessageKind.Nil);
        //
        public MessageKind Kind;
        public Task Sender;
        public TaskResult Result;
        public Clause Clause;
        //
        public Message(MessageKind kind)
        {
            Kind = kind;
            Sender = null;
            Result = null;
            Clause = null;
        }
        public Message(Task sender, Clause clause)
        {
            Kind = MessageKind.Add;
            Sender = sender;
            Result = null;
            Clause = clause;
        }
        public Message(MessageKind kind, Task sender, TaskResult result, Clause clause)
        {
            Kind = kind;
            Sender = sender;
            Result = result;
            Clause = clause;
        }
        //
        public Message Clone()
        {
            Message message = (Message)MemberwiseClone();
            return message;
        }
        public override string ToString()
        {
            string clauseString = "";
            if (Clause != null)
                clauseString = Clause.ToString();
            else
                clauseString = "null";

            string msgString = string.Format("{0} {1} ", Kind.ToString(), clauseString);
            return msgString;
        }
    }
}