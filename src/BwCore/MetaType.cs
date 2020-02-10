using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx
{
    public class MetaType
    {
    }
    public class MetaType<T>
    {
    }
    public class MetaEnum<T> : MetaType<T>
        where T : struct
    {
        public int Length { get { return Enum.GetNames(typeof(T)).Length; } }
    }
}
