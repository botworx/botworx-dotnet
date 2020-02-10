using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//TODO:This doesn't fit here ...
using Botworx.Mia.Compile.Transpile;

namespace Botworx.Mia.Compile.Ast
{
    public class TaskDef : AstNode
    {
        public ExpertDef ExpertDef { get { return (ExpertDef)Parent; } }
        public TriggerStmt TriggerDef;
        public TaskStubDef StubDef;
        public TaskProcedureDef ProcedureDef;
        public List<Var> Parameters = new List<Var>();
        public string ParameterString;
        public string CallString;
        //
        private int SubtaskCount;
        //
        public bool IsCreator { get { return NodeKind == AstNodeKind.CreatorDef; } }
        public bool IsInnerTask { get { return NodeKind == AstNodeKind.InnerTaskDef; } }
        //
        public TaskDef(ExpertDef parent, AstNodeKind kind, Token token, TaskStubDef stubDef, TaskProcedureDef procDef)
            : base(kind, token)
        {
            Parent = parent;
            stubDef.Parent = procDef.Parent = this;
            StubDef = stubDef;
            ProcedureDef = procDef;
            Atomicity = Atomicity.StronglyNonAtomic;
        }
        public override void Resolve()
        {
            base.Resolve();
            if(TriggerDef != null)
                TriggerDef.Resolve();
        }
        public static TaskDef Create(ExpertDef parent, AstNodeKind kind, Token token, TaskStubDef stubDef, TaskProcedureDef procDef)
        {
            TaskDef def = new TaskDef(parent, kind, token, stubDef, procDef);

            if (kind != AstNodeKind.InnerTaskDef)
                def.AddChild(stubDef);
            def.AddChild(procDef);
            parent.AddChild(def);

            return def;
        }
        public TaskDef CreateSubtaskDef()
        {
            TaskDef taskDef = ExpertDef.CreateTaskDef(new Token(Token.ToString() + "_" + ++SubtaskCount), AstNodeKind.InnerTaskDef);
            return taskDef;
        }
        //TODO:!!!:These two belong in Transpiler or something!..
        public void InjectParameters(Scope scope)
        {
            foreach (var param in Parameters)
            {
                scope.AddVar(param);
            }
        }
        public string CreateParameterString(Transpiler t, bool isCall)
        {
            string result = "";
            if (isCall)
            {
                result = "MentalTask bwxTask";
            }
            else
            {
                if (IsInnerTask)
                    result = "__bwxTask";
                else
                    result = "bwxTask";
            }

            foreach (var param in Parameters)
            {
                result += ", ";
                if (isCall)
                    result += param.TypeToken + " ";
                result += param.Token.ToString();
            }
            return result;
        }
    }
}
