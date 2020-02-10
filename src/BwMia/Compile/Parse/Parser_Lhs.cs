using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Botworx.Mia.Compile.Ast;
using Botworx.Mia.Runtime;

namespace Botworx.Mia.Compile.Parse
{
    public partial class Parser
    {
        void ParsePrecondition(AstNode parent)
        {
            Advance();//past name
            Precondition def = new Precondition();
            do
            {
                CreateState(CurrentList);
                ParseCondition();
                PopState();
            } while (Advance());
            parent.AddChild(def);
        }
        void ParseWhere(AstNode parent)
        {
            Advance();//past name
            WhereDef def = new WhereDef();
            ParseLhs(def);
            parent.AddChild(def);
        }
        void ParseLhs(LhsDef def)
        {
            PushNode(def);
            do
            {
                CreateState(CurrentList);
                ParseCondition();
                PopState();
            } while (Advance() && !CurrentList[0].IsArrow);
            PopNode();
            //
            ParseRhsGroup(def);
        }
        void ParseCondition()
        {
            LhsCondStmt def = new LhsCondStmt();

            PushNode(def);
            ExprSeq expr = ParseExprSeq();
            PopNode();

            def.ExprSeq = expr;
            CurrentNode.AddChild(def);
        }

    }
}
