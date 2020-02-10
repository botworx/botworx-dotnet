using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Runtime
{
    public class Entity : Atom
    {
        public string Name;
        //
        public Entity(string name)
        {
            Name = name;
        }
        public Entity(string name, AtomType type)
        {
            Name = name;
            AtomType = type;
        }
        public Entity(string name, object obj)
        {
            Name = name;
            Value = obj;
        }
        //
        public override string ToString()
        {
            return Name;
        }
    }
}
