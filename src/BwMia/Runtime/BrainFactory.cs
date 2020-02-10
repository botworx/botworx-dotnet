using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Runtime
{
    public abstract class BrainFactory
    {
        public string Name;
        public abstract Brain Create();
    }
    public class BrainFactory<T> : BrainFactory where T: Brain, new()
    {
        public BrainFactory(string name)
        {
            Name = name;
        }
        public override Brain Create()
        {
            return new T();
        }
    }
}
