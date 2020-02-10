using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    public class BrainDefVisit<T, N> : ClassStmtVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : BrainDef
    {
        protected override void WriteHeader(N n)
        {
            t.WriteLine("[Brain(\"{0}\")]", n.Name);
            base.WriteHeader(n);
        }
    }
    public class BrainStubDefVisit<T, N> : ExpertStubDefVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : ExpertStubDef
    {
        public override void DoVisit(N n)
        {
            t.WriteLine("{0} _bwxExpert = bwxExpert as {0};", n.Parent.Parent.Name);
            t.WriteLine("bwxProcess.PushExpert(_bwxExpert);", n.Parent.Parent.Name);
            t.WriteLine("MentalTask bwxTask = new Method(bwxProcess, bwxMsg);");
            t.WriteLine("bwxProcess.ScheduleTask(bwxTask, _bwxExpert.{0}({1}));", n.Name + "Proc", n.TaskDef.CallString);
        }
    }
}
