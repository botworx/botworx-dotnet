using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;
using Botworx.Mia.Runtime;

namespace Botworx.Mia.Compile.Parse
{
    public partial class Parser
    {
        internal void ParseMethod(Token tok)
        {
            Advance(); //past 'method
            Token nameToken = CurrentToken;
            Advance(); //past name
            //
            ExpertDef parent = (ExpertDef)CurrentNode;
            TaskDef methodDef = parent.CreateTaskDef(nameToken, AstNodeKind.TaskDef);
            //
            ParseTrigger(methodDef);
            ParseProperties(methodDef);
            //
            if (CurrentToken == null)
                return;
            //else
            PushTaskDef(methodDef);
            PushNode(methodDef.ProcedureDef);
            do
            {
                CreateState(CurrentList);
                ParseMethodItem(methodDef);
                PopState();
            } while (Advance());
            PopNode();
            PopTaskDef();
        }
        void ParseTrigger(TaskDef taskDef)
        {
            TriggerStmt triggerDecl = new TriggerStmt(taskDef);

            PushNode(triggerDecl);
            ExprSeq exprSeq = ParseExprSeq();
            PopNode();
            //
            triggerDecl.ExprSeq = exprSeq;
            taskDef.TriggerDef = triggerDecl;
            bool isCreator = taskDef.IsCreator;
            taskDef.Parent.AddTriggerDef(taskDef.TriggerDef, isCreator);
        }
        protected void ParseProperties(AstNode parent)
        {
            do
            {
                ParseProperty(parent);
            } while (CurrentToken != null && CurrentToken.IsProperty);
        }
        void ParseProperty(AstNode parent)
        {
            if (CurrentToken == null || !CurrentToken.IsProperty)
                return;
            NodeProperty property = new NodeProperty();
            property.Predicate = CurrentToken;
            Advance(); //past 'Property Name
            //else
            if (CurrentToken != null && !CurrentToken.IsLineList && !CurrentToken.IsProperty)
            {
                property.Object = CurrentToken;
                //Advance(); //past 'Property Value
            }
            //
            parent.AddProperty(property);
        }
        void ParseMethodItem(TaskDef parent)
        {
            switch (CurrentToken.Kind)
            {
                case TokenKind.Precondition:
                    ParsePrecondition(parent.ProcedureDef);
                    break;
                default:
                    ParseTaskItem(parent.ProcedureDef);
                    break;
            }
        }
        void ParseTaskItems(AstNode parent)
        {
            do
            {
                CreateState(CurrentList);
                ParseTaskItem(parent);
                PopState();
            } while (Advance());
        }
        void ParseTaskItem(AstNode parent)
        {
            switch (CurrentToken.Kind)
            {
                case TokenKind.Context:
                    ParseContext(parent);
                    break;
                case TokenKind.Do:
                    ParseDo(parent);
                    break;
                case TokenKind.Parallel:
                    ParseParallel(parent);
                    break;
                case TokenKind.Select:
                    ParseSelectStmt(parent);
                    break;
                case TokenKind.Property:
                    ParseProperty(parent);
                    break;
                case TokenKind.Where:
                    ParseWhere(parent);
                    break;
                case TokenKind.Precondition:
                    ParsePrecondition(parent);
                    break;
                default:
                    ParseRhsItem(parent);
                    break;
            }
        }
        void ParseDo(AstNode parent)
        {
            Advance();//past name
            DoStmt def = new DoStmt();
            ParseProperties(def);
            ParseTaskItems(def);
            parent.AddChild(def);
        }
        void ParseParallel(AstNode parent)
        {
            Advance();//past name
            ParallelDef def = new ParallelDef();
            ParseTaskItems(def);
            parent.AddChild(def);
        }
        void ParseSelectStmt(AstNode parent)
        {
            Advance();//past name
            SelectStmt def = new SelectStmt();
            ParseSelectStmtItems(def);
            parent.AddChild(def);
        }
        void ParseSelectStmtItems(AstNode parent)
        {
            do
            {
                CreateState(CurrentList);
                ParseSelectStmtItem(parent);
                PopState();
            } while (Advance());
        }
        void ParseSelectStmtItem(AstNode parent)
        {
            switch (CurrentToken.Kind)
            {
                case TokenKind.Case:
                    ParseCase(parent);
                    break;
            }
        }
        void ParseCase(AstNode parent)
        {
            Advance(); //past name
            CaseStmt def = new CaseStmt(CurrentToken);
            //
            PushNode(def);
            ExprSeq expr = ParseExprSeq();
            PopNode();

            def.ExprSeq = expr;
            parent.AddChild(def);

            ParseRhs(def);
        }
        void ParseProbabilitySelect(AstNode parent)
        {
            Advance();//past name
            ProbabilitySelectDef def = new ProbabilitySelectDef();
            ParseTaskItems(def);
            parent.AddChild(def);
        }
    }
}
