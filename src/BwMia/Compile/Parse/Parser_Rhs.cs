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
        void ParseRhsGroup(LhsDef parent)
        {
            do
            {
                ParseRhsGroupItem(parent);
            } while (CurrentToken != null);
        }
        void ParseRhsGroupItem(LhsDef parent)
        {
            RhsDef rhsDef = new RhsDef();
            switch (CurrentList[0].Kind)
            {
                case TokenKind.LongSkinnyArrow:
                    Advance();//past (-->)
                    parent.SuccessRhs = rhsDef;
                    break;
                case TokenKind.NotLongSkinnyArrow:
                    Advance();//past (!-->)
                    parent.FailureRhs = rhsDef;
                    break;
                case TokenKind.LongFatArrow:
                    Advance();//past (==>)
                    parent.TotalSuccessRhs = rhsDef;
                    break;
                case TokenKind.NotLongFatArrow:
                    Advance();//past (!==>)
                    parent.TotalFailureRhs = rhsDef;
                    break;
                default:
                    throw new Exception();
            }
            do
            {
                CreateState(CurrentList);
                ParseRhsItem(rhsDef);
                PopState();
            } while (Advance() && !CurrentList[0].IsArrow);
        }
        void ParseRhs(AstNode parent)
        {
            RhsDef rhsDef = new RhsDef();
            ParseRhsItems(rhsDef);
            parent.AddChild(rhsDef);
        }
        void ParseRhsItems(AstNode parent)
        {
            PushNode(parent);
            do
            {
                CreateState(CurrentList);
                ParseRhsItem(parent);
                PopState();
            } while (Advance() && !CurrentList[0].IsArrow);
            PopNode();
        }
        void ParseRhsItem(AstNode parent)
        {
            PushNode(parent);
            Token token = CurrentToken;
            switch (token.Kind)
            {
                case TokenKind.Snippet:
                    ParseSnippetEffect(parent);
                    break;
                case TokenKind.Succeed:
                    ParseSucceed(parent);
                    break;
                case TokenKind.Fail:
                    ParseFail(parent);
                    break;
                case TokenKind.Throw:
                    ParseThrow(parent);
                    break;
                case TokenKind.Halt:
                    ParseHalt(parent);
                    break;
                case TokenKind.Return:
                    ParseReturn(parent);
                    break;
                case TokenKind.SquareList:
                    ParseContextDecorator(parent);
                    break;
                case TokenKind.Post:
                    ParsePost(token);
                    break;
                case TokenKind.Propose:
                    ParsePropose(token);
                    break;
                default:
                    ParseImplicitPost();
                    break;
            }
            PopNode();
        }
        void ParseSnippetEffect(AstNode parent)
        {
            SnippetEffect def = new SnippetEffect(CurrentToken);
            parent.AddChild(def);
        }
        void ParseSucceed(AstNode parent)
        {
            Advance(); //past 'succeed
            SucceedDef def = new SucceedDef();
            parent.AddChild(def);
        }
        void ParseFail(AstNode parent)
        {
            Advance(); //past 'fail
            FailDef def = new FailDef();
            parent.AddChild(def);
        }
        void ParseThrow(AstNode parent)
        {
            Advance(); //past 'fail
            ThrowDef def = new ThrowDef();
            parent.AddChild(def);
        }
        void ParseHalt(AstNode parent)
        {
            Advance(); //past 'halt
            HaltDef def = new HaltDef();
            parent.AddChild(def);
        }
        void ParseReturn(AstNode parent)
        {
            Advance(); //past 'return
            ReturnDef def = new ReturnDef();
            parent.AddChild(def);
        }
        void ParseContextDecorator(AstNode parent)
        {
            ContextDecoratorDef contextDecDef = new ContextDecoratorDef();
            CreateState(CurrentList);
            ContextDef contextDef = new ContextDef(CurrentToken);
            contextDecDef.ContextDef = contextDef;
            switch (CurrentToken.Kind)
            {
                case TokenKind.PlusPlus:
                    contextDef.ContextDefKind = ContextDefKind.Next;
                    break;
            }
            PopState();
            Advance(); //past [..]
            ParseRhsItem(contextDecDef);
            parent.AddChild(contextDecDef);
        }
    }
}
