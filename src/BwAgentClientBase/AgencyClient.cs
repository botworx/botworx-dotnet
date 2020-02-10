using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace Botworx.AgentLib.ClientLib
{
    public class AgencyClient : Client, IAgentCallback
    {
        //
        public AgencyClient()
        {
            EndpointAddress endpointAddress = new EndpointAddress("http://localhost:8090/Agency");
            WSDualHttpBinding binding = new WSDualHttpBinding();
            binding.ClientBaseAddress = new Uri("http://localhost:808/WSDualOnXP/");

            DuplexChannelFactory<IAgencyService> channelFactory =
                 new DuplexChannelFactory<IAgencyService>(
                    this,
                    binding,
                    endpointAddress);

            Channel = channelFactory.CreateChannel();
        }
        public AgencyProxy GetAgency()
        {
            return new AgencyProxy(this, Channel, Channel.GetAgency());
        }
        //
        public void OnContextCreated(Guid brainId, Guid contextId, Guid parentId, string label)
        {
            ProcessProxy context = InternContext(contextId);
            if(contextId != parentId)
                context.Parent = GetContextProxy(parentId);
            context.Label = label;
            //
            BrainProxy brain = GetBrainProxy(brainId);
            brain.FireContextCreated(context);
        }
    }
}
