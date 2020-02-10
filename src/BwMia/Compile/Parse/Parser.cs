using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

using Botworx.Mia.Compile.Ast;
//TODO:Why should Parser know about/use Transpiler?
using Botworx.Mia.Compile.Transpile;

namespace Botworx.Mia.Compile.Parse
{
    public partial class Parser : NodeUser
    {
        Stack<ParserState> StateStack = new Stack<ParserState>();
        Stack<TaskDef> TaskDefStack = new Stack<TaskDef>();
        Transpiler Translator;
        public RootBlock RootBlock; //This is the AST root node!
        //
        public ParserState CurrentState { get { return StateStack.Peek(); } }
        public Token PreviousToken { get { return CurrentState.PreviousToken; } }
        public Token CurrentToken { get { return CurrentState.CurrentToken; } }
        public Token NextToken { get { return CurrentState.NextToken; } }
        public TokenList CurrentList { get { return (TokenList)CurrentToken.Value; } }
        //
        Stack<ParserDialect> DialectStack = new Stack<ParserDialect>();
        List<Token> ShuntQueue { get { return CurrentState.ShuntQueue; } }
        Stack<Token> ShuntStack { get { return CurrentState.ShuntStack; } }
        Stack<Token> ShuntStack2 { get { return CurrentState.ShuntStack2; } }
        //
        public Expression Subject { get { return CurrentState.Subject; } set { CurrentState.Subject = value; } }
        public ExprSeq ExprSeq;
        //
        public Parser()
        {
        }
        //
        public void PushDialect(ParserDialect dialect)
        {
            DialectStack.Push(dialect);
        }
        public ParserDialect PopDialect()
        {
            return DialectStack.Pop();
        }
        public ParserDialect CurrentDialect
        {
            get { return DialectStack.Peek(); }
        }
        //
        public void PushTaskDef(TaskDef def)
        {
            TaskDefStack.Push(def);
        }
        public TaskDef PopTaskDef()
        {
            return TaskDefStack.Pop();
        }
        public TaskDef CurrentTaskDef
        {
            get { return TaskDefStack.Peek(); }
        }
        //Parser State Methods
        public void CreateState(TokenList list)
        {
            StateStack.Push(new ParserState(list));
        }
        public ParserState PopState()
        {
            return StateStack.Pop();
        }
        public bool Peek(out Token tok)
        {
            tok = CurrentState.Peek();
            if (tok != null)
                return true;
            else
                return false;
        }
        public bool Advance(out Token token)
        {
            bool success = Advance();
            token = CurrentToken;
            return success;
        }
        public bool Advance(out Token token, out TokenKind tokenKind)
        {
            bool success = Advance();
            token = CurrentToken;
            if (token != null)
                tokenKind = token.Kind;
            else
                tokenKind = TokenKind.Nil;
            return success;
        }
        public bool Advance()
        {
            return StateStack.Peek().Advance();
        }
    }
}
