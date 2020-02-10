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

    public Property<string> Status { get; private set; }
}
 */
namespace Botworx
{
    public struct Property<T> : INotifyPropertyChanged
    {
        T _value;

        public Property(T value)
        {
            _value = value;
            Changed = null;
            PropertyChanged = null;
        }

        public event EventHandler Changed;

        public event PropertyChangedEventHandler PropertyChanged;

        public T Value
        {
            get { return _value; }
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

        public static implicit operator T(Property<T> property)
        {
            return property.Value;
        }
    }
}
