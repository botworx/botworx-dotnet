using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class LhsCondStmt : CondStmt
    {
        //
        public LhsCondStmt()
            : base(AstNodeKind.LhsCondStmt)
        {
        }
    }
}
