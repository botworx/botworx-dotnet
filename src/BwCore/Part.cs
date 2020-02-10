using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Botworx
{
    public abstract class Part
    {
        protected Part Parent = null;
        List<Part> Components = new List<Part>();
        public virtual Accessor GetAccessor() { return null; }
        public abstract PartManager Manager { get; }
        public bool IsInitialized = false;
        public bool IsActive = false;
        //
        public virtual void Initialize()
        {
            Initialize(null, null, null);
        }
        public virtual void Initialize(Part parent)
        {
            Initialize(parent, null, null);
        }
        public virtual void Initialize(Part parent, Settings settings)
        {
            Initialize(parent, settings, null);
        }
        public virtual void Initialize(Part parent, Settings settings, Factory factory)
        {
            Parent = parent;
            IsInitialized = true;
            if(Manager != null)
                Manager.Manage(this);
            Activate();
        }
        public static void Configure(Part part, Settings settings)
        {
            if (settings == null)
                return;
            //else
            Accessor accessor = part.GetAccessor();
            if (accessor == null)
                return;
            //else
            foreach (KeyValuePair<string, object> setting in settings)
            {
                accessor.Set(part, setting);
            }
        }
        public void Activate()
        {
            if (IsActive)
                return;
            IsActive = true;
        }
        public void Deactivate()
        {
            if (!IsActive)
                return;
            IsActive = false;
        }
        public void AddComponent(Part component)
        {
            component.Parent = this;
            Components.Add(component);
        }
        public Part GetComponent(System.Type type)
        {
            foreach (Part component in Components)
            {
                //if (component.GetType() == type)
                if (type.IsAssignableFrom(component.GetType()))
                    return component;
            }
            //else
            return null;
        }
    }
    public class Settings : Dictionary<string, object>
    {
    }
    public delegate void Setter(Part part, object value);

    public class Accessor : Dictionary<string, Setter>
    {
        Accessor parent;
        public Accessor(Accessor parent)
        {
            this.parent = parent;
        }
        public void Set(Part part, KeyValuePair<string, object> setting)
        {
            Setter setter = this[setting.Key];
            setter(part, setting.Value);
        }
    }
}
