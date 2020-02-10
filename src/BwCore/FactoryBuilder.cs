using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using System.Reflection;

namespace Botworx
{
    class FactoryBuilder
    {
        public static Factory Build(string name)
        {
            string filename = "../../../Content/production/" + name + ".xml";
            if (!File.Exists(filename))
                return null;
            FactoryNode root = FactoryReader.Read(filename);
            return BuildFactory(null, root);
        }
        public static Factory BuildFactory(Factory parent, FactoryNode node)
        {
            Factory factory = Factory.ReflectionCreate(node.name);
            Part.Configure(factory, node.settings);
            BuildComponents(factory, node);
            return factory;
        }
        public static void BuildComponents(Factory parent, FactoryNode parentNode)
        {
            foreach (FactoryNode node in parentNode.components)
            {
                Factory factory = BuildFactory(parent, node);
                parent.AddChild(factory);
            }
        }
    }
}
