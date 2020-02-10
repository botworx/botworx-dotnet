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
        void ParsePost(Token token)
        {
            Advance(); //past 'post
            ParseImplicitPost();
        }
        void ParseImplicitPost()
        {
            ParseMessageEffect();
        }
        void ParsePropose(Token token)
        {
            Advance(); //past 'propose
            MessageEffect effect = ParseMessageEffect();
            effect.MessageTag.IsProposal = true;
        }
        MessageEffect ParseMessageEffect()
        {
            MessageEffect effect = new MessageEffect();

            PushNode(effect);
            ExprSeq expr = ParseExprSeq();
            PopNode();
            //TODO:!!!:Add new keyword support
            //ParseProperties(messageEffect);
            //parent.Add(effect);
            effect.ExprSeq = expr;
            PeekNode().AddChild(effect);
            return effect;
        }
        void ParseTaskMessage(ClauseExpr messageDef)
        {
            Advance(); //past 'task
            //
            TaskDef taskDef = CurrentTaskDef.CreateSubtaskDef();
            messageDef.MessageTag.TaskDef = taskDef;
            //
            ParseProperties(taskDef);
            //
            PushTaskDef(taskDef);
            ParseTaskItems(taskDef.ProcedureDef);
            PopTaskDef();
            //
            messageDef.MakeCallback();
        }
    }
}
