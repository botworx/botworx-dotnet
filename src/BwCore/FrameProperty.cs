using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

/*
 * Purpose:
 */

namespace Botworx
{
    public struct FrameProperty<T> : INotifyPropertyChanged
    {
        T _value;

        public FrameProperty(T value)
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

        public static implicit operator T(FrameProperty<T> property)
        {
            return property.Value;
        }
    }
}
