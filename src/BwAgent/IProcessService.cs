using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace Botworx.AgentLib
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IProcessService
    {
        [OperationContract()]
        List<string> GetClauses(Guid contextId);
    }
}
