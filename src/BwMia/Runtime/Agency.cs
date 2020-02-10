using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;

namespace Botworx.Mia.Runtime
{
    public class Agency
    {
        public static readonly Agency Instance = new Agency();
        //
        public Guid Guid = Guid.NewGuid();
        public Dictionary<string, Type> BrainClasses = new Dictionary<string, Type>();
        public List<string> BrainFactoryNames = new List<string>();
        //
        public void AddBrainClass(Type type)
        {
            BrainFactoryNames.Add(type.Name);
            BrainClasses.Add(type.Name, type);
        }
        public List<string> GetBrainClassNames()
        {
            return BrainFactoryNames;
        }
        public void RegisterAssembly(string assemblyName)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            var types = from type in assembly.GetTypes()
                        where Attribute.IsDefined(type, typeof(BrainAttribute))
            select type;
            //
            foreach (var type in types)
            {
                AddBrainClass(type);
            }
        }
        public Agent CreateAgent(string brainClassName)
        {
            Type brainClass = null;
            BrainClasses.TryGetValue(brainClassName, out brainClass);
            Brain brain = Activator.CreateInstance(brainClass) as Brain;
            Agent agent = new Agent(brain);
            return agent;
        }
    }
}
