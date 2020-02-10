using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

using Botworx.Mia.Runtime;

namespace Botworx.AgentLib.ServerLib
{
    public class AgencySession : Session<Agency>, IAgencyServiceBase
    {
        public AgencySession(AgencyService service, Agency inner, IAgentCallback outer, Guid guid) : base(service, inner, outer, guid) { }
        //
        public List<string> GetBrainFactoryNames()
        {
            return Agency.Instance.GetBrainClassNames();
        }
        public Guid CreateAgent(string brainFactoryName)
        {
            Agent agent = Agency.Instance.CreateAgent(brainFactoryName);
            return Service.Intern(agent);
        }
    }
}
