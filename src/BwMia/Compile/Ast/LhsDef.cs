using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class LhsDef : StmtBlock
    {
        public RhsDef SuccessRhs = null;
        public RhsDef FailureRhs = null;
        public RhsDef TotalSuccessRhs = null;
        public RhsDef TotalFailureRhs = null;
        //
        public LhsDef()
            : base(AstNodeKind.Lhs)
        {
        }
        public override void Resolve()
        {
            base.Resolve();
            if (SuccessRhs != null)
                SuccessRhs.Resolve();
            if (FailureRhs != null)
                FailureRhs.Resolve();
            if (TotalSuccessRhs != null)
                TotalSuccessRhs.Resolve();
            if (TotalFailureRhs != null)
                TotalFailureRhs.Resolve();
        }
        public CondStmt FirstCondition
        {
            get { return ((CondStmt)Children[0]); }
        }
        public CondStmt LastCondition
        {
            get { return ((CondStmt)Children[Children.Count - 1]); }
        }
        //
        public bool HasSuccessRhs
        {
            get { return SuccessRhs != null; }
        }
        public bool HasFailureRhs
        {
            get { return FailureRhs != null; }
        }
        public bool HasTotalSuccessRhs
        {
            get { return TotalSuccessRhs != null; }
        }
        public bool HasTotalFailureRhs
        {
            get { return TotalFailureRhs != null; }
        }
    }
}
