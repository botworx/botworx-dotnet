using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile
{
    public class NodeVisitorPolicy
    {
        public int VisitsLength = Enum.GetValues(typeof(AstNodeKind)).Length;
        public NodeVisitInfo[] VisitInfoTable = null;
        //
        public NodeVisitorPolicy()
        {
            VisitInfoTable = new NodeVisitInfo[VisitsLength];
        }
        public void Visit(AstNode node)
        {
            if (node == null)
                return;
            //else
                VisitInfoTable[node.Key].Visit.Perform(node);
        }
        public void Visit(AstNode node, NodeVisit cb)
        {
            if (node == null)
                return;
            //else
                VisitInfoTable[node.Key].Visit.Perform(node, cb);
        }
        public void MergeVisits(NodeVisitInfo[] table)
        {
            foreach (var info in table)
            {
                VisitInfoTable[(int)info.Kind] = info;
            }
        }
        public void ConfigureVisits(INodeVisitor t)
        {
            foreach (var info in VisitInfoTable)
            {
                if (info.Visit != null)
                    info.Visit.Configure(t);
            }
        }
    }
}
