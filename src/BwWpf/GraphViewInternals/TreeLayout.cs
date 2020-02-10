using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Botworx.Wpf.GraphViewInternals
{
    public delegate void LayoutNodeChanged(LayoutNode node);
    public delegate void LayoutEdgeChanged(LayoutEdge edge);

    public class LayoutTreeConfig
    {
        public int iMaxDepth = 100;
        public double iLevelSeparation = 40;
        public double iSiblingSeparation = 40;
        public double iSubtreeSeparation = 80;
        public int iRootOrientation = TreeLayout.RO_TOP;
        public int iNodeJustification = TreeLayout.NJ_TOP;
        public double topXAdjustment;
        public double topYAdjustment;
        //
        public LayoutTreeConfig()
        {
        }
    }
    public class TreeLayout
    {
        public LayoutTreeConfig Config = new LayoutTreeConfig();
        public float CanvasoffsetTop = 0;
        public float CanvasoffsetLeft = 0;

        double[] MaxLevelHeight = new double[100];
        double[] MaxLevelWidth = new double[100];
        LayoutNode[] PreviousLevelNode = new LayoutNode[100];

        public double RootYOffset = 0;
        public double RootXOffset = 0;

        List<LayoutNode> Nodes = new List<LayoutNode>();
        Dictionary<string, LayoutNode> NodeMap = new Dictionary<string, LayoutNode>();
        List<LayoutEdge> Edges = new List<LayoutEdge>();

        LayoutNode Root;
        //
        public event LayoutNodeChanged OnLayoutNodeChanged;
        public event LayoutEdgeChanged OnLayoutEdgeChanged;
        //
        public TreeLayout(LayoutTreeConfig cfg = null)
        {
            if(cfg != null)
                Config = cfg;
        }
        //Constant values

        //Tree orientation
        public const int RO_TOP = 0;
        public const int RO_BOTTOM = 1;
        public const int RO_RIGHT = 2;
        public const int RO_LEFT = 3;

        //Level node alignment
        public const int NJ_TOP = 0;
        public const int NJ_CENTER = 1;
        public const int NJ_BOTTOM = 2;

        //Layout algorithm
        public void FirstWalk(LayoutNode node, int level)
        {
            LayoutNode leftSibling = null;

            node.X = 0;
            node.Y = 0;
            node.Prelim = 0;
            node.Modifier = 0;
            node.LeftNeighbor = null;
            node.RightNeighbor = null;
            _setLevelHeight(node, level);
            _setLevelWidth(node, level);
            _setNeighbors(node, level);
            if (node._getChildrenCount() == 0 || level == Config.iMaxDepth)
            {
                leftSibling = node._getLeftSibling();
                if (leftSibling != null)
                    node.Prelim = leftSibling.Prelim + GetNodeSize(leftSibling) + Config.iSiblingSeparation;
                else
                    node.Prelim = 0;
            }
            else
            {
                var n = node._getChildrenCount();
                for (var i = 0; i < n; i++)
                {
                    var iChild = node._getChildAt(i);
                    FirstWalk(iChild, level + 1);
                }

                var midPoint = node._getChildrenCenter(this);
                midPoint -= GetNodeSize(node) / 2;
                leftSibling = node._getLeftSibling();
                if (leftSibling != null)
                {
                    node.Prelim = leftSibling.Prelim + GetNodeSize(leftSibling) + Config.iSiblingSeparation;
                    node.Modifier = node.Prelim - midPoint;
                    _apportion(node, level);
                }
                else
                {
                    node.Prelim = midPoint;
                }
            }
        }
        public void _apportion(LayoutNode node, int level)
        {
            var firstChild = node._getFirstChild();
            var firstChildLeftNeighbor = firstChild.LeftNeighbor;
            var j = 1;
            for (var k = Config.iMaxDepth - level; firstChild != null && firstChildLeftNeighbor != null && j <= k; )
            {
                double modifierSumRight = 0;
                double modifierSumLeft = 0;
                var rightAncestor = firstChild;
                var leftAncestor = firstChildLeftNeighbor;
                for (var l = 0; l < j; l++)
                {
                    rightAncestor = rightAncestor.Parent;
                    leftAncestor = leftAncestor.Parent;
                    modifierSumRight += rightAncestor.Modifier;
                    modifierSumLeft += leftAncestor.Modifier;
                }

                var totalGap = (firstChildLeftNeighbor.Prelim + modifierSumLeft + GetNodeSize(firstChildLeftNeighbor) + Config.iSubtreeSeparation) - (firstChild.Prelim + modifierSumRight);
                if (totalGap > 0)
                {
                    var subtreeAux = node;
                    var numSubtrees = 0;
                    for (; subtreeAux != null && subtreeAux != leftAncestor; subtreeAux = subtreeAux._getLeftSibling())
                        numSubtrees++;

                    if (subtreeAux != null)
                    {
                        var subtreeMoveAux = node;
                        var singleGap = totalGap / numSubtrees;
                        for (; subtreeMoveAux != leftAncestor; subtreeMoveAux = subtreeMoveAux._getLeftSibling())
                        {
                            subtreeMoveAux.Prelim += totalGap;
                            subtreeMoveAux.Modifier += totalGap;
                            totalGap -= singleGap;
                        }

                    }
                }
                j++;
                if (firstChild._getChildrenCount() == 0)
                    firstChild = _GetLeftmost(node, 0, j);
                else
                    firstChild = firstChild._getFirstChild();
                if (firstChild != null)
                    firstChildLeftNeighbor = firstChild.LeftNeighbor;
            }
        }
        public void _secondWalk(TreeLayout tree, LayoutNode node, int level, double X, double Y)
        {
            if (level <= tree.Config.iMaxDepth)
            {
                var xTmp = tree.RootXOffset + node.Prelim + X;
                var yTmp = tree.RootYOffset + Y;
                double maxsizeTmp = 0;
                double nodesizeTmp = 0;
                var flag = false;

                switch (tree.Config.iRootOrientation)
                {
                    case TreeLayout.RO_TOP:
                    case TreeLayout.RO_BOTTOM:
                        maxsizeTmp = tree.MaxLevelHeight[level];
                        nodesizeTmp = node.H;
                        break;

                    case TreeLayout.RO_RIGHT:
                    case TreeLayout.RO_LEFT:
                        maxsizeTmp = tree.MaxLevelWidth[level];
                        flag = true;
                        nodesizeTmp = node.W;
                        break;
                }
                switch (tree.Config.iNodeJustification)
                {
                    case TreeLayout.NJ_TOP:
                        node.X = xTmp;
                        node.Y = yTmp;
                        break;

                    case TreeLayout.NJ_CENTER:
                        node.X = xTmp;
                        node.Y = yTmp + (maxsizeTmp - nodesizeTmp) / 2;
                        break;

                    case TreeLayout.NJ_BOTTOM:
                        node.X = xTmp;
                        node.Y = (yTmp + maxsizeTmp) - nodesizeTmp;
                        break;
                }
                if (flag)
                {
                    var swapTmp = node.X;
                    node.X = node.Y;
                    node.Y = swapTmp;
                }
                switch (tree.Config.iRootOrientation)
                {
                    case TreeLayout.RO_BOTTOM:
                        node.Y = -node.Y - nodesizeTmp;
                        break;

                    case TreeLayout.RO_RIGHT:
                        node.X = -node.X - nodesizeTmp;
                        break;
                }
                if (node._getChildrenCount() != 0)
                    _secondWalk(tree, node._getFirstChild(), level + 1, X + node.Modifier, Y + maxsizeTmp + tree.Config.iLevelSeparation);
                var rightSibling = node._getRightSibling();
                if (rightSibling != null)
                    _secondWalk(tree, rightSibling, level, X, Y);
            }
        }

        public void UpdateTree()
        {
            this.MaxLevelHeight = new double[100];
            this.MaxLevelWidth = new double[100];
            this.PreviousLevelNode = new LayoutNode[100];
            FirstWalk(Root, 0);

            switch (this.Config.iRootOrientation)
            {
                case TreeLayout.RO_TOP:
                case TreeLayout.RO_LEFT:
                    this.RootXOffset = this.Config.topXAdjustment + Root.X;
                    this.RootYOffset = this.Config.topYAdjustment + Root.Y;
                    break;

                case TreeLayout.RO_BOTTOM:
                case TreeLayout.RO_RIGHT:
                    this.RootXOffset = this.Config.topXAdjustment + this.Root.X;
                    this.RootYOffset = this.Config.topYAdjustment + this.Root.Y;
                    break;
            }

            _secondWalk(this, this.Root, 0, 0, 0);
            //
            foreach (var node in Nodes)
                OnLayoutNodeChanged(node);
            foreach (var edge in Edges)
            {
                edge.Update(this);
                OnLayoutEdgeChanged(edge);
            }
        }

        void _setLevelHeight(LayoutNode node, int level)
        {
            if (this.MaxLevelHeight[level] < node.H)
                this.MaxLevelHeight[level] = node.H;
        }

        void _setLevelWidth(LayoutNode node, int level)
        {
            if (this.MaxLevelWidth[level] < node.W)
                this.MaxLevelWidth[level] = node.W;
        }

        void _setNeighbors(LayoutNode node, int level)
        {
            node.LeftNeighbor = this.PreviousLevelNode[level];
            if (node.LeftNeighbor != null)
                node.LeftNeighbor.RightNeighbor = node;
            this.PreviousLevelNode[level] = node;
        }

        public double GetNodeSize(LayoutNode node)
        {
            switch (this.Config.iRootOrientation)
            {
                case TreeLayout.RO_TOP:
                case TreeLayout.RO_BOTTOM:
                    return node.W;

                case TreeLayout.RO_RIGHT:
                case TreeLayout.RO_LEFT:
                    return node.H;
            }
            return 0;
        }

        LayoutNode _GetLeftmost(LayoutNode node, int level, int maxlevel)
        {
            if (level >= maxlevel) return node;
            if (node._getChildrenCount() == 0) return null;

            var n = node._getChildrenCount();
            for (var i = 0; i < n; i++)
            {
                var iChild = node._getChildAt(i);
                var leftmostDescendant = this._GetLeftmost(iChild, level + 1, maxlevel);
                if (leftmostDescendant != null)
                    return leftmostDescendant;
            }

            return null;
        }

        void _CollapseAllInt(bool flag)
        {
            LayoutNode node = null;
            for (var n = 0; n < this.Nodes.Count; n++)
            {
                node = this.Nodes[n];
                if (node.CanCollapse)
                    node.Collapsed = flag;
            }
            this.UpdateTree();
        }

        public void CreateNode(string id, object element, double w, double h)
        {
            var node = new LayoutNode(this, id, element, w, h);	//New node creation...
            NodeMap[id] = node;
            Nodes.Add(node);
        }

        public void CreateEdge(string srcId, string tgtId, object element)
        {
            LayoutNode source = NodeMap[srcId];
            if (source.Parent == null)
                Root = source;
            source.CanCollapse = true;
            LayoutNode target = NodeMap[tgtId];
            target.Parent = source;
            source.Children.Add(target);
            LayoutEdge edge = new LayoutEdge(this, source, target, element);
            source.Edges.Add(edge);
            Edges.Add(edge);
        }

        public void CollapseAll()
        {
            this._CollapseAllInt(true);
        }

        public void ExpandAll()
        {
            this._CollapseAllInt(false);
        }

        public void ToggleChildrenVisibility(string nodeid, bool upd)
        {
            LayoutNode node = NodeMap[nodeid];
            node.ToggleChildren();
            if (upd)
                UpdateTree();
        }
        //TODO:Delete this one?
        public void CollapseNode(string nodeid, bool upd)
        {
            LayoutNode node = NodeMap[nodeid];
            node.Collapsed = !node.Collapsed;
            if (upd) 
                UpdateTree();
        }


        public List<LayoutNode> GetSelectedNodes()
        {
            LayoutNode node = null;
            List<LayoutNode> selection = new List<LayoutNode>();
            LayoutNode selnode = null;

            for (var n = 0; n < this.Nodes.Count; n++)
            {
                node = this.Nodes[n];
                if (node.IsSelected)
                {
                    selnode = node;
                    selection[selection.Count] = selnode;
                }
            }
            return selection;
        }

    }
}
