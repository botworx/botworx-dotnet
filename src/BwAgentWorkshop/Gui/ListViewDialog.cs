using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Botworx.AgentLib.ClientLib.Workshop.Gui
{
    public partial class ListViewDialog : Form
    {
        public string ItemName;

        public ListViewDialog()
        {
            InitializeComponent();
        }

        private void ItemActivated(object sender, EventArgs e)
        {
            ItemName = ListView.SelectedItems[0].Text;
            Close();
        }
    }
}
