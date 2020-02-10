using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Botworx.Wpf
{
    public class GraphViewItem : TreeViewItem
    {
        public GraphView View;
        //
        public GraphViewItem()
        {
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(GraphViewItemCanvas));
            this.ItemsPanel = new ItemsPanelTemplate(factory);
        }
        protected override void AddChild(object value)
        {
            base.AddChild(value);
        }
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is GraphViewItem);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return View.CreateItem();
        }
    }
    public class GraphViewItemPresenter : ItemsPresenter
    {
    }
}
