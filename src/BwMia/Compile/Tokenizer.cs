using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Botworx.Mia.Compile
{
    public class Tokenizer
    {
        Stack<int> IndentStack;
        int ExtraIndent = 0; //modified by #indent and #dedent directives
        //
        public Tokenizer()
        {
            IndentStack = new Stack<int>();
            IndentStack.Push(0);
        }
        public TokenList Tokenize(Stream str)
        {
            StreamReader sr = new StreamReader(str);
            String line;
            TokenList tokenList = new TokenList();
            try
            {
                TokenList tokList = null;
                while ((line = sr.ReadLine()) != null)
                {
                    tokList = TokenizeLine(line);
                    if(tokList != null)
                        tokenList.AddRange(tokList);
                }
            }
            finally
            {
                str.Close();
            }
            tokenList.Add(new Token(TokenKind.FileEnd, 0));
            return tokenList;
        }
        private TokenList TokenizeLine(string line)
        {
            int oldLength = line.Length;
            line = line.TrimStart(' ');
            if (line == "")
                return null;
            //else
            TokenList tokenList = new TokenList();
            //
            int indentLevel = oldLength - line.Length;
            indentLevel += ExtraIndent;
            if (indentLevel > IndentStack.Peek())
            {
                Indent(indentLevel, tokenList);
            }
            else if (indentLevel < IndentStack.Peek())
            {
                Dedent(indentLevel, tokenList);
            }
            //
            Regex regexPattern = new Regex(TokenInfo.PatternString);
            MatchCollection matches = regexPattern.Matches(line);

            foreach (Match match in matches)
            {
                TokenKind i = 0;
                TokenInfo reserved;
                TokenKind code = 0;

                foreach (Group group in match.Groups)
                {
                    string matchValue = group.Value;
                    bool success = group.Success;
                    // ignore capture index 0 and 1 (general and WhiteSpace)
                    if (success && i > 0)
                    {
                        code = i;
                        switch (i)
                        {
                            case TokenKind.Name:
                                matchValue = matchValue.Replace('-', '_');
                                if (TokenInfo.Keywords.TryGetValue(matchValue, out reserved))
                                    code = reserved.Kind;
                                tokenList.Add(CreateToken(code, matchValue, line));
                                break;
                            case TokenKind.Directive:
                                matchValue = matchValue.Replace('-', '_');
                                matchValue = matchValue.Replace("#", "pound");
                                if (TokenInfo.Directives.TryGetValue(matchValue, out reserved))
                                    code = reserved.Kind;
                                switch (code)
                                {
                                    case TokenKind.PoundIndent:
                                        ExtraIndent += 4;
                                        return null;
                                    case TokenKind.PoundDedent:
                                        ExtraIndent -= 4;
                                        return null;
                                }
                                break;
                            case TokenKind.Type:
                                matchValue = matchValue.Replace('-', '_');
                                matchValue = matchValue.TrimEnd(':');
                                tokenList.Add(CreateToken(code, matchValue, line));
                                break;
                            case TokenKind.Property:
                                matchValue = matchValue.Replace('-', '_');
                                matchValue = matchValue.TrimStart(':');
                                tokenList.Add(CreateToken(code, matchValue, line));
                                break;
                            default:
                                tokenList.Add(CreateToken(code, matchValue, line));
                                break;
                        }
                    }
                    i++;
                }

            }
            tokenList.Add(new Token(TokenKind.LineEnd, indentLevel));
            return tokenList;
        }
        Token CreateToken(TokenKind kind, object value, string line)
        {
            Token token = null;
            switch (kind)
            {
                case TokenKind.Name:
                    token = Token.Create((string)value);
                    break;
                case TokenKind.Snippet:
                    string s = (string)value;
                    s = s.TrimStart('{');
                    s = s.TrimEnd('}');
                    value = s;
                    token = new Token(kind, value);
                    break;
                default:
                    token = new Token(kind, value);
                    break;
            }
            token.Line = line;
            return token;
        }
        void Indent(int indentLevel, TokenList tokenList){
            IndentStack.Push(indentLevel);
            tokenList.Add(new Token(TokenKind.Indent, indentLevel));
        }
        void Dedent(int indentLevel, TokenList tokenList){
            while (IndentStack.Peek() != indentLevel)
            {
                tokenList.Add(new Token(TokenKind.Dedent, IndentStack.Pop()));
            }
        }
    }
}