using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class FunctionDef : RhsDef
    {
        //public TaskDef TaskDef;
        public TaskDef TaskDef { get { return (TaskDef)Parent; } }
        //
        public FunctionDef(AstNodeKind kind) : this(kind, null)
        {
        }
        public FunctionDef(AstNodeKind kind, Token token)
            : base(kind, token)
        {
        }
    }
    public class ConstructorDef : FunctionDef
    {
        public ConstructorDef(AstNodeKind kind, Token token)
            : base(kind, token)
        {
        }
    }
    public class TaskFunctionDef : FunctionDef
    {
        public TaskFunctionDef(AstNodeKind kind, Token token)
            : base(kind, token)
        {
        }
    }
    public class TaskStubDef : TaskFunctionDef
    {
        public TaskStubDef(Token token)
            : base(AstNodeKind.TaskStubDef, token)
        {
        }
        public TaskStubDef(AstNodeKind kind, Token token)
            : base(kind, token)
        {
        }
    }

    public class TaskProcedureDef : TaskFunctionDef
    {
        public TaskProcedureDef(Token token)
            : base(AstNodeKind.TaskProcedureDef, token)
        {
        }
    }
}
