using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile
{
    public class TranspilerState
    {
        public Stmt Stmt;
        public TranspilerState(Stmt statement)
        {
            Stmt = statement;
        }
    }
}
