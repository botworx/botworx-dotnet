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
    public partial class AgentForm : Form
    {
        public AgentModel Model;
        //
        public AgentForm()
        {
            InitializeComponent();
        }
        public void OpenModel(string name)
        {
            Text = name;
            Model = new AgentModel();
            Model.Open(name);
            //
            /*var form = new ContextExplorer();
            form.MdiParent = MdiParent;
            form.Present(Model);*/
            new ContextExplorerPresenter(Model, ContextGraphPanel, ClauseListBox);
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            Model.Brain.Run();
        }
    }
}
