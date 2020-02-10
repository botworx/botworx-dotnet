using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia
{
    public static class TokenInstance
    {
        public static Token SubjectMacro = new Token(TokenKind.SubjectMacro);
        public static Token Fragment = new Token(TokenKind.Fragment);
        public static class Mia
        {
            //Punctuation
            public static Token LeftRound = new Token(TokenKind.LeftRound);
            public static Token ExpressionEnd = new Token(TokenKind.ExpressionEnd);
            //Keywords
            public static Token Namespace = new Token(TokenKind.Namespace, "namespace");
            public static Token Brain = new Token(TokenKind.Brain, "brain");
            public static Token Using = new Token(TokenKind.Using, "using");
            public static Token Predicate = new Token(TokenKind.PredicateKw, "predicate");
            public static Token Expert = new Token(TokenKind.Expert, "expert");
            public static Token Context = new Token(TokenKind.Context, "context");
            public static Token Topic = new Token(TokenKind.Topic, "topic");
            public static Token Rule = new Token(TokenKind.Rule, "rule");
            public static Token Method = new Token(TokenKind.Method, "method");
            public static Token Task = new Token(TokenKind.Task, "task");
            public static Token Where = new Token(TokenKind.Where, "where");
            public static Token Post = new Token(TokenKind.Post, "post");
            public static Token Propose = new Token(TokenKind.Propose, "propose");
            public static Token Parallel = new Token(TokenKind.Parallel, "parallel");
            public static Token Do = new Token(TokenKind.Do, "do");
            public static Token Maybe = new Token(TokenKind.Maybe, "maybe");
            public static Token Weight = new Token(TokenKind.Weight, "weight");
            public static Token Select = new Token(TokenKind.Select, "select");
            public static Token Case = new Token(TokenKind.Case, "case");
            public static Token Condition = new Token(TokenKind.Condition, "condition");
            public static Token Precondition = new Token(TokenKind.Precondition, "precondition");
            public static Token Postcondition = new Token(TokenKind.Postcondition, "postcondition");
            public static Token Branch = new Token(TokenKind.Branch, "branch");
            public static Token Succeed = new Token(TokenKind.Succeed, "succeed");
            public static Token Fail = new Token(TokenKind.Fail, "fail");
            public static Token Throw = new Token(TokenKind.Throw, "throw");
            public static Token Halt = new Token(TokenKind.Halt, "halt");
            public static Token Return = new Token(TokenKind.Return, "return");
        }
        public static class CSharp
        {
            public static Token THIS = Token.Create("this");
            public static Token NULL = Token.Create("null");
            //
            public static Token EMPTYSTRING = Token.Create("");
            public static Token BLANK = Token.Create("_");
            public static Token NIL = Token.Create("NIL");
            public static Token SELF = Token.Create("Self");
            public static Token CREATE = Token.Create("Create");
            public static Token MODULE = Token.Create("Module");
            public static Token MESSAGE = Token.Create("Message");
            public static Token ELEMENT = Token.Create("Atom");
            public static Token ENTITY = Token.Create("Entity");
            public static Token BRAIN = Token.Create("Brain");
            public static Token EXPERT = Token.Create("Expert");
            public static Token REACTOR = Token.Create("Reactor");
            public static Token DELIBERATOR = Token.Create("Deliberator");
            public static Token METHOD = Token.Create("Method");
            public static Token TASK = Token.Create("Task");
            //
            public static Token ACTIVE = Token.Create("Active");
            public static Token SUSPENDED = Token.Create("Suspended");
            public static Token SUCCEEDED = Token.Create("Succeeded");
            public static Token FAILED = Token.Create("Failed");
            //
            public static Token TYPE_ATOM = Token.Create(TokenKind.Type, "Atom");
            public static Token TYPE_ENTITY = Token.Create(TokenKind.Type, "Entity");
            public static Token TYPE_CLAUSE = Token.Create(TokenKind.Type, "Clause");
            public static Token TYPE_BELIEF = Token.Create(TokenKind.Type, "Belief");
            public static Token TYPE_GOAL = Token.Create(TokenKind.Type, "Goal");
            public static Token TYPE_PERFORM = Token.Create(TokenKind.Type, "Perform");
            public static Token TYPE_ACHIEVE = Token.Create(TokenKind.Type, "Achieve");
            public static Token TYPE_QUERY = Token.Create(TokenKind.Type, "Query");
            public static Token TYPE_MAINTAIN = Token.Create(TokenKind.Type, "Maintain");
            //
            public static Token PRED_NIL = Token.Create("nil");
            public static Token PRED_CALLBACK = Token.Create("callback");
            public static Token PRED_STATUS = Token.Create("status");
            public static Token PRED_CONTEXT = Token.Create("context");
            public static Token OP_NOTEQUAL = Token.Create("!=");
            //
            public static Token _BWXSUBJECT = Token.Create(TokenKind.Variable, "_bwxSubject");
            public static Token _BWXRESULT = Token.Create(TokenKind.Variable, "_bwxResult");
            public static Token BWXTASK = Token.Create(TokenKind.Variable, "bwxTask");
        }
    }
}
