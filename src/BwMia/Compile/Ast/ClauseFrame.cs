using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class ClauseFrame : Frame<Expression, SlotKind>
    {
        public ClauseFrame()
        {
            for (int i = 0; i < KindLength; ++i)
                //Slots[i].Ref = Definition.Nil;
                Slots[i] = new Slot<Expression>(Expression.Nil);
        }
    }
    //public class ClauseSlot : 
}
