using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy.Lhs
{
    public class LhsPolicy : RootPolicy
    {
        public LhsPolicy()
        {
            //
            var visitTable = new[] {
                new NodeVisitInfo(AstNodeKind.Nil, new NilExprVisit<Transpiler, Expression>()),
                new NodeVisitInfo(AstNodeKind.ContextDecoratorDef, new ContextDecoratorDefVisit<Transpiler, ContextDecoratorDef>()),
                new NodeVisitInfo(AstNodeKind.ClauseExpr, new LhsClauseExprVisit<ClauseExpr>()),
                new NodeVisitInfo(AstNodeKind.NotEqualExpr, new LhsClauseExprVisit<BinaryOp>()),
                new NodeVisitInfo(AstNodeKind.EqualExpr, new LhsClauseExprVisit<BinaryOp>()),
                new NodeVisitInfo(AstNodeKind.TriggerDef, new TriggerStmtVisit<Transpiler, TriggerStmt>()),
                new NodeVisitInfo(AstNodeKind.TaskDef, new TaskDefVisit<Transpiler, TaskDef>()),
                new NodeVisitInfo(AstNodeKind.CreatorDef, new TaskDefVisit<Transpiler, TaskDef>()),
                new NodeVisitInfo(AstNodeKind.InnerTaskDef, new TaskDefVisit<Transpiler, TaskDef>()),
                new NodeVisitInfo(AstNodeKind.TaskStubDef, new TaskStubDefVisit<Transpiler, TaskStubDef>()),
                new NodeVisitInfo(AstNodeKind.ExpertStubDef, new ExpertStubDefVisit<Transpiler, ExpertStubDef>()),
                new NodeVisitInfo(AstNodeKind.TaskProcedureDef, new TaskProcedureDefVisit<Transpiler, TaskProcedureDef>()),
                new NodeVisitInfo(AstNodeKind.Lhs, new LhsVisit<Transpiler, LhsDef>()),
                new NodeVisitInfo(AstNodeKind.LhsCondStmt, new ClauseCondStmtVisit<Transpiler, LhsCondStmt>()),
                //new VisitInfo(NodeKind.HostCodeCondition, new SnippetCondStmtVisit<Transpiler, SnippetCondStmt>()),
                new NodeVisitInfo(AstNodeKind.Snippet, new LhsSnippetExprVisit<Transpiler, SnippetExpr>()),
                new NodeVisitInfo(AstNodeKind.Rhs, new RhsStmtVisit<Transpiler, RhsDef>()),
                new NodeVisitInfo(AstNodeKind.DoStmt, new BlockStmtVisit<Transpiler, DoStmt>()),
                new NodeVisitInfo(AstNodeKind.LiteralExpr, new LiteralExprVisit<Transpiler, LiteralExpr>()),
                new NodeVisitInfo(AstNodeKind.Name, new NameVisit<Transpiler, Name>())
            };
            MergeVisits(visitTable);
        }
    }
}
