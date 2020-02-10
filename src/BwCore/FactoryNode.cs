using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx
{
    class FactoryNode
    {
        public string name;
        public Settings settings = new Settings();
        public List<FactoryNode> components = new List<FactoryNode>();
        public List<FactoryNode> children = new List<FactoryNode>();
        //
        public void AddSetting(string name, object value)
        {
            settings[name] = value;
        }
        public void AddComponent(FactoryNode node)
        {
            components.Add(node);
        }
        public void AddChild(FactoryNode node)
        {
            children.Add(node);
        }
    }
}
