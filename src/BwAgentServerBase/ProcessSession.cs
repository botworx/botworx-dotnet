using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

using Botworx.Mia.Runtime;

namespace Botworx.AgentLib.ServerLib
{
    public class ProcessSession : Session<Process>, IProcessService
    {
        public ProcessSession(Service service, Process inner, IAgentCallback outer, Guid guid) : base(service, inner, outer, guid) { }
        //
        public List<string> GetClauses(Guid contextId)
        {
            List<string> clauses = new List<string>();
            foreach (Clause clause in Inner.Context.Clauses)
            {
                clauses.Add(clause.ToString());
            }
            return clauses;
        }
    }
}
