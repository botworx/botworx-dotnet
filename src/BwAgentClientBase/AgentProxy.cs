using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace Botworx.AgentLib.ClientLib
{
    public class AgentProxy : Proxy<IAgentService>
    {
        public AgentProxy(Client client, IAgentService outer, Guid guid) : base(client, outer, guid) { }
        //
        public BrainProxy GetBrain()
        {
            BrainProxy brain;
            brain = Client.InternBrain(Outer.GetBrain(Guid));
            return brain;
        }
    }
}
