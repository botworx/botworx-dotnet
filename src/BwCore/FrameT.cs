using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

namespace Botworx
{
    public class Frame<TRef, TKind> : Frame
        where TKind : struct  // C# does not allow enum constraint
    {
        public static Singleton<MetaEnum<TKind>> Kinds;
        public static int KindLength = Kinds.Value.Length;
        public Slots<TRef> Slots = new Slots<TRef>();
        //
        public Frame()
        {
            /*for (int i = 0; i < KindLength; ++i)
                Debug.WriteLine(i);*/
        }
        //
        public Slot<TRef> this[TKind kind]
        {
            get
            {
                return Slots[(int)(ValueType)kind]; // C# does not allow enum constraint, hence cast to ValueType
            }
            set
            {
                int ndx = (int)(ValueType)kind;
                Slots[ndx] = value;
            }
        }
        //
        protected virtual TRef Resolve(TRef _ref)
        {
            return default(TRef);
        }
    }
    public class Slots<TRef> : List<Slot<TRef>>
    {
    }
    public struct Slot<TRef>
    {
        public TRef Ref;
        //
        public Slot(TRef _ref = default(TRef))
        {
            Ref = _ref;
        }
    }
}
