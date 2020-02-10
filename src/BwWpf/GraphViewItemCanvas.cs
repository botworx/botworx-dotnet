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
    public class GraphViewItemCanvas : Canvas
    {
        public GraphViewItemCanvas()
        {
        }
        // Override the default Measure method of Panel 
        protected override Size MeasureOverride(Size availableSize)
        {
            Size panelDesiredSize = new Size();

            // In our example, we just have one child.  
            // Report that our panel requires just the size of its only child. 
            foreach (UIElement child in InternalChildren)
            {
                child.Measure(availableSize);
                panelDesiredSize = child.DesiredSize;
            }

            return panelDesiredSize;
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                double x = 50;
                double y = 50;

                child.Arrange(new Rect(new Point(x, y), child.DesiredSize));
            }
            return finalSize; // Returns the final Arranged size
        }
    }
}
