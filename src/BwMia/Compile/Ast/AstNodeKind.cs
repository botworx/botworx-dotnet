﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public enum AstNodeKind
    {
        Nil,
        Name,
        TypeCheckDef,
        LiteralExpr,
        Block,
        Lhs,
        Rhs,
        SnippetEffect,
        MessageEffect,
        SucceedDef,
        FailDef,
        ThrowDef,
        HaltDef,
        ReturnDef,
        HostCodeCondition,
        UsingDef,
        LhsCondStmt,
        AtomTypeDef,
        EntityDef,
        PredicateDef,
        Var,
        ExprSeq,
        ClauseExpr,
        PropertyExpr,
        MessageDef,
        MessagePattern,
        DoStmt,
        ParallelDef,
        SelectStmt,
        ProbabilitySelectDef,
        TaskDef,
        CreatorDef,
        InnerTaskDef,
        TaskStubDef,
        ExpertStubDef,
        TaskProcedureDef,
        ObjectDef,
        ProbabilityCaseDef,
        ExpertConstructorDef,
        CaseStmt,
        Snippet,
        AtomDef,
        RootBlock,
        ExpertDef,
        NamespaceDef,
        ClassDef,
        ModuleDef,
        BrainDef,
        ActionDef,
        MethodDef,
        TriggerDef,
        TopicDef,
        ContextDef,
        ContextDecoratorDef,
        //Operators
        NotEqualExpr,
        EqualExpr
    }
    public enum Atomicity
    {
        WeaklyAtomic,
        StronglyAtomic,
        WeaklyNonAtomic,
        StronglyNonAtomic
    }
}
