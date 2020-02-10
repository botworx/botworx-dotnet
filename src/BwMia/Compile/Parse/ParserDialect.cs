using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Parse
{
    public class ParserDialect
    {
        public Parser Parser;
        public TokenInfo[] TokenInfoTable;
        //
        public ParserDialect(Parser parser)
        {
            Parser = parser;
            TokenInfoTable = (TokenInfo[])TokenInfo.Instances.Clone();
            for (int i = 0; i < TokenInfoTable.Length; ++i)
            {
                TokenInfo info = TokenInfoTable[i];
                info.Action = Parser.EvalError;
                info.Action2 = Parser.EvalError;
                TokenInfoTable[i] = info;
            }
            var tokOps = new[] {
                new TokenInfo(TokenKind.Namespace, Parser.ParseNamespace),
                new TokenInfo(TokenKind.Brain, Parser.ParseBrain),
                //
                new TokenInfo(TokenKind.PredicateKw, Parser.ParsePredicate),
                new TokenInfo(TokenKind.Expert, Parser.ParseExpert),
                new TokenInfo(TokenKind.Method, Parser.ParseMethod),
                //
                new TokenInfo(TokenKind.Nil, 255, 255, Parser.EvalNil),
                new TokenInfo(TokenKind.SubjectMacro, 255, 255, Parser.EvalSubjectMacro),
                new TokenInfo(TokenKind.LineList, 255, 255, Parser.EvalLineList),
                new TokenInfo(TokenKind.Boolean, 255, 255, Parser.EvalLiteral),
                new TokenInfo(TokenKind.Integer, 255, 255, Parser.EvalLiteral),
                new TokenInfo(TokenKind.Snippet,  15, 0, Parser.EvalSnippet),
                new TokenInfo(TokenKind.Name, 255, 255, Parser.EvalEntity),
                new TokenInfo(TokenKind.Variable, 255, 255, Parser.EvalVariable),
                new TokenInfo(TokenKind.Bang, 15, 0, Parser.EvalNegate),
                new TokenInfo(TokenKind.Plus, 15, 0, Parser.EvalAssert),
                new TokenInfo(TokenKind.Minus, 15, 0, Parser.EvalRetract),
                //new TokenInfo(TokenKind.ShortSkinnyArrow,  15, 0, EvalAssignRight),
                new TokenInfo(TokenKind.ShortSkinnyArrow,  14, 1, Parser.EvalAssignRight),
                new TokenInfo(TokenKind.NotEqual,  15, 0, Parser.EvalNotEqual),
                new TokenInfo(TokenKind.Equal,  15, 0, Parser.EvalEqual),
                new TokenInfo(TokenKind.At,  2, 1, Parser.EvalAttempt),
                new TokenInfo(TokenKind.ForwardSlash, 2, 1, Parser.EvalWaitNot),
                new TokenInfo(TokenKind.Predicate, 1, 0, Parser.EvalPredicate),
                new TokenInfo(TokenKind.Property, 1, 0, Parser.EvalProperty),
                new TokenInfo(TokenKind.Star,  3, 1, Parser.EvalPropose),
                //new TokenInfo(TokenKind.Type,  0, 0, EvalType),
                new TokenInfo(TokenKind.Type,  14, 0, Parser.EvalType),
                new TokenInfo(TokenKind.LeftRound,  0, 0, Parser.EvalNop)
            };
            MergeTable(tokOps);
        }
        public int GetPrec(Token tok)
        {
            return TokenInfoTable[(int)tok.Kind].Prec;
        }
        public int GetAssoc(Token tok)
        {
            int ndx = (int)tok.Kind;
            return TokenInfoTable[ndx].Assoc;
        }
        public TokenAction GetAction(Token tok)
        {
            int ndx = (int)tok.Kind;
            return TokenInfoTable[ndx].Action;
        }
        public void MergeTable(TokenInfo[] table){
            foreach (var info in table)
            {
                //Table[(int)info.Kind] = info;
                TokenInfoTable[(int)info.Kind].Prec = info.Prec;
                TokenInfoTable[(int)info.Kind].Assoc = info.Assoc;
                TokenInfoTable[(int)info.Kind].Action = info.Action;
            }
        }
        /*
        //
        //Access Properties
        protected RootBlock RootBlock { get { return Parser.RootBlock; } }
        //
        protected Token CurrentToken { get { return Parser.CurrentToken; } }
        protected Token PreviousToken { get { return Parser.PreviousToken; } }
        public Token NextToken { get { return Parser.NextToken; } }
        public TokenList CurrentList { get { return Parser.CurrentList; } }

        protected Expression Subject { get { return Parser.Subject; } set { Parser.Subject = value; } }
        protected ExprSeq ExprSeq { get { return Parser.ExprSeq; } set { Parser.ExprSeq = value; } }

        protected void PushTaskDef(TaskDef def)
        {
            Parser.PushTaskDef(def);
        }
        protected TaskDef PopTaskDef()
        {
            return Parser.PopTaskDef();
        }
        protected TaskDef CurrentTaskDef
        {
            get { return Parser.CurrentTaskDef; }
        }
        //Access Methods
        protected bool Advance()
        {
            return Parser.Advance();
        }
        protected void CreateState(TokenList list)
        {
            Parser.CreateState(list);
        }
        protected ParserState PopState()
        {
            return Parser.PopState();
        }
        protected Node PopNode()
        {
            return Parser.PopNode();
        }
        protected void PushNode(Node node)
        {
            Parser.PushNode(node);
        }
        protected Node PeekNode()
        {
            return Parser.PeekNode();
        }
        protected Node CurrentNode { get { return Parser.CurrentNode; } }
        protected Node ParentNode { get { return Parser.ParentNode; } }
        //
        protected ExprSeq ParseExprSeq(ParseFlag flags = ParseFlag.None)
        {
            return Parser.ParseExprSeq(flags);
        }
        protected Expression ParseExpression()
        {
            return Parser.ParseExpression();
        }
        protected void PushExpr(Expression expr)
        {
            Parser.PushExpr(expr);
        }
        protected Expression PopExpr()
        {
            return Parser.PopExpr();
        }
        protected Expression PeekExpr()
        {
            return Parser.PeekExpr();
        }*/
    }
}
