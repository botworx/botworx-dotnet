using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    public class RootPolicy : TranspilerPolicy
    {
        public RootPolicy()
        {
            //
            var visitTable = new[] {
                new NodeVisitInfo(AstNodeKind.Nil, new NilExprVisit<Transpiler, Expression>()),
                new NodeVisitInfo(AstNodeKind.ExprSeq, new NodeVisit<Transpiler, ExprSeq>()),
                new NodeVisitInfo(AstNodeKind.RootBlock, new RootBlockVisit<Transpiler, RootBlock>()),
                new NodeVisitInfo(AstNodeKind.BrainDef, new BrainDefVisit<Transpiler, BrainDef>()),
                new NodeVisitInfo(AstNodeKind.NamespaceDef, new NamespaceStmtVisit<Transpiler, AstNode>()),
                new NodeVisitInfo(AstNodeKind.ClassDef, new ClassStmtVisit<Transpiler, ClassDef>()),
                new NodeVisitInfo(AstNodeKind.ExpertConstructorDef, new ExpertConstructorDefVisit<Transpiler, ExpertConstructorDef>()),
                new NodeVisitInfo(AstNodeKind.ExpertDef, new ClassStmtVisit<Transpiler, ClassDef>()),
                new NodeVisitInfo(AstNodeKind.UsingDef, new UsingStmtVisit<Transpiler, AstNode>()),
                new NodeVisitInfo(AstNodeKind.ContextDecoratorDef, new ContextDecoratorDefVisit<Transpiler, ContextDecoratorDef>()),
                new NodeVisitInfo(AstNodeKind.TopicDef, new TopicDefVisit<Transpiler, TopicDef>()),
                new NodeVisitInfo(AstNodeKind.ContextDef, new ContextDefVisit<Transpiler, ContextDef>()),
                new NodeVisitInfo(AstNodeKind.ClauseExpr, new ClauseExprVisit<ClauseExpr>()),
                new NodeVisitInfo(AstNodeKind.TriggerDef, new TriggerStmtVisit<Transpiler, TriggerStmt>()),
                new NodeVisitInfo(AstNodeKind.TaskDef, new TaskDefVisit<Transpiler, TaskDef>()),
                new NodeVisitInfo(AstNodeKind.CreatorDef, new TaskDefVisit<Transpiler, TaskDef>()),
                new NodeVisitInfo(AstNodeKind.InnerTaskDef, new TaskDefVisit<Transpiler, TaskDef>()),
                new NodeVisitInfo(AstNodeKind.TaskStubDef, new TaskStubDefVisit<Transpiler, TaskStubDef>()),
                new NodeVisitInfo(AstNodeKind.ExpertStubDef, new ExpertStubDefVisit<Transpiler, ExpertStubDef>()),
                new NodeVisitInfo(AstNodeKind.TaskProcedureDef, new TaskProcedureDefVisit<Transpiler, TaskProcedureDef>()),
                new NodeVisitInfo(AstNodeKind.Lhs, new LhsVisit<Transpiler, LhsDef>()),
                new NodeVisitInfo(AstNodeKind.Rhs, new RhsStmtVisit<Transpiler, RhsDef>()),
                new NodeVisitInfo(AstNodeKind.AtomTypeDef, new AtomTypeDefVisit<Transpiler, AtomTypeDef>()),
                new NodeVisitInfo(AstNodeKind.EntityDef, new EntityDefVisit<Transpiler, EntityDef>()),
                new NodeVisitInfo(AstNodeKind.PredicateDef, new PredicateDefVisit<Transpiler, PredicateDef>()),
                new NodeVisitInfo(AstNodeKind.DoStmt, new BlockStmtVisit<Transpiler, DoStmt>()),
                new NodeVisitInfo(AstNodeKind.LiteralExpr, new LiteralExprVisit<Transpiler, LiteralExpr>()),
                new NodeVisitInfo(AstNodeKind.Name, new NameVisit<Transpiler, Name>())
            };
            MergeVisits(visitTable);
        }
    }
}
