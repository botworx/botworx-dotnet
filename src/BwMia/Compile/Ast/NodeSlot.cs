using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class NodeSlotKey : Tuple<int, string>{
        public NodeSlotKey(int key1, string key2) : base(key1, key2)
        {
        }
    }
    public struct NodeSlot
    {
        public IExpression Ref;
        public INode Def;
        //
        public NodeSlot(IExpression _ref, INode def = null)
        {
            Ref = _ref;
            Def = def;
        }
    }
    public class NodeSlots : List<NodeSlot>
    {
    }
}
