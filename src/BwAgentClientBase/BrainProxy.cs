using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.AgentLib.ClientLib
{
    public delegate void ContextCreated(ProcessProxy context);

    public class BrainProxy : Proxy<IBrainService>
    {
        public event ContextCreated ContextCreated;
        //
        public BrainProxy(Client client, IBrainService outer, Guid guid) : base(client, outer, guid) { }
        //
        public void Run()
        {
            Outer.Run(Guid);
        }
        //Invoked by client.
        public void FireContextCreated(ProcessProxy context)
        {
            ContextCreated(context);
        }
        public void ObserveContextCreated(ContextCreated handler)
        {
            ContextCreated += handler;
        }
    }
}
