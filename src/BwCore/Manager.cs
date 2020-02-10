using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx
{
    public abstract class PartManager : Part
    {
        public List<Part> Managed = new List<Part>();
        //
        public override PartManager Manager { get { return PartSystem.Instance; } }
        //
        public virtual void Manage(Part component)
        {
            Managed.Add(component);
        }
        public virtual void Unmanage(Part component)
        {
            Managed.Remove(component);
        }
    }
    public class ManagerT<T> : PartManager where T : Part, new()
    {
    }
    public class PartSystem : PartManager
    {
        public static PartSystem Instance = new PartSystem();
        //
        Dictionary<string, PartManager> Managers = new Dictionary<string, PartManager>();
        public PartManager GetManager(string name)
        {
            return Managers[name];
        }
        public override PartManager Manager { get { return null; } }
        void AddManager(string name, PartManager manager)
        {
            manager.Initialize(this);
            Managers.Add(name, manager);
        }
        //
        public override void Initialize(Part parent, Settings settings, Factory factory)
        {
            base.Initialize(parent, settings, factory);
            AddManager("Factory", FactoryManager.Instance);
        }
    }
}
