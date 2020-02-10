using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Parse
{
    public class ParserState
    {
        public ParseFlag ParseFlags;
        //
        public TokenList List;
        IEnumerator<Token> Iter;
        IEnumerator<Token> NextIter;
        //
        public Token PreviousToken;
        public Token CurrentToken;
        public Token NextToken;
        //
        public Stack<Token> ShuntStack = new Stack<Token>();
        public Stack<Token> ShuntStack2 = new Stack<Token>();
        public List<Token> ShuntQueue = new List<Token>();
        //
        public Expression Subject;
        //
        public ParserState(TokenList list)
        {
            List = list;
            Iter = list.GetEnumerator();
            Iter.MoveNext();
            CurrentToken = Iter.Current;
            NextIter = list.GetEnumerator();
            NextIter.MoveNext();
            NextIter.MoveNext();
            NextToken = NextIter.Current;
            //
            //Subject = BuiltinDefs.Self.CreateName();
            Subject = Expression.Nil;
        }
        public ParserState Clone()
        {
            ParserState clone = new ParserState(List);
            clone.Subject = Subject;
            return clone;
        }
        public Token Peek()
        {
            return NextToken;
        }
        public bool Advance()
        {
            bool success = true;
            PreviousToken = Iter.Current;
            if (success = Iter.MoveNext())
                CurrentToken = Iter.Current;
            else
                CurrentToken = null;
            //
            if (NextIter.MoveNext())
                NextToken = NextIter.Current;
            else
                NextToken = null;
            return success;
        }
    }
    [Flags]
    public enum ParseFlag : int
    {
        None = 0,
        ParsingClause = 1,
        ParseLines = 2
    }
}
