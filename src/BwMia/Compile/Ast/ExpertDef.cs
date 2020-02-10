using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class ExpertDef : ClassDef
    {
        public List<TriggerStmt> TriggerDefs = new List<TriggerStmt>();
        //
        public ExpertDef(Token name, Token baseName)
            : base(name, baseName)
        {
            NodeKind = AstNodeKind.ExpertDef;
            AddChild(new ExpertConstructorDef(name));
        }
        public override void AddTriggerDef(TriggerStmt triggerDef, bool isCreator)
        {
            if (isCreator && Parent != null)
            {
                Parent.AddTriggerDef(triggerDef, false);
                return;
            }
            //else
            TriggerDefs.Add(triggerDef);
        }
        public TaskDef CreateTaskDef(Token token, AstNodeKind kind)
        {
            if (token.ToString() == Name)
                kind = AstNodeKind.CreatorDef;

            switch (kind)
            {
                case AstNodeKind.CreatorDef:
                    token = TokenInstance.CSharp.CREATE;
                    break;
            }
            //Create StubDef
            TaskStubDef stubDef;
            switch (kind)
            {
                case AstNodeKind.CreatorDef:
                    stubDef = new ExpertStubDef(token);
                    break;
                default:
                    stubDef = new TaskStubDef(token);
                    break;
            }
            //CreateProcDef
            TaskProcedureDef procDef = new TaskProcedureDef(new Token(token.ToString() + "Proc"));

            TaskDef taskDef = TaskDef.Create(this, kind, token, stubDef, procDef);

            return taskDef;
        }
    }
    public class ExpertStubDef : TaskStubDef
    {
        public ExpertStubDef(Token name)
            : base(AstNodeKind.ExpertStubDef, name)
        {
        }
    }
    //
    public class ExpertConstructorDef : ConstructorDef
    {
        public ExpertConstructorDef(Token name)
            : base(AstNodeKind.ExpertConstructorDef, name)
        {
        }
    }
}
