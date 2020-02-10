using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

using Botworx.Mia.Runtime;

namespace Botworx.AgentLib.ServerLib
{
    public class AgentSession : Session<Agent>, IAgentService
    {
        public AgentSession(Service service, Agent inner, IAgentCallback outer, Guid guid) : base(service, inner, outer, guid) { }
        //
        public Guid GetBrain(Guid agentId)
        {
            return Service.Intern(Inner.Brain);
        }
    }
}
