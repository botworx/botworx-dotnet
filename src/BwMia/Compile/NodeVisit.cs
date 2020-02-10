using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile
{
    public class NodeVisit
    {
    }
    public interface INodeVisit
    {
        void Configure(INodeVisitor t);
        void Perform(AstNode n);
        void Perform(AstNode n, NodeVisit cb);
    }
    public class NodeVisit<TVisitor, TNode> : NodeVisit<TVisitor, TNode, NodeVisit>
        where TVisitor : INodeVisitor
        where TNode : AstNode
    {
    }
    public class NodeVisit<TVisitor, TNode, TCallback> : NodeVisit, INodeVisit
        where TVisitor : INodeVisitor
        where TNode : AstNode
        where TCallback : NodeVisit
    {
        protected TVisitor t;
        //
        public void Configure(INodeVisitor t)
        {
            this.t = (TVisitor)t;
        }
        //Vanilla
        public void Perform(AstNode n) { BeginVisit((TNode)n); DoVisit((TNode)n); EndVisit((TNode)n); }
        public virtual void BeginVisit(TNode n)
        {
            t.PushNode(n);
        }
        public virtual void DoVisit(TNode n)
        {
            VisitChildren(n);
        }
        public virtual void EndVisit(TNode n)
        {
            t.PopNode();
        }
        //
        public void VisitChildren(AstNode n)
        {
            foreach (AstNode def in n.Children)
            {
                VisitChild(def);
            }
        }
        public void VisitChild(AstNode n)
        {
            t.Visit(n);
        }
        //Callbacks
        public void Perform(AstNode n, NodeVisit cb)
        {
            BeginVisit((TNode)n, (TCallback)cb);
            DoVisit((TNode)n, (TCallback)cb);
            EndVisit((TNode)n, (TCallback)cb);
        }
        public virtual void BeginVisit(TNode n, TCallback cb)
        {
            this.BeginVisit(n);
        }
        public virtual void DoVisit(TNode n, TCallback cb)
        {
            VisitChildren(n, cb);
        }
        public virtual void EndVisit(TNode n, TCallback cb)
        {
            this.EndVisit(n);
        }
        public void VisitChildren(AstNode n, NodeVisit cb)
        {
            foreach (AstNode child in n.Children)
            {
                VisitChild(child, cb);
            }
        }
        public void VisitChild(AstNode n, NodeVisit cb)
        {
            t.Visit(n, cb);
        }
    }
}
