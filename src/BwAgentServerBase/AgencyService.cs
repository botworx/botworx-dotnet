using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Description;

using Botworx.Mia.Runtime;

namespace Botworx.AgentLib.ServerLib
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class AgencyService : AgentService, IAgencyService
    {
        AgencySession Session;
        AgencyService()
        {
            Session = new AgencySession(this, Agency.Instance, Callback, Agency.Instance.Guid);
        }
        //
        public class HostFactory : ServiceHostFactory
        {
            public HostFactory(Uri uri)
                : base(uri)
            {
            }
            public override void Create()
            {
                ServiceHost host = new ServiceHost(typeof(AgencyService), Uri);
                host.AddServiceEndpoint(typeof(IAgencyService), new WSDualHttpBinding(), "");
                //Enable metadata exchange
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                host.Description.Behaviors.Add(smb);
                //Start the Service
                host.Open();
            }
        }
        public Guid GetAgency() 
        {
            return Session.Guid;
        }
        public List<string> GetBrainFactoryNames()
        {
            return Session.GetBrainFactoryNames();
        }
        public Guid CreateAgent(string brainFactoryName)
        {
            return Session.CreateAgent(brainFactoryName);
        }
    }
}
