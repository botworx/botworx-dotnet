using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia
{
    public struct TokenInfo
    {
        public static string PatternString;
        public static Dictionary<string, TokenInfo> Keywords = new Dictionary<string, TokenInfo>();
        public static Dictionary<string, TokenInfo> Operators = new Dictionary<string, TokenInfo>();
        public static Dictionary<string, TokenInfo> Directives = new Dictionary<string, TokenInfo>();

        public TokenKind Kind;
        public TokenCategory Category;
        public string Pattern;
        public TokenAction Action; //Unary
        public TokenAction Action2; //Binary
        public int Prec;
        public int Assoc;
        //
        public TokenInfo(TokenKind kind, TokenAction action)
            : this(kind, 0, 0, action)
        {
        }
        public TokenInfo(TokenKind kind, int prec, int assoc, TokenAction action)
            : this(kind, TokenCategory.None, null)
        {
            Action = action;
            Prec = prec;
            Assoc = assoc;
        }
        public TokenInfo(TokenKind kind, TokenCategory category, string pattern)
            : this(kind, category, pattern, null)
        {
        }
        public TokenInfo(TokenKind kind, TokenCategory category, string pattern, TokenAction action)
            : this(kind, category, pattern, action, null)
        {
        }
        public TokenInfo(TokenKind kind, TokenCategory category, string pattern, TokenAction action, TokenAction action2)
        {
            Kind = kind;
            Category = category;
            Pattern = pattern;
            Action = action;
            Action2 = action2;
            Prec = 0;
            Assoc = 0;
        }
        //
        static TokenInfo()
        {
            foreach (var tokInfo in TokenInfo.Instances)
            {
                PatternString += tokInfo.Pattern;
            }
            for (int i = 0; i < TokenInfo.Instances.Length; ++i)
            {
                TokenInfo info = TokenInfo.Instances[i];
                switch (info.Category)
                {
                    case TokenCategory.Keyword:
                        Keywords.Add(info.Pattern.ToLower(), TokenInfo.Instances[i]);
                        break;
                    case TokenCategory.Operator:
                        Operators.Add(info.Pattern.ToLower(), TokenInfo.Instances[i]);
                        break;
                    case TokenCategory.Directive:
                        Directives.Add(info.Pattern.ToLower(), TokenInfo.Instances[i]);
                        break;
                }
            }
        }
        //
        public static TokenInfo[] Instances
            = {
            new TokenInfo(TokenKind.Nil, TokenCategory.None, "Nil"),
            new TokenInfo(TokenKind.WhiteSpace, TokenCategory.None, @"(?<WhiteSpace>\s)|"),
            new TokenInfo(TokenKind.Comment, TokenCategory.None, @"(?<Comment>\//.*$)|"),
            new TokenInfo(TokenKind.LeftRound, TokenCategory.None, @"(?<LeftRound>\()|"),
            new TokenInfo(TokenKind.RightRound, TokenCategory.None, @"(?<RightRound>\))|"),
            new TokenInfo(TokenKind.LeftSquare, TokenCategory.None, @"(?<LeftSquare>\[)|"),
            new TokenInfo(TokenKind.RightSquare, TokenCategory.None, @"(?<RightSquare>\])|"),
            new TokenInfo(TokenKind.LeftAngle, TokenCategory.None, @"(?<LeftAngle>\<)|"),
            new TokenInfo(TokenKind.RightAngle, TokenCategory.None, @"(?<RightAngle>\>)|"),
            //
            new TokenInfo(TokenKind.ShortSkinnyArrow, TokenCategory.Operator | TokenCategory.Arrow, @"(?<ShortSkinnyArrow>->)|"),
            new TokenInfo(TokenKind.LongSkinnyArrow, TokenCategory.Operator | TokenCategory.Arrow, @"(?<LongSkinnyArrow>-->)|"),
            new TokenInfo(TokenKind.NotLongSkinnyArrow, TokenCategory.Operator | TokenCategory.Arrow, @"(?<NotLongSkinnyArrow>!-->)|"),
            new TokenInfo(TokenKind.ShortFatArrow, TokenCategory.Operator | TokenCategory.Arrow, @"(?<ShortFatArrow>=>)|"),
            new TokenInfo(TokenKind.LongFatArrow, TokenCategory.Operator | TokenCategory.Arrow, @"(?<LongFatArrow>==>)|"),
            new TokenInfo(TokenKind.NotLongFatArrow, TokenCategory.Operator | TokenCategory.Arrow, @"(?<NotLongFatArrow>!==>)|"),
            //
            new TokenInfo(TokenKind.Equal, TokenCategory.Operator, @"(?<Equal>\==)|"),
            new TokenInfo(TokenKind.NotEqual, TokenCategory.Operator, @"(?<NotEqual>\!=)|"),
            //
            new TokenInfo(TokenKind.BangBang, TokenCategory.Operator, @"(?<BangBang>\!!)|"),
            new TokenInfo(TokenKind.Bang, TokenCategory.Operator, @"(?<Bang>\!)|"),
            new TokenInfo(TokenKind.PlusPlus, TokenCategory.Operator, @"(?<PlusPlus>\+\+)|"),
            new TokenInfo(TokenKind.MinusMinus, TokenCategory.Operator, @"(?<MinusMinus>\--)|"),
            new TokenInfo(TokenKind.MinusPlus, TokenCategory.Operator, @"(?<MinusPlus>\-\+)|"),

            new TokenInfo(TokenKind.Plus, TokenCategory.Operator, @"(?<Plus>\+)|"),
            new TokenInfo(TokenKind.Minus, TokenCategory.Operator, @"(?<Minus>\-)|"),
            new TokenInfo(TokenKind.ForwardSlash, TokenCategory.Operator, @"(?<ForwardSlash>\/)|"),
            new TokenInfo(TokenKind.Tilde, TokenCategory.Operator, @"(?<Tilde>\~)|"),
            new TokenInfo(TokenKind.Assign, TokenCategory.Operator, @"(?<Assign>\=)|"),
            new TokenInfo(TokenKind.At, TokenCategory.Operator, @"(?<At>\@)|"),
            //
            new TokenInfo(TokenKind.Dollar, TokenCategory.None, @"(?<Dollar>\$)|"),
            //@"(?<Pound>\#)|",
            new TokenInfo(TokenKind.QuestionMark, TokenCategory.None, @"(?<QuestionMark>\?)|"),
            new TokenInfo(TokenKind.Star, TokenCategory.Operator, @"(?<Star>\*)|"),
            //@"(?<Colon>:)|",
            new TokenInfo(TokenKind.Float, TokenCategory.Literal, @"(?<Float>[-+]?[0-9]*\.[0-9]+[f|F])|"),
            new TokenInfo(TokenKind.Double, TokenCategory.Literal, @"(?<Double>[-+]?[0-9]*\.[0-9]+)|"),
            new TokenInfo(TokenKind.Integer, TokenCategory.Literal, @"(?<Integer>[0-9]+)|"),
            new TokenInfo(TokenKind.Boolean, TokenCategory.Literal, @"(?<Boolean>true|false)|"),
            new TokenInfo(TokenKind.Type, TokenCategory.Literal, @"(?<Type>[?a-zA-Z\-_$][a-zA-Z0-9\-_$]*:)|"),
            /*@"(?<Property>:[?a-zA-Z\-_$][a-zA-Z0-9\-_$]*)|",
            @"(?<Directive>#[?a-zA-Z\-_$][a-zA-Z0-9\-_$]*)|",
            @"(?<Name>[?a-zA-Z\-_$][a-zA-Z0-9\-_$]*)|",*/
            new TokenInfo(TokenKind.Property, TokenCategory.None, @"(?<Property>:[?a-zA-Z_$][a-zA-Z0-9_$]*)|"),
            new TokenInfo(TokenKind.Directive, TokenCategory.None, @"(?<Directive>#[?a-zA-Z_$][a-zA-Z0-9_$]*)|"),
            new TokenInfo(TokenKind.Name, TokenCategory.None, @"(?<Name>[?a-zA-Z_$][a-zA-Z0-9_$]*)|"),
            new TokenInfo(TokenKind.Dot, TokenCategory.None, @"(?<Dot>\.)|"),
            new TokenInfo(TokenKind.Snippet, TokenCategory.None, @"(?<HostCode>{.*})|"),
            //
            new TokenInfo(TokenKind.Invalid, TokenCategory.None, @"(?<Invalid>[^\s]+)"),
        //KeywordsBegin
        new TokenInfo(TokenKind.Namespace, TokenCategory.Keyword, "namespace"),
        new TokenInfo(TokenKind.Brain, TokenCategory.Keyword, "brain"),
        new TokenInfo(TokenKind.Using, TokenCategory.Keyword, "using"),
        new TokenInfo(TokenKind.PredicateKw, TokenCategory.Keyword, "predicate"),
        new TokenInfo(TokenKind.Expert, TokenCategory.Keyword, "expert"),
        new TokenInfo(TokenKind.Context, TokenCategory.Keyword, "context"),
        new TokenInfo(TokenKind.Topic, TokenCategory.Keyword, "topic"),
        new TokenInfo(TokenKind.Rule, TokenCategory.Keyword, "rule"),
        new TokenInfo(TokenKind.Method, TokenCategory.Keyword, "method"),
        new TokenInfo(TokenKind.Task, TokenCategory.Keyword, "task"),
        new TokenInfo(TokenKind.Where, TokenCategory.Keyword, "where"),
        new TokenInfo(TokenKind.Post, TokenCategory.Keyword, "post"),
        new TokenInfo(TokenKind.Propose, TokenCategory.Keyword, "propose"),
        new TokenInfo(TokenKind.Parallel, TokenCategory.Keyword, "parallel"),
        new TokenInfo(TokenKind.Do, TokenCategory.Keyword, "do"),
        new TokenInfo(TokenKind.Maybe, TokenCategory.Keyword, "maybe"),
        new TokenInfo(TokenKind.Weight, TokenCategory.Keyword, "weight"),
        new TokenInfo(TokenKind.Select, TokenCategory.Keyword, "select"),
        new TokenInfo(TokenKind.Case, TokenCategory.Keyword, "case"),
        new TokenInfo(TokenKind.Condition, TokenCategory.Keyword, "condition"),
        new TokenInfo(TokenKind.Precondition, TokenCategory.Keyword, "precondition"),
        new TokenInfo(TokenKind.Postcondition, TokenCategory.Keyword, "postcondition"),
        new TokenInfo(TokenKind.Branch, TokenCategory.Keyword, "branch"),
        new TokenInfo(TokenKind.Succeed, TokenCategory.Keyword, "succeed"),
        new TokenInfo(TokenKind.Fail, TokenCategory.Keyword, "fail"),
        new TokenInfo(TokenKind.Throw, TokenCategory.Keyword, "throw"),
        new TokenInfo(TokenKind.Halt, TokenCategory.Keyword, "halt"),
        new TokenInfo(TokenKind.Return, TokenCategory.Keyword, "return"),
        //KeywordsEnd

        //DirectivesBegin
        new TokenInfo(TokenKind.PoundIndent, TokenCategory.Directive, "PoundIndent"),
        new TokenInfo(TokenKind.PoundDedent, TokenCategory.Directive, "PoundDedent"),
        //DirectivesEnd

        //MarkersBegin
        new TokenInfo(TokenKind.FileEnd, TokenCategory.Marker, "FileEnd"),
        new TokenInfo(TokenKind.LineEnd, TokenCategory.Marker, "LineEnd"),
        new TokenInfo(TokenKind.ExpressionEnd, TokenCategory.Marker, "ExpressionEnd"),
        new TokenInfo(TokenKind.Indent, TokenCategory.Marker, "Indent"),
        new TokenInfo(TokenKind.Dedent, TokenCategory.Marker, "Dedent"),
        new TokenInfo(TokenKind.List, TokenCategory.Marker, "List"), //Abstract
        new TokenInfo(TokenKind.LineList, TokenCategory.Marker, "LineList"),
        new TokenInfo(TokenKind.ExpressionList, TokenCategory.Marker, "ExpressionList"), //Abstract
        new TokenInfo(TokenKind.RoundList, TokenCategory.Marker, "RoundList"),
        new TokenInfo(TokenKind.SquareList, TokenCategory.Marker, "SquareList"),
        new TokenInfo(TokenKind.DottedName, TokenCategory.Marker, "DottedName"),
        new TokenInfo(TokenKind.Variable, TokenCategory.Marker, "Variable"),
        new TokenInfo(TokenKind.Literal, TokenCategory.Marker, "Literal"),
        new TokenInfo(TokenKind.Fragment, TokenCategory.Marker, "Fragment"),
        new TokenInfo(TokenKind.Predicate, TokenCategory.Marker, "Predicate"),
        //MarkersEnd

        //Macros Begin
        new TokenInfo(TokenKind.SubjectMacro, TokenCategory.Macro, "SubjectMacro")
        //Macros End
              };
    }
}
