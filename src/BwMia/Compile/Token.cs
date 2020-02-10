using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia
{
    public delegate void TokenAction(Token tok);

    public class Token
    {
        public static Token Nil = new Token(TokenKind.Nil);
        //
        public string Line;
        //
        public readonly TokenKind Kind;
        public readonly object Value;
        //
        public Token(TokenKind kind)
        {
            Kind = kind;
        }
        public Token(Token tok, TokenKind kind)
        {
            Kind = kind;
            Value = tok.Value;
            Line = tok.Line;
        }
        public Token(TokenKind kind, object value)
        {
            Kind = kind;
            Value = value;
        }
        public Token(string name)
        {
            Kind = TokenKind.Name;
            Value = name;
        }
        public override bool Equals(System.Object obj)
        {
            if (obj == null)
                return false;
            Token tok = obj as Token;
            if (tok == null)
                return false;
            //else
            return Equals(tok);
        }
        public bool Equals(Token token)
        {
            if (token == null)
                return false;
            if (Kind != token.Kind)
                return false;
            if (!Value.Equals(token.Value))
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode() ^ Kind.GetHashCode();
        }
        public static implicit operator string(Token t)
        {
            //return t.Value.ToString();
            return t.ToString();
        }
        public override string ToString()
        {
            //return (string)Value;
            if (Value == null)
                return Enum.GetName(typeof(TokenKind), Kind);
            //else
            return Value.ToString();
        }
        public static Token Create(string name)
        {
            return Create(TokenKind.Name, name);
        }
        public static Token Create(TokenKind kind, string name)
        {
            return new Token(kind, name);
        }
        public string Translate()
        {
            if (this == TokenInstance.CSharp.NIL)
                return "null";
            //else
            string name = "";

            switch (Kind)
            {
                case TokenKind.Variable:
                    if (Value is string)
                        name = (string)Value;
                    else
                        name = ((Token)Value).Translate();
                    break;
                case TokenKind.DottedName:
                    foreach (Token token in (TokenList)Value)
                    {
                        name += token.Value + ".";
                    }
                    name = name.TrimEnd('.');
                    break;
                case TokenKind.Type:
                case TokenKind.Property:
                case TokenKind.Name:
                    if (Value is string)
                        //name = "Ent_" + this;
                        name = this; //Implicit cast to string.
                    else
                        name = ((Token)Value).Translate();
                    break;
                default:
                    name = Value.ToString();
                    break;
            }
            return name;
        }
        public bool IsNil { get { return Kind == TokenKind.Nil; } }
        public bool IsLineList { get { return Kind == TokenKind.LineList; } }
        public bool IsExpressionList { get { return Kind == TokenKind.RoundList || Kind == TokenKind.SquareList; } }
        public bool IsRoundList { get { return Kind == TokenKind.RoundList; }}
        public bool IsSquareList { get { return Kind == TokenKind.SquareList; } }
        public bool IsName { get { return Kind == TokenKind.Name; } }
        public bool IsPredicate { get { return Kind == TokenKind.Predicate; } }
        public bool MaybePredicate { get { return Kind == TokenKind.Name && ((string)Value)[0] == char.ToLower(((string)Value)[0]); } }
        public bool MaybeType { get { return Kind == TokenKind.Property && ((string)Value)[0] == char.ToUpper(((string)Value)[0]); } }
        public bool IsConstituent { get { return IsLiteral || IsName || IsVariable || IsHostCode || IsExpressionList; } }
        public bool IsVariable { get { return Kind == TokenKind.Variable; } }
        public bool IsHostCode { get { return Kind == TokenKind.Snippet; } }
        public bool IsType { get { return Kind == TokenKind.Type; } }
        public bool IsProperty { get { return Kind == TokenKind.Property; } }
        public bool IsLiteral { get { return (TokenInfo.Instances[(int)Kind].Category & TokenCategory.Literal) == TokenCategory.Literal; } }
        public bool IsReserved { get { return (TokenInfo.Instances[(int)Kind].Category & TokenCategory.Keyword) == TokenCategory.Keyword; } }
        public bool IsArrow { get { return (TokenInfo.Instances[(int)Kind].Category & TokenCategory.Arrow) == TokenCategory.Arrow; } }
        public bool IsOperator { get { return (TokenInfo.Instances[(int)Kind].Category & TokenCategory.Operator) == TokenCategory.Operator; } }
        public bool IsMacro { get { return (TokenInfo.Instances[(int)Kind].Category & TokenCategory.Macro) == TokenCategory.Macro; } }
    }
    public class TokenList : List<Token>
    {
        public override string ToString()
        {
            System.IO.StringWriter writer = new StringWriter();
            Print(writer);
            writer.Close();
            return writer.ToString();
        }
        public void Print(TextWriter writer)
        {
            Print(writer, 0);
        }
        public void Print(TextWriter writer, int indent)
        {
            PrintIndent(writer, indent);
            if (Count == 0)
            {
                writer.Write("()");
                return;
            }
            foreach (Token token in this)
            {
                switch(token.Kind){
                    case TokenKind.LineList:
                        writer.WriteLine();
                        ((TokenList)token.Value).Print(writer, indent + 4);
                        break;
                    case TokenKind.RoundList:
                        writer.Write("( ");
                        ((TokenList)token.Value).Print(writer);
                        writer.Write(" )");
                        break;
                    default:
                        writer.Write(token.Translate() + " ");
                        break;
            }
            }
        }
        public void RawPrint(TextWriter writer)
        {
            int indent = 0;
            bool isNewLine = true;
            foreach (Token token in this)
            {
                switch (token.Kind)
                {
                    case TokenKind.Indent:
                        indent += 4;
                        break;
                    case TokenKind.Dedent:
                        indent -= 4;
                        break;
                    case TokenKind.LineEnd:
                        isNewLine = true;
                        break;
                    default:
                        {
                            if (isNewLine)
                            {
                                writer.WriteLine("");
                                PrintIndent(writer, indent);
                                isNewLine = false;
                            }
                            writer.Write(token.Value);
                            break;
                        }
                }
            }
        }
        public void PrintIndent(TextWriter writer, int indent)
        {
            for (int i = 0; i < indent; ++i)
            {
                writer.Write(" ");
            }
        }
    }
}
