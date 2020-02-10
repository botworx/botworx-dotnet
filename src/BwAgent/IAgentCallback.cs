using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace Botworx.AgentLib
{
    public interface IAgentCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnContextCreated(Guid brainId, Guid contextId, Guid parentId, string label);
    }
}
