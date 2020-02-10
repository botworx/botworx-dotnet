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

using Botworx.Wpf.GraphViewInternals;

namespace Botworx.Wpf
{
    public class GraphView : ItemsControl
    {
        TreeLayout Layout;
        //
        public GraphView()
        {
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(GraphViewCanvas));
            this.ItemsPanel = new ItemsPanelTemplate(factory);
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is GraphViewItem);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return CreateItem();
        }
        public GraphViewItem CreateItem()
        {
            GraphViewItem item = new GraphViewItem();
            //Layout.CreateNode(item.Tag, item, item.Width, item.Height);
            item.View = this;
            return item;
        }
    }
}
