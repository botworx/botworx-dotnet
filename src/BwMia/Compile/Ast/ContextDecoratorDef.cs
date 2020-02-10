using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class ContextDecoratorDef : DecoratorDef
    {
        public ContextDef ContextDef;
        //
        public ContextDecoratorDef() : base(AstNodeKind.ContextDecoratorDef) { }
    }
}
