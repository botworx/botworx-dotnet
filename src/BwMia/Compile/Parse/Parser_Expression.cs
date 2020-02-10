//#define DEBUG_EXPRESSION_PARSING

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Parse
{
    public partial class Parser
    {
        public ExprSeq ParseExprSeq(ParseFlag flags = ParseFlag.None)
        {
            CurrentState.ParseFlags |= flags;
            //
            ExprSeq = new ExprSeq();
            Expression expr = null;

            if (CurrentToken.Kind == TokenKind.RoundList)
            {
                if (CurrentList.Count != 0)
                {
                    CreateState(CurrentList);
                    expr = ParseExpression();
                    PopState();
                }
                Advance();
            }
            else
                expr = ParseExpression();
            if (expr == null)
                return null;
            if (!(expr is ClauseExpr))
            {
                //Debug.Print(expr.ToString());
                ExprSeq.AddChild(expr);
            }

            return ExprSeq;
        }
        public Expression ParseExpression()
        {
            ShuntQueue.Clear();
            ShuntStack.Clear();
            ShuntExpression();
            //
            Expression expression = Eval(ShuntQueue);
            return expression;
        }
        void ShuntExpression()
        {
            ParseOperand();
            //
            while (ShuntStack.Count != 0){
                Token op = ShuntStack.Pop();
                if (op.Kind != TokenKind.LeftRound)
                    ShuntQueue.Add(op);
            }
            while (ShuntStack2.Count != 0)
            {
                Token op = ShuntStack2.Pop();
                ShuntQueue.Add(op);
            }
#if DEBUG_EXPRESSION_PARSING
            Debug.WriteLine("ShuntQueue Dump");
            Debug.WriteLine("----------------");
            foreach (var op in ShuntQueue)
            {
                Debug.Write(op.ToString());
                Debug.Write("\t\t\t");
                Debug.Write(op.Kind);
                Debug.Write(" : ");
                Debug.WriteLine(op.Kind);
            }
            Debug.WriteLine("");
#endif
        }
        void ParseOperator()
        {
            Token tok = CurrentToken;
            if (tok == null)
                return;
            if (tok.Kind == TokenKind.LineList && !CurrentState.ParseFlags.HasFlag(ParseFlag.ParseLines))
                return;
            //else

            if(tok.MaybePredicate) //Handles (...) predicate
                tok = new Token(tok, TokenKind.Predicate);
            else if (tok.MaybeType) //Handles ':TypeName'
                tok = new Token(tok, TokenKind.Type);

            PushOperator(tok);//Push Binary Operator or Property
            Advance();
            ParseOperand();
        }
        void ParseOperand()
        {
            Token tok = CurrentToken;
            //Neither of these are hit!
            if (tok == null)
                return;
            if (tok.Kind == TokenKind.LineList && !CurrentState.ParseFlags.HasFlag(ParseFlag.ParseLines))
                return;
            //
            Token secondTok;
            Token thirdTok;
            bool isNext = Advance(out secondTok);
            if (tok.IsOperator) //Prefix operator
            {
                ParseOperand();
                PushOperator(tok);
            }
            else if (tok.IsType)
            {
                PushOperator(tok);
                ParseOperand();
            }
            else if (tok.IsExpressionList)
            {
                CreateState(tok.Value as TokenList);
                ShuntExpression();
                ParserState state = PopState();

                ShuntQueue.AddRange(state.ShuntQueue);
                ParseOperator();
            }
            else if (isNext && secondTok.IsConstituent)
            {
                CurrentState.ParseFlags |= ParseFlag.ParsingClause;
                PushOperator(TokenInstance.Mia.LeftRound);
                isNext = Advance(out thirdTok);

                if (isNext && thirdTok.IsConstituent) //Long clause
                {
                    QueueOperator(tok);
                    QueueOperator(thirdTok);
                    QueueOperator(new Token(secondTok, TokenKind.Predicate));
                    Advance();
                    ParseOperator();
                }
                else //Short clause
                {
                    //QueueOperator(Token.Nil);
                    //QueueOperator(TokenInstance.CSharp._BWXSUBJECT);
                    QueueOperator(TokenInstance.SubjectMacro);
                    QueueOperator(secondTok);
                    QueueOperator(new Token(tok, TokenKind.Predicate));
                    ParseOperator();
                }
            }
            else
            {
                if (tok.MaybePredicate) //Predicate, no subject or object
                {
                    //QueueOperator(Token.Nil);
                    QueueOperator(TokenInstance.SubjectMacro);
                    QueueOperator(Token.Nil);
                    QueueOperator(new Token(tok, TokenKind.Predicate));
                    ParseOperator();
                }
                else //Subject only
                {
                    QueueOperator(tok);
                    /*QueueOperator(tok);
                    QueueOperator(Token.Nil);
                    QueueOperator(TokenInstance.Fragment);*/
                    ParseOperator();
                }
            }
        }
        //
        //
        //Util
        void QueueOperator(Token tok)
        {
            /*if (tok.IsNil)
                System.Diagnostics.Debugger.Break();*/
            ShuntQueue.Add(tok);
        }
        void PushOperator(Token op)
        {
            /*if (op.IsNil)
                System.Diagnostics.Debugger.Break();*/

            if (op.Kind == TokenKind.LeftRound)
            {
                ShuntStack.Push(op);
                return;
            }
            if (CurrentState.ParseFlags.HasFlag(ParseFlag.ParsingClause))
            {
                if (op.IsProperty || op.IsOperator)
                {
                    bool popParen = false;
                    while (ShuntStack.Count != 0)
                    {
                        Token op2 = ShuntStack.Peek();
                        if (op2.Kind == TokenKind.LeftRound)
                            break;
                        //else
                        ShuntStack.Pop();
                        ShuntQueue.Add(op2);
                    }
                    if (popParen && op.Kind != TokenKind.Property)
                    {
                        ShuntStack.Pop();
                        CurrentState.ParseFlags &= ~ParseFlag.ParsingClause;
                    }
                }
            }
            while (ShuntStack.Count != 0)
            {
                int p = CurrentDialect.GetPrec(op);
                int a = CurrentDialect.GetAssoc(op);
                Token op2 = ShuntStack.Peek();
                int p2 = CurrentDialect.GetPrec(op2);
                int a2 = CurrentDialect.GetAssoc(op2);
                if ((a == 0 && p >= p2) || (p > p2))
                {
                    ShuntStack.Pop();
                    ShuntQueue.Add(op2);
                }
                else
                    break;
            }
            ShuntStack.Push(op);
        }
        //
    }
}
