using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

using Botworx.Mia.Runtime;

namespace Botworx.AgentLib.ServerLib
{
    public class BrainSession : Session<Brain>, IBrainService
    {
        public BrainSession(Service service, Brain inner, IAgentCallback outer, Guid guid) : base(service, inner, outer, guid) 
        {
            //TODO:Implement Open and close methods ...
            inner.ProcessCreated += OnProcessCreated;
        }
        //
        public void Run(Guid brainId)
        {
            Inner.Run();
        }
        //Callbacks
        private void OnProcessCreated(Process process)
        {
            Guid contextId = Service.Intern(process);
            Guid parentId;
            if (process.Parent != null)
                parentId = process.Parent.Guid;
            else
                parentId = contextId;
            string label;

            if (process.Proposal.Message.Clause != null)
                label = process.Proposal.Message.Clause.ToString();
            else
                label = "Root";

            Outer.OnContextCreated(Guid, contextId, parentId, label);
        }
    }
}
