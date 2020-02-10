using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Runtime
{
    public delegate void MessageCallback(Process context, Expert expert, Message msg);

    public struct Trigger
    {
        public MessageCallback Callback;
        public MessagePattern MessagePattern;
        //
        public Trigger(MessageCallback callback, MessagePattern messagePattern)
        {
            Callback = callback;
            MessagePattern = messagePattern;
        }
    }
}
