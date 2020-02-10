using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Runtime
{
    public class Atom : IEnumerable<KeyValuePair<Atom, object>>
    {
        public AtomFlag AtomFlags;
        public AtomType AtomType;
        //
        public Dictionary<Atom, object> Properties = new Dictionary<Atom, object>();
        public dynamic Value;
        public Context InnerContext;
        //
        protected Atom()
        {
        }
        //
        public bool TypeCheck(AtomType type)
        {
            if (type.IsIntegral && (AtomType.MemberFlags & type.MemberFlags) == type.MemberFlags)
                return true;
            if (AtomType == type)
                return true;
            //else
            return false;
        }
        //Property Support
        public object this[Atom pred]
        {
            get{
                object value = null;
                if (!Properties.TryGetValue(pred, out value))
                    return null;
                return Properties[pred]; 
            }
            set{ Properties[pred] = value; }
        }
        public void Add(Atom key, object value)
        {
            Properties.Add(key, value);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Properties.GetEnumerator();
        }
        public IEnumerator<KeyValuePair<Atom, object>> GetEnumerator()
        {
            return Properties.GetEnumerator();
        }
        //
        //Query Support
        public bool Exists<T>(Atom pred, T value)
        {
            object obj = null;
            if (!Properties.TryGetValue(pred, out obj))
                return false;
            //else
            return obj.Equals(value);
        }
        public bool Exists<T>(Atom pred, Predicate<T> predicate)
        {
            object obj = null;
            if (!Properties.TryGetValue(pred, out obj))
                return false;
            //else
            return predicate((T)obj);
        }
    }
}