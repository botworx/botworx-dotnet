using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.AgentLib
{
    public class ServiceHostFactory
    {
        public Uri Uri;
        //
        public ServiceHostFactory(Uri uri)
        {
            Uri = uri;
        }
        public virtual void Create()
        {
        }
    }
}
