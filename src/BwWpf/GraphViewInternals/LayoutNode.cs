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
    public class LayoutNode : LayoutAtom
    {
        public string Id;

        public double X;
        public double Y;
        public double W;
        public double H;
        public double CenterX { get { return X + W * .5; } }
        public double CenterY { get { return Y + H * .5; } }

        public double Prelim;
        public double Modifier;

        public LayoutNode LeftNeighbor;
        public LayoutNode RightNeighbor;
        public LayoutNode Parent;
        public List<LayoutEdge> Edges = new List<LayoutEdge>();
        public List<LayoutNode> Children = new List<LayoutNode>();

        public bool CanCollapse;
        public bool ChildrenCollapsed;
        public bool IsSelected;
        //
        public LayoutNode(TreeLayout tree, string id, object element, double w, double h)
        {
            Tree = tree;
            Id = id;
            Element = element;
            W = w;
            H = h;
        }
        public void ToggleChildren()
        {
            ChildrenCollapsed = !ChildrenCollapsed;
            foreach (var edge in Edges)
                edge.Toggle();
        }
        public override void Toggle()
        {
            base.Toggle();
        }
        public int _getLevel()
        {
            if (this.Parent.Id == null) { return 0; }
            else return this.Parent._getLevel() + 1;
        }
        public bool _isAncestorCollapsed()
        {
            if (this.Parent.Collapsed) { return true; }
            else
            {
                if (this.Parent.Id == null) { return false; }
                else { return this.Parent._isAncestorCollapsed(); }
            }
        }
        public void _setAncestorsExpanded()
        {
            if (this.Parent.Id == null) { return; }
            else
            {
                this.Parent.Collapsed = false;
                this.Parent._setAncestorsExpanded();
            }
        }
        public int _getChildrenCount()
        {
            if (this.Collapsed) return 0;
            if (this.Children == null)
                return 0;
            else
                return this.Children.Count;
        }
        public LayoutNode _getLeftSibling()
        {
            if (LeftNeighbor != null && LeftNeighbor.Parent == Parent)
                return LeftNeighbor;
            else
                return null;
        }
        public LayoutNode _getRightSibling()
        {
            if (RightNeighbor != null && RightNeighbor.Parent == Parent)
                return RightNeighbor;
            else
                return null;
        }
        public LayoutNode _getChildAt(int i)
        {
            return this.Children[i];
        }
        public double _getChildrenCenter(TreeLayout tree)
        {
            LayoutNode node = this._getFirstChild();
            LayoutNode node1 = this._getLastChild();
            return node.Prelim + ((node1.Prelim - node.Prelim) + tree.GetNodeSize(node1)) / 2;
        }
        public LayoutNode _getFirstChild()
        {
            return this._getChildAt(0);
        }
        public LayoutNode _getLastChild()
        {
            return this._getChildAt(this._getChildrenCount() - 1);
        }
    }
}
