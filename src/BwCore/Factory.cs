using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx
{
    public abstract class Factory : Part
    {
        public string name;
        public List<Factory> children = new List<Factory>();
        //
        public void AddChild(Factory factory)
        {
            children.Add(factory);
        }
        public static Factory Create(string name)
        {
            Factory factory = FactoryManager.Instance.Value.GetFactory(name);
            if (factory == null)
                factory = FactoryBuilder.Build(name);
            if (factory == null)
                factory = ReflectionCreate(name);
            return factory;
        }
        public static Factory ReflectionCreate(string productClassName)
        {
            return ReflectionCreate("Botworx", "Botworx", productClassName);
        }
        public static Factory ReflectionCreate(string assemblyName, string nameSpaceName, string productClassName)
        {
            Factory factory = null;
            string factoryClassName = null;
            try
            {
                factoryClassName = String.Format("{0}.{1}Factory", nameSpaceName, productClassName);
                factory = Activator.CreateInstance(assemblyName, factoryClassName).Unwrap() as Factory;
            }
            catch (TypeLoadException)
            {
                factoryClassName = String.Format("{0}.FactoryT`1[{0}.{1}]", nameSpaceName, productClassName);
                factory = Activator.CreateInstance(assemblyName, factoryClassName).Unwrap() as Factory;
                //Save the below.  It works.  Might come in handy later.
                /*Type classType = Type.GetType("Botworx.FactoryT`1");
                Type[] typeParams = new Type[] { Type.GetType("Botworx." + name) };
                Type constructedType = classType.MakeGenericType(typeParams);
                factory = Activator.CreateInstance(constructedType) as Factory;*/
            }
            return factory;
        }
        //
        public abstract Part Produce(Part parent);
        public abstract Part Produce(Part parent, Settings settings);
    }
    public class FactoryT<T> : Factory
        where T : Part, new()
    {
        public override Part Produce(Part parent)
        {
            return Produce(parent, null, this);
        }
        public override Part Produce(Part parent, Settings settings)
        {
            return Produce(parent, settings, this);
        }
        public T Produce(Part parent, Settings settings, Factory factory)
        {
            T part = new T();
            part.Initialize(parent, settings, factory);
            Configure(part, settings);
            if (factory == null)
                return part;
            //else
            foreach (Factory childFactory in factory.children)
            {
                Part child = childFactory.Produce(part);
                /*switch (childFactory.kind)
                {
                    case FactoryKind.Component:
                }*/
            }
            return part;
        }
        //
        public override PartManager Manager { get { return FactoryManager.Instance; } }
    }
    class FactoryManager : PartManager
    {
        public static Singleton<FactoryManager> Instance;
        public override void Initialize(Part parent, Settings settings, Factory factory)
        {
            base.Initialize(parent, settings, factory);
        }
        //
        Dictionary<string, Factory> Factories = new Dictionary<string, Factory>();
        public override void Manage(Part part)
        {
            base.Manage(part);
            Factory factory = part as Factory;
            AddFactory(factory.name, factory);
        }
        public override void Unmanage(Part component)
        {
            base.Unmanage(component);
        }
        void AddFactory(string name, Factory factory)
        {
            Factories.Add(name, factory);
        }
        public Factory GetFactory(string name)
        {
            try
            {
                return Factories[name];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
        public override PartManager Manager { get { return null; } }
    }
}
