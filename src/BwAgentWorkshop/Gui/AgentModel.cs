using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.AgentLib.ClientLib.Workshop.Gui
{
    public class AgentModel : Model
    {
        public AgentProxy Agent;
        public BrainProxy Brain;
        //
        public void Open(string name)
        {
            AgencyClient client = new AgencyClient();
            AgencyProxy agency = client.GetAgency();

            Agent = agency.CreateAgent(name);
            Brain = Agent.GetBrain();
        }
    }
}
