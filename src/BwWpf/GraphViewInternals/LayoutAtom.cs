using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Wpf.GraphViewInternals
{
    public class LayoutAtom
    {
        public TreeLayout Tree;
        public object Element;
        public bool Collapsed;
        //
        public virtual void Toggle()
        {
            Collapsed = !Collapsed;
        }
    }
}
