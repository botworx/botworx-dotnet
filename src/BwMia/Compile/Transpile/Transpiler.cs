using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

using Botworx.Mia.Compile.Ast;

using Botworx.Mia.Compile.Transpile.Policy;
using Botworx.Mia.Compile.Transpile.Policy.Rhs;
using Botworx.Mia.Compile.Transpile.Policy.Lhs;
using Botworx.Mia.Compile.Transpile.Policy.Trigger;
using Botworx.Mia.Compile.Transpile.Policy.Context;

namespace Botworx.Mia.Compile.Transpile
{
    public partial class Transpiler : NodeVisitor
    {
        TextWriter CurrentWriter;
        TextWriter FinalWriter;
        int IndentLevel = 0;
        public RootBlock RootBlock;
        //
        Stack<Scope> ScopeStack = new Stack<Scope>();
        public Scope CurrentScope { get { return ScopeStack.Peek(); } }
        public int ScopeLevel { get { return ScopeStack.Count; } }
        //
        Stack<ContextDef> ContextDefStack = new Stack<ContextDef>();
        public ContextDef CurrentContextDef { get { return ContextDefStack.Peek(); } }
        //
        RootPolicy RootPolicy = new RootPolicy();
        RhsPolicy RhsPolicy = new RhsPolicy();
        RhsMsgPolicy RhsMsgPolicy = new RhsMsgPolicy();
        LhsPolicy LhsPolicy = new LhsPolicy();
        TriggerPolicy TriggerPolicy = new TriggerPolicy();
        ContextPolicy ContextPolicy = new ContextPolicy();
        //
        public Transpiler(Stream stream)
        {
            CurrentWriter = new StringWriter();
            FinalWriter = new StreamWriter(stream);
            //writer.AutoFlush = true;
            //
            ContextDefStack.Push(new ContextDef(new Token("Default")));
            ScopeStack.Push(new Scope(null, null));
            //
            ConfigPolicies();
        }
        //Policies
        void ConfigPolicies()
        {
            RootPolicy.ConfigureVisits(this);
            RhsPolicy.ConfigureVisits(this);
            RhsMsgPolicy.ConfigureVisits(this);
            LhsPolicy.ConfigureVisits(this);
            TriggerPolicy.ConfigureVisits(this);
            ContextPolicy.ConfigureVisits(this);
            //
            PushRhsPolicy();
        }
        public void PushRootPolicy() { PolicyStack.Push(RootPolicy); }
        public void PushRhsPolicy() { PolicyStack.Push(RhsPolicy); }
        public void PushRhsMsgPolicy() { PolicyStack.Push(RhsMsgPolicy); }
        public void PushLhsPolicy() { PolicyStack.Push(LhsPolicy); }
        public void PushTriggerPolicy() { PolicyStack.Push(TriggerPolicy); }
        public void PushContextPolicy() { PolicyStack.Push(ContextPolicy); }
        //
        public void Close()
        {
            FinalWriter.Write(CurrentWriter);
            FinalWriter.Close();
        }
        public void PushContextDef(ContextDef def)
        {
            ContextDefStack.Push(def);
        }
        public void PopContextDef()
        {
            ContextDefStack.Pop();
        }
        //
        public void StartBlock(AstNode def)
        {
            StartScope(def);
            WriteLine("{");
            Indent();
        }
        public void EndBlock(AstNode def)
        {
            EndScope(def, () => { Dedent(); WriteLine("}"); });
        }
        //
        public string Translate(AstNode node)
        {
            if (node == null)
                return "null";
            //else
            TextWriter prevWriter = CurrentWriter;
            CurrentWriter = new StringWriter();
            int prevIndent = IndentLevel;
            IndentLevel = 0;
            Visit(node);
            string result = CurrentWriter.ToString();
            CurrentWriter = prevWriter;
            IndentLevel = prevIndent;
            return result;
        }
        public void WriteLine()
        {
            WriteIndent();
            CurrentWriter.WriteLine();
#if DEBUG_TRANSPILER
            Debug.Print("");
#endif
        }
        public void WriteLine(string val)
        {
            WriteIndent();
            CurrentWriter.WriteLine(val);
#if DEBUG_TRANSPILER
            Debug.Print(val);
#endif
        }
        public void WriteLine(string val, params object[] args)
        {
            WriteIndent();
            CurrentWriter.WriteLine(val, args);
#if DEBUG_TRANSPILER
            Debug.Print(val, args);
#endif
        }
        public void WriteIndented(string val)
        {
            WriteIndent();
            CurrentWriter.Write(val);
#if DEBUG_TRANSPILER
            Debug.Write(val);
#endif
        }
        public void WriteIndent()
        {
            string indent = "    ";
            string totalIndent = "";
            for (int i = 0; i < IndentLevel; ++i)
            {
                totalIndent += indent;
            }
            Write(totalIndent);
        }
        public void Write(string val, params object[] args)
        {
            CurrentWriter.Write(val, args);
        }
        public void WriteLineEnd()
        {
            CurrentWriter.WriteLine();
        }
        public void WriteStatementEnd()
        {
            WriteStatementEnd(0);
        }
        public void WriteStatementEnd(int level)
        {
            for (int i = 0; i < level; ++i)
                Write(")");
            Write(";");
            //WriteLine();
            WriteLineEnd();
        }
        public void Indent()
        {
            IndentLevel += 1;
        }
        public void Dedent()
        {
            IndentLevel -= 1;
        }
        //
        public void StartScope(AstNode def)
        {
            Scope prevScope = ScopeStack.Peek();
            ScopeStack.Push(new Scope(prevScope, def));
        }
        public void EndScope(AstNode def)
        {
            EndScope(def, null);
        }
        public void EndScope(AstNode def, ScopeCleanup cleanup)
        {
            Scope scope;
            do
            {
                scope = ScopeStack.Pop();
                scope.Cleanup();
                if (cleanup != null)
                    cleanup();
            } while (scope.Node != def);
        }
        public bool InternVariable(Token token, out Var var)
        {
            return InternVariable(token, TokenInstance.CSharp.ELEMENT, out var);
        }
        public bool InternVariable(Token token, Token typeToken, out Var var)
        {
            if (token != null && token.IsVariable)
            {
                var = CurrentScope.FindVar(token.ToString());
                if (var == null)
                {
                    var = CurrentScope.CreateVar(token, typeToken);
                    return true; //was interned
                }
            }
            var = null; //must assign out before return.
            return false;
        }
        public string FixName(string name)
        {
            name = name.Replace('-', '_');
            name = name.Replace("!", "NOT_");
            name = name.Replace("=", "EQUAL_");
            return name;
        }
    }
    public class TranspilerVisit<TNode, TCallback> : NodeVisit<Transpiler, TNode, TCallback>
        where TNode : AstNode
        where TCallback : NodeVisit
    {
    }
    public class TranspilerVisit<TNode> : NodeVisit<Transpiler, TNode>
        where TNode : AstNode
    {
    }
    public class TranspilerPolicy : NodeVisitorPolicy
    {
    }
}
