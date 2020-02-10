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
    public partial class Parser
    {
        public void ParseFile(string moduleName, Stream inStream, Transpiler t)
        {
            Translator = t;
            RootBlock = RootBlock.Create(moduleName + "Module");
            t.RootBlock = RootBlock;
            Preprocessor preprocessor = new Preprocessor();
            TokenList list = preprocessor.Read(inStream);
#if DEBUG_TOKENIZER
            Debug.Print("");
            Debug.Print("");
            //DebugTextWriter writer = new DebugTextWriter();
            StringWriter writer = new StringWriter();
            list.Print(writer);
            Debug.Print(writer.ToString());
            Debug.Print("");
            Debug.Print("");
#endif
            //
            //PushPolicy(RootPolicy);
            CreateState(list);
            //Visit(RootBlock);
            PushDialect(new ParserDialect(this));
            ParseLines(RootBlock);
            PopState();
            //PopPolicy();

            //TODO:This should go in the compiler class.
            RootBlock.Resolve();
            t.Visit(RootBlock);
            //
            t.Close();
        }
        //
        internal void ParseNamespace(Token tok)
        {
            Advance(); //past 'namespace
            NamespaceBlock def = new NamespaceBlock(CurrentToken);
            Advance(); //past name
            ParseLines(def);
            CurrentNode.AddChild(def);
        }
        internal void ParseBrain(Token tok)
        {
            Advance(); //past 'brain
            Token token = CurrentToken;
            Advance(); //past name
            BrainDef def = new BrainDef(token, TokenInstance.CSharp.BRAIN);
            RootBlock.BrainDef = def;
            ParseLines(def);
            CurrentNode.AddChild(def);
        }
        internal void ParseUsing(NamespaceBlock parent)
        {
            Advance(); //past 'using
            Token name = CurrentToken;
            Advance(); //past name
            UsingDef def = new UsingDef(name);
            parent.AddChild(def);
        }
        //
        internal void ParsePredicate(Token tok)
        {
            Advance(); //past 'predicate
            //
            AtomTypeDef clauseType = BuiltinDefs.Belief;
            if (CurrentToken.Kind == TokenKind.Type)
            {
                clauseType = RootBlock.InternAtomTypeDef(CurrentToken);
                Advance(); //past clauseType
            }
            //
            Token name = CurrentToken;
            Advance(); //past name
            //
            Token specName = TokenInstance.CSharp.ELEMENT;
            if (CurrentToken.Kind == TokenKind.RoundList)
            {
                specName = CurrentList[0];
                Advance(); //past (specname)
            }
            PredicateDef predDef = new PredicateDef(name, clauseType, specName);
            ParseProperties(predDef);
            RootBlock.AddEntityDef(predDef);
        }
        internal void ParseExpert(Token tok)
        {
            Advance();//past expert
            Token name = CurrentToken;
            Advance();//past name
            //
            Token baseName = TokenInstance.CSharp.EXPERT;
            if (CurrentToken.Kind == TokenKind.RoundList)
            {
                TokenList tokList = CurrentList;
                if (tokList.Count != 0)
                    baseName = tokList[0];
                Advance();//past base name
            }
            //
            ExpertDef def = new ExpertDef(name, baseName);
            CurrentNode.AddChild(def);
            ParseLines(def);
        }
        //
        void ParseLine()
        {
            while (CurrentToken != null)
            {
                Token tok = CurrentToken;
                TokenAction action = CurrentDialect.GetAction(tok);
                action(tok);
            }
        }
        public void ParseLines(AstNode node)
        {
            PushNode(node);
            do
            {
                CreateState(CurrentList);
                ParseLine();
                PopState();
            } while (Advance());
            PopNode();
        }
    }
}
