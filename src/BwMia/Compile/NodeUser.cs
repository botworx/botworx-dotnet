using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile
{
    public class NodeUser
    {
        protected Stack<NodeStackElement> NodeStack = new Stack<NodeStackElement>();
        public void PushNode(AstNode node)
        {
            AstNode parent = null;
            if (NodeStack.Count > 0)
                parent = NodeStack.Peek().Node;
            NodeStack.Push(new NodeStackElement(node, parent));
        }
        public AstNode PopNode()
        {
            return NodeStack.Pop().Node;
        }
        public AstNode PeekNode()
        {
            return NodeStack.Peek().Node;
        }
        public AstNode CurrentNode { get { return PeekNode(); } }
        public AstNode ParentNode
        {
            get
            {
                return NodeStack.Peek().Parent;
            }
        }
    }
    //
    public struct NodeStackElement
    {
        public NodeStackElement(AstNode node, AstNode parent)
        {
            Node = node;
            Parent = parent;
        }
        public AstNode Node;
        public AstNode Parent;
    }
}
