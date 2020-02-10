using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

using Botworx;

namespace Botworx
{
    public struct FactorySingleton<T> : INotifyPropertyChanged
        where T : Factory, new()
    {
        T _value;
        string name;

        public FactorySingleton(T value)
        {
            _value = value;
            Changed = null;
            PropertyChanged = null;
            name = null;
        }

        public FactorySingleton(string name)
        {
            _value = null;
            Changed = null;
            PropertyChanged = null;
            this.name = name;
        }

        public event EventHandler Changed;

        public event PropertyChangedEventHandler PropertyChanged;

        public T Value
        {
            get
            {
                if (_value == null)
                {
                    if (name == null)
                        _value = new T();
                    else
                        _value = Factory.Create(name) as T;
                }
                return _value;
            }
            set
            {
                if ((value != null && !value.Equals(_value)) ||
                    (_value != null && !_value.Equals(value)))
                {
                    _value = value;
                    OnChanged();
                }
            }
        }
        public override string ToString()
        {
            return object.ReferenceEquals(_value, null) ? string.Empty : _value.ToString();
        }

        public void OnChanged()
        {
            if (Changed != null)
                Changed(this, EventArgs.Empty);

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Value"));
        }

        public static implicit operator T(FactorySingleton<T> property)
        {
            return property.Value;
        }
    }
}
