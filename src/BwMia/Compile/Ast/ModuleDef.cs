using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    /*public class ModuleDef : ExpertDef
    {
        public ModuleDef(Token name, Token baseName)
            : base(name, baseName)
        {
            Kind = NodeKind.Module;
        }
    }
    public class ModuleStubDef : ExpertStubDef
    {
        public ModuleStubDef(Token name)
            : base(name)
        {
        }
    }
    public class ModuleStubDefVisit<T, N> : ExpertStubDefVisit<T, N>
        where T : Transpiler, IVisitor
        where N : ModuleStubDef
    {
        public override void DoVisit(N n)
        {
            t.WriteLine("{0} _bwxExpert = bwxExpert as {0};", n.Parent.Parent.Name);
            t.WriteLine("bwxProcess.PushExpert(_bwxExpert);", n.Parent.Parent.Name);
            t.WriteLine("MentalTask bwxTask = new Method(bwxProcess, bwxMsg);");
            t.WriteLine("bwxProcess.ScheduleTask(bwxTask, _bwxExpert.{0}({1}));", n.Name + "Proc", n.TaskDef.CallString);
        }
    }*/
}
