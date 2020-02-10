using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Runtime
{
    public class MentalTask : Task<Process>
    {
        public Message Message;
        public Context Context;
        //
        public MentalTask(Process process, Message message)
            : base(process)
        {
            Context = process.Context;
            Message = message;
        }
    }
}
