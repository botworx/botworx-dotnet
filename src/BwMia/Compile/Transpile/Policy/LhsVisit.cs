using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    public class LhsVisit<T, N> : BlockStmtVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : LhsDef
    {
        public override void BeginVisit(N n)
        {
            t.PushLhsPolicy();
            base.BeginVisit(n);
        }
        public override void EndVisit(N n)
        {
            base.EndVisit(n);
            t.PopPolicy();
        }
        public override void DoVisit(N n)
        {
            if (n.HasTotalSuccessRhs || n.HasTotalFailureRhs)
                t.WriteLine("ControlFlags _bwxQflags = ControlFlags.None;");

            if (n.HasTotalSuccessRhs || n.HasTotalFailureRhs)
                t.StartBlock(n);

            base.DoVisit(n);

            if (n.HasTotalSuccessRhs || n.HasTotalFailureRhs)
                t.WriteLine("_bwxQflags |= ControlFlags.PartialSuccess;");

            if (n.HasSuccessRhs)
                t.Visit(n.SuccessRhs);

            if (!n.LastCondition.IsIterator && (n.HasFailureRhs || n.HasTotalSuccessRhs || n.HasTotalFailureRhs))
            {
                t.EndBlock(n.LastCondition);
                t.WriteLine("else");
                t.StartBlock(n);
                if (n.HasTotalSuccessRhs || n.HasTotalFailureRhs)
                    t.WriteLine("_bwxQflags |= ControlFlags.PartialFailure;");
                if (n.HasFailureRhs)
                    t.Visit(n.FailureRhs);
                t.EndBlock(n);
            }

            if (n.HasTotalSuccessRhs || n.HasTotalFailureRhs)
            {
                t.EndBlock(n);
                if (n.NeedsInnerExit)
                    t.WriteLine(n.GenerateInnerExitLabel());
                //
                if (n.HasTotalSuccessRhs)
                {
                    t.WriteLine("if(_bwxQflags == ControlFlags.PartialSuccess)");
                    t.StartBlock(n);
                    t.Visit(n.TotalSuccessRhs);
                    t.EndBlock(n);
                }
                if (n.HasTotalFailureRhs)
                {
                    t.WriteLine("if(_bwxQflags == ControlFlags.None || _bwxQflags == ControlFlags.PartialFailure)");
                    t.StartBlock(n);
                    t.Visit(n.TotalFailureRhs);
                    t.EndBlock(n);
                }
            }
        }
    }
}
