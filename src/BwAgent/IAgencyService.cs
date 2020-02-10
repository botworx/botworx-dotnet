using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace Botworx.AgentLib
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IAgentCallback))]
    public interface IAgencyService 
        : IAgencyServiceBase, IAgentService, IBrainService, IProcessService
    {
        [OperationContract()]
        Guid GetAgency();
    }

    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IAgencyServiceBase
    {
        [OperationContract()]
        List<string> GetBrainFactoryNames();
        [OperationContract()]
        Guid CreateAgent(string brainFactoryName);
    }
}
