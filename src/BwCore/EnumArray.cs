using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx
{
    public class EnumArray<T, E> where E : struct
    {
        private T[] _array;
        private Func<E, int> _convert;

        public EnumArray(int size, Func<E, int> convert)
        {
            this._array = new T[size];
            this._convert = convert;
        }

        public T this[E index]
        {
            get { return this._array[this._convert(index)]; }
            set { this._array[this._convert(index)] = value; }
        }
    }
}
