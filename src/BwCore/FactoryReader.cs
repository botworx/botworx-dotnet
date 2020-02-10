using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Linq;

namespace Botworx
{
    class FactoryReader
    {
        public static FactoryNode Read(string filename)
        {
            FactoryReader reader = new FactoryReader();
            XDocument doc = XDocument.Load(filename);
            //
            XElement element = doc.Element("root");
            FactoryNode root = reader.ReadFactory(null, element);
            return root;
        }
        FactoryNode ReadFactory(FactoryNode parent, XElement element)
        {
            FactoryNode node = new FactoryNode();
            XAttribute nameAttr = element.Attribute("name");
            node.name = nameAttr.Value;
            ReadSettings(node, element);
            ReadComponents(node, element);
            ReadChildren(node, element);
            return node;
        }
        //
        void ReadSettings(FactoryNode node, XElement element)
        {
            XElement settingsAtom = element.Element("settings");
            if (settingsAtom == null)
                return;
            //else
            foreach (XElement settingAtom in settingsAtom.Elements())
            {
                ReadSetting(node, settingAtom);
            }
        }
        void ReadSetting(FactoryNode node, XElement element)
        {
            XAttribute nameAttr = element.Attribute("name");
            XAttribute valueAttr = element.Attribute("value");
            node.AddSetting(nameAttr.Value, valueAttr.Value);
        }
        //
        void ReadComponents(FactoryNode parent, XElement element)
        {
            XElement componentsAtom = element.Element("components");
            if (componentsAtom == null)
                return;
            //else
            foreach (XElement componentAtom in componentsAtom.Elements())
            {
                ReadComponent(parent, componentAtom);
            }
        }
        void ReadComponent(FactoryNode parent, XElement element)
        {
            FactoryNode node = ReadFactory(parent, element);
            parent.AddComponent(node);
        }
        //
        void ReadChildren(FactoryNode parent, XElement element)
        {
            XElement childrenAtom = element.Element("children");
            if (childrenAtom == null)
                return;
            //else
            foreach (XElement childAtom in childrenAtom.Elements())
            {
                ReadComponent(parent, childAtom);
            }
        }
        void ReadChild(FactoryNode parent, XElement element)
        {
            FactoryNode node = ReadFactory(parent, element);
            parent.AddChild(node);
        }
    }
}
