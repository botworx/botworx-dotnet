using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.AgentLib.ClientLib
{
    public class ProcessProxy : Proxy<IProcessService>
    {
        public ProcessProxy Parent;
        public string Label;
        //
        public ProcessProxy(Client client, IProcessService outer, Guid guid) : base(client, outer, guid) { }
        //
        public List<string> GetClauses()
        {
            return Outer.GetClauses(Guid);
        }
    }
}
