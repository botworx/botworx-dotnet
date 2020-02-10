using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
//using System.Windows.Controls;
using Wpf = System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;


namespace Botworx.AgentLib.ClientLib.Workshop.Gui
{
    class ContextExplorerPresenter : Presenter
    {
        AgentModel Model;
        ContextGraphPanel Control;
        ContextGraphPanelWpf Panel;
        ContextGraphCanvas Canvas;
        ListBox ClauseListView;
        //
        Wpf.ScrollViewer CanvasViewer;
        Wpf.Slider CanvasZoomer;
        //
        Dictionary<Guid, TreeCanvasNode> Dictionary = new Dictionary<Guid, TreeCanvasNode>();
        //
        public ContextExplorerPresenter(AgentModel model, ContextGraphPanel control, ListBox listView)
            : base(model)
        {
            Model = model;
            Control = control;
            //
            ClauseListView = listView;
            /*ClauseListView.Items.Add("one");
            ClauseListView.Items.Add("two");
            ClauseListView.Items.Add("three");*/
            //
            Panel = new ContextGraphPanelWpf();
            control.ElementHost.Child = Panel;
            Canvas = (ContextGraphCanvas)Panel.FindName("graphCanvas");
            CanvasViewer = (Wpf.ScrollViewer)Panel.FindName("canvasViewer");
            CanvasZoomer = (Wpf.Slider)Panel.FindName("canvasZoomer");
            CanvasZoomer.ValueChanged += OnCanvasZoomerValue;
            //
            model.Brain.ObserveContextCreated(OnContextCreated);
        }
        public override void Refresh(bool totalRefresh)
        {
            Canvas.Clear();
            Build();
        }
        private delegate void InvokeDelegate();
        private void OnContextCreated(ProcessProxy context)
        {
            if (context.Parent == null)
            {
                Dictionary[context.Guid] = CreateNode(null, context);
                return;
            }
            //else
            TreeCanvasNode parentNode = Dictionary[context.Parent.Guid];
            Dictionary[context.Guid] = CreateNode(parentNode, context);
        }
        private void OnCanvasZoomerValue(object sender, EventArgs e)
        {
            Canvas.LayoutTransform = new ScaleTransform(CanvasZoomer.Value, CanvasZoomer.Value);
        }
        //
        public void Build()
        {
            if (Model.Brain == null)
                return;
        }
        public TreeCanvasNode CreateNode(TreeCanvasNode parent, ProcessProxy context)
        {
            if (context == null)
                return null;
            //else
            System.Windows.Controls.Button control = new System.Windows.Controls.Button();
            control.Content = context.Label;
            control.Click += (s, e) => InspectContext(context);
            //
            TreeCanvasNode canvasNode;
            if (parent == null)
                canvasNode = Canvas.AddRoot(control);
            else
                canvasNode = Canvas.AddNode(control, parent);
            return canvasNode;
        }
        public void InspectContext(ProcessProxy context)
        {
            ClauseListView.Items.Clear();
            List<string> clauses = context.GetClauses();
            foreach (string clause in clauses)
            {
                ClauseListView.Items.Add(clause);
            }
        }
    }
}
