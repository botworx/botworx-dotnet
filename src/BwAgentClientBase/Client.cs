using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.AgentLib.ClientLib
{
    public class Client
    {
        public IAgencyService Channel;
        public Dictionary<Guid, AgentProxy> Agents = new Dictionary<Guid, AgentProxy>();
        public Dictionary<Guid, BrainProxy> Brains = new Dictionary<Guid, BrainProxy>();
        public Dictionary<Guid, ProcessProxy> Contexts = new Dictionary<Guid, ProcessProxy>();
        //
        public AgentProxy InternAgent(Guid agentGuid)
        {
            AgentProxy interned = null;
            if (!Agents.TryGetValue(agentGuid, out interned))
            {
                interned = new AgentProxy(this, Channel, agentGuid);
                Agents.Add(agentGuid, interned);
            }
            return interned;
        }
        public AgentProxy GetAgentProxy(Guid guid)
        {
            AgentProxy session = null;
            Agents.TryGetValue(guid, out session);
            return session;
        }
        public BrainProxy InternBrain(Guid brainGuid)
        {
            BrainProxy interned = null;
            if (!Brains.TryGetValue(brainGuid, out interned))
            {
                interned = new BrainProxy(this, Channel, brainGuid);
                Brains.Add(brainGuid, interned);
            }
            return interned;
        }
        public BrainProxy GetBrainProxy(Guid guid)
        {
            BrainProxy proxy = null;
            Brains.TryGetValue(guid, out proxy);
            return proxy;
        }
        public ProcessProxy InternContext(Guid contextGuid)
        {
            ProcessProxy interned = null;
            if (!Contexts.TryGetValue(contextGuid, out interned))
            {
                interned = new ProcessProxy(this, Channel, contextGuid);
                Contexts.Add(contextGuid, interned);
            }
            return interned;
        }
        public ProcessProxy GetContextProxy(Guid guid)
        {
            ProcessProxy proxy = null;
            Contexts.TryGetValue(guid, out proxy);
            return proxy;
        }
    }
}
