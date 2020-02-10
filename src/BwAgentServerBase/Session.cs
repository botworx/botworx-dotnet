using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.AgentLib.ServerLib
{
    public abstract class Session
    {
        public Service Service;
        public Guid Guid;
    }
    //NOTE:Unfortunately we can't template Outer like with proxy since callback interface must match
    public class Session<TInner> : Session
    {
        protected TInner Inner;
        protected IAgentCallback Outer;
        //
        protected Session(Service service, TInner inner, IAgentCallback outer, Guid guid)
        {
            Service = service;
            Inner = inner;
            Outer = outer;
            Guid = guid;
        }
    }
}
