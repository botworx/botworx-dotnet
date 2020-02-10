using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class SelectStmt : StmtBlock
    {
        public SelectStmt() : base(AstNodeKind.SelectStmt) { }
    }
    public class CaseStmt : Stmt
    {
        public CaseStmt(Token token)
            : base(AstNodeKind.CaseStmt, token)
        {
        }
    }
}
