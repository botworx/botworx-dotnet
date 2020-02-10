using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

/*
 * Author: Don Knackman
 * Blog: http://spookycoding.blogspot.com/
 * Usage:
 * class ViewModel
{
    public ViewModel()
    {
        Status = new Property<string>("");
    }

    public static Singleton<MyClass> MyClass { get; private set; }
}
 */

namespace Botworx
{
    public struct Singleton<T> : INotifyPropertyChanged
        where T : new()
    {
        T _value;

        public Singleton(T value)
        {
            _value = value;
            Changed = null;
            PropertyChanged = null;
        }

        public event EventHandler Changed;

        public event PropertyChangedEventHandler PropertyChanged;

        public T Value
        {
            get
            {
                if (_value == null)
                    _value = new T();
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

        public static implicit operator T(Singleton<T> property)
        {
            return property.Value;
        }
    }
}
