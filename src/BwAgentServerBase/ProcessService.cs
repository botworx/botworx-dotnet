using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Description;

using Botworx.Mia.Runtime;

namespace Botworx.AgentLib.ServerLib
{
    public class ProcessService : Service
    {
        //
        public List<string> GetClauses(Guid contextId)
        {
            ProcessSession session = GetContextSession(contextId);
            return session.GetClauses(contextId);
        }
    }
}
