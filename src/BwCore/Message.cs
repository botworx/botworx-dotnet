using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx
{
    public delegate void MessageDelegate(Message msg);
    //
    public class Message
    {
        public static Message NilMessage = new Message();
        //
        public Task Sender;
        //
        public Message()
        {
            Sender = null;
        }
        public Message(Task sender)
        {
            Sender = sender;
        }
    }
}
