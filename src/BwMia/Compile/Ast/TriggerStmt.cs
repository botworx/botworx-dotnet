using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Runtime;

namespace Botworx.Mia.Compile.Ast
{
    public class TriggerStmt : Stmt
    {
        public TaskDef TaskDef;
        //
        public TriggerStmt(TaskDef taskDef)
            : base(AstNodeKind.TriggerDef)
        {
            TaskDef = taskDef;
        }
        public override void Resolve()
        {
            base.Resolve();
            if(ExprSeq != null)
                ExprSeq.Resolve();
            List<Var> vars = new List<Var>();
            if (ExprSeq != null)
                ExprSeq[0].CollectVariables(vars);
            TaskDef.Parameters = vars;
        }
    }
}
