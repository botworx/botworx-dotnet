using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Runtime
{
    public class Agent
    {
        public Guid Guid;
        public Brain Brain;
        //
        public Agent(Brain brain)
        {
            Guid = Guid.NewGuid();
            Brain = brain;
        }
    }
}
