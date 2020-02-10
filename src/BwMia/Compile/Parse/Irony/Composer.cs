using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Irony.Parsing;
using Irony.Ast;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Parse.Irony
{
    public static class Composer
    {
        public static void ComposeNode(AstContext context, ParseTreeNode treeNode)
        {
            //Console.Out.WriteLine("Duh");
            var astNode = new AstNode(AstNodeKind.Nil);
            treeNode.AstNode = astNode;
        }
        public static void ComposeNamespace(AstContext context, ParseTreeNode treeNode)
        {
            //Console.Out.WriteLine("Duh");
            var token = new Token(treeNode.Token.ValueString);
            var astNode = new NamespaceBlock(token);
            treeNode.AstNode = astNode;
        }
    }
}
