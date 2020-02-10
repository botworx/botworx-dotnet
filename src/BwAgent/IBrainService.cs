using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace Botworx.AgentLib
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IBrainService
    {
        [OperationContract(IsOneWay = true)]
        void Run(Guid brainId);
    }
}
