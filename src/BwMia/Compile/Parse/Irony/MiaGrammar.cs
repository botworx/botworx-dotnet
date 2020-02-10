using System;
using System.Collections.Generic;
using System.Text;

using Irony.Parsing;
using Irony.Ast;

using System.Globalization;

namespace Botworx.Mia.Compile.Parse.Irony
{
    [Language("Mia", "0.1", "Mia WIP")]
    public class MiaGrammar : Grammar
    {
        void SetComposer(BnfTerm term, AstNodeCreator creator)
        {
            term.AstConfig.NodeCreator = creator;
        }
        public MiaGrammar()
            : base(caseSensitive: true)
        {

            // 1. Terminals
            var number = TerminalFactory.CreatePythonNumber("number");
            SetComposer(number, Composer.ComposeNode);
            var identifier = TerminalFactory.CreatePythonIdentifier("identifier");
            SetComposer(identifier, Composer.ComposeNode);
            //var propertyId = MiaTerminalFactory.CreatePropertyId("property");
            var varId = MiaTerminalFactory.CreateVariableId("variable");
            SetComposer(varId, Composer.ComposeNode);
            var nounId = MiaTerminalFactory.CreateNounId("noun");
            SetComposer(nounId, Composer.ComposeNode);
            var verbId = MiaTerminalFactory.CreateVerbId("verb");
            SetComposer(verbId, Composer.ComposeNode);
            var comment = new CommentTerminal("comment", "//", "\n", "\r");
            SetComposer(comment, Composer.ComposeNode);
            //comment must to be added to NonGrammarTerminals list; it is not used directly in grammar rules,
            // so we add it to this list to let Scanner know that it is also a valid terminal. 
            base.NonGrammarTerminals.Add(comment);
            var comma = ToTerm(",");
            var colon = ToTerm(":");
            var semicolon = ToTerm(";");
            var snippet1 = new StringLiteral("snippet");
            SetComposer(snippet1, Composer.ComposeNode);
            var snippet2 = new StringLiteral("snippet2");
            SetComposer(snippet2, Composer.ComposeNode);
            //snippet.AddStartEnd("{", "}", StringOptions.None);
            snippet1.AddStartEnd("{", "}", StringOptions.AllowsLineBreak);
            snippet2.AddStartEnd("{%", "%}", StringOptions.AllowsLineBreak);
            // 2. Non-terminals
            var Snippet = new NonTerminal("Snippet", Composer.ComposeNode);
            var Constituent = new NonTerminal("Constituent", Composer.ComposeNode);
            var Expr = new NonTerminal("Expr", Composer.ComposeNode);
            var Term = new NonTerminal("Term", Composer.ComposeNode);
            var BinExpr = new NonTerminal("BinExpr", Composer.ComposeNode);
            var ParExpr = new NonTerminal("ParExpr", Composer.ComposeNode);
            var UnExpr = new NonTerminal("UnExpr", Composer.ComposeNode);
            var UnOp = new NonTerminal("UnOp", Composer.ComposeNode);
            var BinOp = new NonTerminal("BinOp", Composer.ComposeNode);
            var ColonExpr = new NonTerminal("ColonExpr", Composer.ComposeNode);
            var CommaExpr = new NonTerminal("CommaExpr", Composer.ComposeNode);
            var PropertyExpr = new NonTerminal("PropertyExpr", Composer.ComposeNode);
            var AssignmentStmt = new NonTerminal("AssignmentStmt", Composer.ComposeNode);
            var Stmt = new NonTerminal("Stmt", Composer.ComposeNode);
            var ExtStmt = new NonTerminal("ExtStmt", Composer.ComposeNode);
            //
            var ReturnStmt = new NonTerminal("return", Composer.ComposeNode);
            var PassStmt = new NonTerminal("pass", Composer.ComposeNode);
            var Block = new NonTerminal("Block", Composer.ComposeNode);
            var StmtList = new NonTerminal("StmtList", Composer.ComposeNode);
            //
            var DeclBlock = new NonTerminal("DeclBlock", Composer.ComposeNode);
            var DeclStmtList = new NonTerminal("DeclStmtList", Composer.ComposeNode);

            var ParamList = new NonTerminal("ParamList", Composer.ComposeNode);
            var ArgList = new NonTerminal("ArgList", Composer.ComposeNode);
            var FunctionDef = new NonTerminal("FunctionDef", Composer.ComposeNode);
            var FunctionCall = new NonTerminal("FunctionCall", Composer.ComposeNode);
            //
            var NamespaceDef = new NonTerminal("NamespaceDef", Composer.ComposeNamespace);
            var BrainDef = new NonTerminal("BrainDef", Composer.ComposeNode);
            var ExpertDef = new NonTerminal("ExpertDef", Composer.ComposeNode);
            var MethodDef = new NonTerminal("MethodDef", Composer.ComposeNode);
            var PredicateDef = new NonTerminal("PredicateDef", Composer.ComposeNode);
            var ClauseExpr = new NonTerminal("ClauseExpr", Composer.ComposeNode);
            var PropertyList = new NonTerminal("PropertyList", Composer.ComposeNode);
            //
            var WhereActions = new NonTerminal("WhereActions", Composer.ComposeNode);
            var WhereAction = new NonTerminal("WhereAction", Composer.ComposeNode);
            var WhereStmt = new NonTerminal("WhereStmt", Composer.ComposeNode);
            var IfTrueStmt = new NonTerminal("IfTrueStmt", Composer.ComposeNode);
            var IfFalseStmt = new NonTerminal("IfFalseStmt", Composer.ComposeNode);
            var IfAllTrueStmt = new NonTerminal("IfAllTrueStmt", Composer.ComposeNode);
            var IfAllFalseStmt = new NonTerminal("IfAllFalseStmt", Composer.ComposeNode);
            //
            var SelectStmt = new NonTerminal("SelectStmt", Composer.ComposeNode);
            var CaseStmt = new NonTerminal("CaseStmt", Composer.ComposeNode);
            // 3. BNF rules
            //Expr.Rule = Term | UnExpr | BinExpr;
            Expr.Rule = Term | UnExpr | BinExpr | PropertyExpr;
            Constituent.Rule = number | ParExpr | nounId | varId | Snippet;
            Snippet.Rule = snippet1 | snippet2;
            Term.Rule = varId | nounId | ClauseExpr;
            ParExpr.Rule = "(" + Expr + ")";
            UnExpr.Rule = UnOp + Expr | UnOp + UnExpr;
            UnOp.Rule = ToTerm("+") | "-" | "*" | "@" | "/" | "not";
            BinExpr.Rule = Expr + BinOp + Expr;
            CommaExpr.Rule = Expr + "," + Eos + Block;
            ColonExpr.Rule = Expr + ":" + Eos + Block;
            PropertyExpr.Rule = Term + "`" + Term;
            BinOp.Rule = ToTerm("&&") | "||" | "~=" | "==" | "->";

            AssignmentStmt.Rule = identifier + "=" + Expr;
            Stmt.Rule = AssignmentStmt | Snippet | Expr | PassStmt | ReturnStmt | Empty;

            ReturnStmt.Rule = ("return" + Expr) | ("return" + Empty);
            PassStmt.Rule = "pass";
            //Eos is End-Of-Statement token produced by CodeOutlineFilter
            Block.Rule = Indent + StmtList + Dedent;
            DeclBlock.Rule = Indent + StmtList + Dedent;
            StmtList.Rule = MakePlusRule(StmtList, ExtStmt);
            ExtStmt.Rule = 
                Stmt + Eos | 
                ColonExpr | 
                CommaExpr | 
                NamespaceDef | 
                BrainDef | 
                ExpertDef | 
                MethodDef | 
                WhereStmt | 
                PredicateDef | 
                SelectStmt | 
                CaseStmt;

            ParamList.Rule = MakeStarRule(ParamList, comma, identifier);
            ArgList.Rule = MakeStarRule(ArgList, comma, Expr);
            FunctionDef.Rule = "def" + identifier + "(" + ParamList + ")" + colon + Eos + Block;
            FunctionDef.NodeCaptionTemplate = "def #{1}(...)";
            FunctionCall.Rule = identifier + "(" + ArgList + ")";
            FunctionCall.NodeCaptionTemplate = "call #{0}(...)";
            //
            NamespaceDef.Rule = "namespace" + identifier + Eos + Block;
            BrainDef.Rule = "brain" + identifier + Eos + Block;
            ExpertDef.Rule = "expert" + identifier + Eos + Block;
            //MethodDef.Rule = "method" + identifier + "(" + (Expr | Empty) + ")" + Eos + Block;
            MethodDef.Rule = ("method" + identifier + "(" + ArgList + ")" + Eos + Block) | ("method" + identifier + Eos + Block);
            PredicateDef.Rule = "predicate" + identifier + "(" + ParamList + ")" + Eos;
            //
            WhereActions.Rule = MakePlusRule(WhereActions, WhereAction);
            WhereAction.Rule = IfTrueStmt | IfFalseStmt | IfAllTrueStmt | IfAllFalseStmt ;
            WhereStmt.Rule = "where" + Eos + Block + WhereActions;
            IfTrueStmt.Rule = "-->" + Eos + Block;
            IfFalseStmt.Rule = "~->" + Eos + Block;
            IfAllTrueStmt.Rule = "==>" + Eos + Block;
            IfAllFalseStmt.Rule = "~=>" + Eos + Block;
            //
            SelectStmt.Rule = "select" + Eos + Block;
            CaseStmt.Rule = "case" + Snippet + Eos + Block;
            //
            PropertyList.Rule = MakeStarRule(PropertyList, comma, Expr);
            //
            ClauseExpr.Rule = verbId | (Constituent + verbId) | (verbId + Constituent) | (Constituent + verbId + Constituent);
            //
            //
            this.Root = StmtList;       // Set grammar root

            // 4. Token filters - created in a separate method CreateTokenFilters
            //    we need to add continuation symbol to NonGrammarTerminals because it is not used anywhere in grammar
            NonGrammarTerminals.Add(ToTerm(@"\"));

            // 5. Operators precedence
            //RegisterOperators(1, "+", "-");
            //RegisterOperators(2, "*", "/");
            //RegisterOperators(3, Associativity.Right, "**");

            // 6. Miscellaneous: punctuation, braces, transient nodes
            MarkPunctuation("(", ")", ":");
            RegisterBracePair("(", ")");
            //MarkTransient(Term, Expr, Stmt, ExtStmt, UnOp, BinOp, ExtStmt, ParExpr, Block);
            MarkTransient(Term, Expr, Stmt, UnOp, BinOp, ExtStmt, ParExpr, Block, Constituent, Snippet);

            // 7. Error recovery rule
            ExtStmt.ErrorRule = SyntaxError + Eos;
            FunctionDef.ErrorRule = SyntaxError + Dedent;

            // 8. Syntax error reporting
            AddToNoReportGroup("(");
            AddToNoReportGroup(Eos);
            AddOperatorReportGroup("operator");

            // 9. Initialize console attributes
            ConsoleTitle = "Mini-Python Console";
            ConsoleGreeting =
      @"Irony Sample Console for mini-Python.
 
   Supports a small sub-set of Python: assignments, arithmetic operators, 
   function declarations with 'def'. Supports big integer arithmetics.
   Supports Python indentation and line-joining rules, including '\' as 
   a line joining symbol. 

Press Ctrl-C to exit the program at any time.
";
            ConsolePrompt = ">>>";
            ConsolePromptMoreInput = "...";

            // 10. Language flags
            this.LanguageFlags = LanguageFlags.NewLineBeforeEOF | LanguageFlags.CreateAst | LanguageFlags.SupportsBigInt;
            //this.LanguageFlags = LanguageFlags.NewLineBeforeEOF | LanguageFlags.SupportsBigInt;
        }//constructor

        public override void CreateTokenFilters(LanguageData language, TokenFilterList filters)
        {
            var outlineFilter = new CodeOutlineFilter(language.GrammarData,
              OutlineOptions.ProduceIndents | OutlineOptions.CheckBraces, ToTerm(@"\")); // "\" is continuation symbol
            filters.Add(outlineFilter);
        }

    }//class
}//namespace


