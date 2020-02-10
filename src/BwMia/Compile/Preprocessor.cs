//#define DEBUG_PREPROCESSOR

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Botworx.Mia.Compile
{
    class Preprocessor
    {
        Token CurrentToken = null;
        Token NextToken = null;
        IEnumerator<Token> CurrentTokenIter = null;
        IEnumerator<Token> NextTokenIter = null;
        //
        List<TokenKind> NoTerminators =
            new List<TokenKind> {};
        List<TokenKind> RoundListTerminators =
            new List<TokenKind> { TokenKind.RightRound };
        List<TokenKind> SquareListTerminators =
            new List<TokenKind> { TokenKind.RightSquare };
        List<TokenKind> AngleListTerminators =
            new List<TokenKind> { TokenKind.RightAngle };
        //
        public TokenList Read(Stream str)
        {
            Tokenizer tokenizer = new Tokenizer();
            TokenList inList = tokenizer.Tokenize(str);
            //
            TokenList outList = new TokenList();
            CurrentTokenIter = inList.GetEnumerator();
            NextTokenIter = inList.GetEnumerator();
            NextTokenIter.MoveNext();
            NextToken = NextTokenIter.Current;
            //
            ReadTokenLists(outList);
            //
            //DebugTextWriter writer = new DebugTextWriter();
            StringWriter writer = new StringWriter();
            inList.RawPrint(writer);
#if DEBUG_PREPROCESSOR
            Debug.Print(writer.ToString());
#endif
            return outList;
        }
        public bool ReadTokenLists(TokenList parentList)
        {
            bool more = true;
            while (more)
            {
                try
                {
                    more = ReadTokenList(parentList, TokenKind.LineList, NoTerminators);
                }
                catch (Escape)
                {
                    break;
                }
            }
            return more;
        }
        public bool ReadTokenList(TokenList parentList, TokenKind listKind, List<TokenKind> terminators)
        {
            TokenList outList = new TokenList();
            bool more = true;
            while (more)
            {
                more = ReadToken(outList, terminators);
            }
            if (listKind != TokenKind.RoundList && outList.Count == 0)
                return true;
            //else
            parentList.Add(new Token(listKind, outList));
            return true;
        }
        public bool MaybeReadToken(TokenList parentList, List<TokenKind> terminators)
        {
            if (terminators.Contains(NextToken.Kind))
                return false;
            //else
            return ReadToken(parentList, terminators);
        }
        public bool ReadToken(TokenList parentList, List<TokenKind> terminators)
        {
            Advance();
            if (terminators.Contains(CurrentToken.Kind))
                return false;
            //else
            switch (CurrentToken.Kind)
            {
                case TokenKind.Name:
                    parentList.Add(ReadComplexName(TokenKind.Name));
                    break;
                case TokenKind.Dollar:
                    Advance();
                    parentList.Add(ReadComplexName(TokenKind.Variable));
                    break;
                case TokenKind.LeftRound:
                    ReadTokenList(parentList, TokenKind.RoundList, RoundListTerminators);
                    break;
                case TokenKind.LeftSquare:
                    ReadTokenList(parentList, TokenKind.SquareList, SquareListTerminators);
                    break;
                case TokenKind.LeftAngle:
                    ReadTokenList(parentList, TokenKind.SquareList, AngleListTerminators);
                    break;
                case TokenKind.LineEnd:
                    if (NextToken.Kind == TokenKind.Indent)
                    {
                        Advance();
                        ReadTokenLists(parentList);
                        return false;
                    }
                    return false;
                    //break;
                case TokenKind.Dedent:
                    throw new Escape();
                default:
                    parentList.Add(CurrentToken);
                    break;
            }
            return true;
        }
        Token ReadComplexName(TokenKind targetKind)
        {
            if (NextToken.Kind != TokenKind.Dot)
                if (targetKind == TokenKind.Name)
                    return CurrentToken;
                else
                    return new Token(CurrentToken, targetKind);
            //else
            //TODO:Need to test this more
            TokenList nameList = new TokenList();
            do 
            {
                TokenKind kind = CurrentToken.Kind;
                if (kind == TokenKind.Name)
                {
                    nameList.Add(CurrentToken);
                    if (NextToken.Kind != TokenKind.Dot)
                        break;
                }
                else if (kind == TokenKind.Dot)
                    continue;
                else
                    break;
            } while (Advance()) ;
            return new Token(targetKind, new Token(TokenKind.DottedName, nameList));
        }
        bool Advance(){
            bool success = NextTokenIter.MoveNext();
            if (!success)
                throw new Escape();
            NextToken = NextTokenIter.Current;
            CurrentTokenIter.MoveNext();
            CurrentToken = CurrentTokenIter.Current;
            //Debug.Write(CurrentToken.Kind); Debug.Write(":  ");
            //Debug.WriteLine(CurrentToken.Value);
            return success && SkipJunk();
        }
        bool SkipJunk()
        {
            switch (CurrentToken.Kind)
            {
                case TokenKind.WhiteSpace:
                case TokenKind.Comment:
                    return Advance();
            }
            return true;
        }
    }
    class Escape : Exception
    {
    }
}
