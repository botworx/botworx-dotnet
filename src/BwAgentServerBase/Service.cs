using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Description;

using Botworx.Mia.Runtime;

namespace Botworx.AgentLib.ServerLib
{
    public class Service
    {
        public IAgentCallback Callback = OperationContext.Current.GetCallbackChannel<IAgentCallback>();

        public Dictionary<Guid, AgentSession> Agents = new Dictionary<Guid, AgentSession>();
        public Dictionary<Guid, BrainSession> Brains = new Dictionary<Guid, BrainSession>();
        public Dictionary<Guid, ProcessSession> Contexts = new Dictionary<Guid, ProcessSession>();
        //
        public Guid Intern(Agent agent)
        {
            AgentSession interned = null;
            if (!Agents.TryGetValue(agent.Guid, out interned))
            {
                interned = new AgentSession(this, agent, Callback, agent.Guid);
                Agents.Add(agent.Guid, interned);
            }
            return interned.Guid;
        }
        public AgentSession GetAgentSession(Guid guid)
        {
            AgentSession session = null;
            Agents.TryGetValue(guid, out session);
            return session;
        }
        public Guid Intern(Brain brain)
        {
            BrainSession interned = null;
            if (!Brains.TryGetValue(brain.Guid, out interned))
            {
                interned = new BrainSession(this, brain, Callback, brain.Guid);
                Brains.Add(brain.Guid, interned);
            }
            return interned.Guid;
        }
        public BrainSession GetBrainSession(Guid guid)
        {
            BrainSession session = null;
            Brains.TryGetValue(guid, out session);
            return session;
        }
        public Guid Intern(Process process)
        {
            ProcessSession interned = null;
            if (!Contexts.TryGetValue(process.Guid, out interned))
            {
                interned = new ProcessSession(this, process, Callback, process.Guid);
                Contexts.Add(process.Guid, interned);
            }
            return interned.Guid;
        }
        public ProcessSession GetContextSession(Guid guid)
        {
            ProcessSession session = null;
            Contexts.TryGetValue(guid, out session);
            return session;
        }
    }
}
