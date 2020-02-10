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

namespace Botworx.Wpf.Test
{
    /// <summary>
    /// Interaction logic for TreeViewTestPage.xaml
    /// </summary>
    public partial class TreeViewTestPage : Page
    {
        public TreeViewTestPage()
        {
            InitializeComponent();
            var provider = (XmlDataProvider)this.Resources["xmlDataProvider"];
            //provider.Source = new Uri(@"../../TreeViewTestPage.xaml", UriKind.Relative);
        }
    }
}
