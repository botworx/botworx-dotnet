using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile
{
    //TODO:!!!:None of these classes are fully generic!!!
    public interface INodeVisitor
    {
        void Visit(AstNode node);
        void Visit(AstNode node, NodeVisit cb);
        void PushNode(AstNode node);
        AstNode PopNode();
    }
    public struct NodeVisitInfo
    {
        public NodeVisitInfo(AstNodeKind kind, INodeVisit visit)
        {
            Kind = kind;
            Visit = visit;
        }
        public AstNodeKind Kind;
        public INodeVisit Visit;
    }
    public class NodeVisitor : NodeUser, INodeVisitor
    {
        //
        private Stack<Stmt> StmtStack = new Stack<Stmt>();
        public Stmt CurrentStmt { get { return StmtStack.Peek(); } }
        public void PushStmt(Stmt stmt)
        {
            StmtStack.Push(stmt);
        }
        public Stmt PopStmt()
        {
            return StmtStack.Pop();
        }
        //
        public Stack<NodeVisitorPolicy> PolicyStack = new Stack<NodeVisitorPolicy>();
        public void PushPolicy(NodeVisitorPolicy policy)
        {
            PolicyStack.Push(policy);
        }
        public NodeVisitorPolicy PopPolicy()
        {
            return PolicyStack.Pop();
        }
        public NodeVisitorPolicy PeekPolicy()
        {
            return PolicyStack.Peek();
        }
        public NodeVisitorPolicy CurrentPolicy { get { return PeekPolicy(); } }
        //
        public NodeVisitor()
        {
        }
        public void Visit(AstNode node)
        {
            if (node == null)
                return;
            //else
            CurrentPolicy.Visit(node);
        }
        public void Visit(AstNode node, NodeVisit cb)
        {
            if (node == null)
                return;
            //else
            CurrentPolicy.Visit(node, cb);
        }
    }
}
