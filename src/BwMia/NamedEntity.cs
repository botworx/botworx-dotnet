using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia
{
    public class NamedEntity : Atom
    {
        //
        public string Name;
        //
        public override string ToString()
        {
            return Name;
        }
        //
        public NamedEntity(string name)
        {
            Name = name;
        }
        public NamedEntity(string name, AtomType type)
        {
            Name = name;
            Type = type;
        }
        public NamedEntity(string name, object obj)
        {
            Name = name;
            Dynamic = obj;
        }
    }
    sealed public class EntityManager
    {
        Dictionary<string, Atom> Dictionary = new Dictionary<string, Atom>();
        //
        private static readonly EntityManager instance = new EntityManager();

        private EntityManager() { }

        public static EntityManager Instance
        {
            get
            {
                return instance;
            }
        }
        public Atom Intern(string name)
        {
            Atom value;
            if (!Dictionary.TryGetValue(name, out value))
            {
                value = new NamedEntity(name);
                Dictionary.Add(name, value);
            }
            return value;
        }
    }
}
