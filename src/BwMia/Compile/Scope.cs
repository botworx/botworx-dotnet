using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile
{
    //
    public delegate void ScopeCleanup();
    //
    public class Scope
    {
        Scope Previous;
        public AstNode Node;
        Stack<ScopeCleanup> Cleanups; //uses lazy creation
        Dictionary<string, Var> Dictionary = new Dictionary<string, Var>();
        //
        public Scope(Scope previous, AstNode node)
        {
            Previous = previous;
            Node = node;
        }
        public Var CreateVar(Token token)
        {
            return CreateVar(token, TokenInstance.CSharp.ELEMENT);
        }
        public Var CreateVar(Token token, Token typeToken)
        {
            Var var = new Var(token, typeToken);
            AddVar(var);
            return var;
        }
        public void AddVar(Var var)
        {
            Dictionary.Add(var.Token.ToString(), var);
        }
        public Var FindVar(string key)
        {
            Var var;
            if (Dictionary.TryGetValue(key, out var))
                return var;
            //else
            if (Previous != null)
                return Previous.FindVar(key);
            //else
            return null;
        }
        public void CollectVariables(List<Var> vars)
        {
            if (Previous != null)
                Previous.CollectVariables(vars);
            foreach (var pair in Dictionary)
            {
                vars.Add(pair.Value);
            }
        }
        public void PushCleanup(ScopeCleanup cleanup)
        {
            if (Cleanups == null)
                Cleanups = new Stack<ScopeCleanup>();
            Cleanups.Push(cleanup);
        }
        public void Cleanup()
        {
            if (Cleanups == null)
                return;
            //else
            foreach (var cleanup in Cleanups)
            {
                cleanup();
            }
        }
    }
}
