using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.AgentLib.ClientLib
{
    public class AgencyProxy : Proxy<IAgencyService>
    {
        //
        public AgencyProxy(AgencyClient client, IAgencyService outer, Guid guid)
            : base(client, outer, guid)
        {
        }
        public List<string> GetBrainFactoryNames()
        {
            return Outer.GetBrainFactoryNames();
        }
        public AgentProxy CreateAgent(string brainFactoryName)
        {
            return new AgentProxy(Client, Client.Channel, Outer.CreateAgent(brainFactoryName));
        }
    }
}
