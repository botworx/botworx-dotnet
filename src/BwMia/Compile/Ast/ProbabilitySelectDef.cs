using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class ProbabilitySelectDef : StmtBlock
    {
        public ProbabilitySelectDef() : base(AstNodeKind.ProbabilitySelectDef) { }
    }
    //
    public class ProbabilityCaseDef : AstNode
    {
        public ProbabilityCaseDef(Token token)
            : base(AstNodeKind.ProbabilityCaseDef, token)
        {
        }
    }
}
