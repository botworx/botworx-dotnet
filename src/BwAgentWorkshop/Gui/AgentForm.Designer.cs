namespace Botworx.AgentLib.ClientLib.Workshop.Gui
{
    partial class AgentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentForm));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ConsolePage = new System.Windows.Forms.TabPage();
            this.ConsoleTextBox = new System.Windows.Forms.TextBox();
            this.ExplorerPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.playButton = new System.Windows.Forms.ToolStripButton();
            this.ContextGraphPanel = new Botworx.AgentLib.ClientLib.Workshop.Gui.ContextGraphPanel();
            this.ClauseListBox = new System.Windows.Forms.ListBox();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.ConsolePage.SuspendLayout();
            this.ExplorerPage.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tabControl1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(462, 355);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(462, 380);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.ConsolePage);
            this.tabControl1.Controls.Add(this.ExplorerPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(462, 355);
            this.tabControl1.TabIndex = 0;
            // 
            // ConsolePage
            // 
            this.ConsolePage.Controls.Add(this.ConsoleTextBox);
            this.ConsolePage.Location = new System.Drawing.Point(4, 22);
            this.ConsolePage.Name = "ConsolePage";
            this.ConsolePage.Padding = new System.Windows.Forms.Padding(3);
            this.ConsolePage.Size = new System.Drawing.Size(454, 329);
            this.ConsolePage.TabIndex = 0;
            this.ConsolePage.Text = "Console";
            this.ConsolePage.UseVisualStyleBackColor = true;
            // 
            // ConsoleTextBox
            // 
            this.ConsoleTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsoleTextBox.Location = new System.Drawing.Point(3, 3);
            this.ConsoleTextBox.Multiline = true;
            this.ConsoleTextBox.Name = "ConsoleTextBox";
            this.ConsoleTextBox.Size = new System.Drawing.Size(448, 323);
            this.ConsoleTextBox.TabIndex = 0;
            // 
            // ExplorerPage
            // 
            this.ExplorerPage.Controls.Add(this.splitContainer1);
            this.ExplorerPage.Location = new System.Drawing.Point(4, 22);
            this.ExplorerPage.Name = "ExplorerPage";
            this.ExplorerPage.Padding = new System.Windows.Forms.Padding(3);
            this.ExplorerPage.Size = new System.Drawing.Size(454, 329);
            this.ExplorerPage.TabIndex = 1;
            this.ExplorerPage.Text = "Explorer";
            this.ExplorerPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ClauseListBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ContextGraphPanel);
            this.splitContainer1.Size = new System.Drawing.Size(448, 323);
            this.splitContainer1.SplitterDistance = 149;
            this.splitContainer1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(35, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // playButton
            // 
            this.playButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.playButton.Image = ((System.Drawing.Image)(resources.GetObject("playButton.Image")));
            this.playButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(23, 22);
            this.playButton.Text = "Play";
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // ContextGraphPanel
            // 
            this.ContextGraphPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContextGraphPanel.Location = new System.Drawing.Point(0, 0);
            this.ContextGraphPanel.Name = "ContextGraphPanel";
            this.ContextGraphPanel.Size = new System.Drawing.Size(295, 323);
            this.ContextGraphPanel.TabIndex = 2;
            // 
            // ClauseListBox
            // 
            this.ClauseListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClauseListBox.FormattingEnabled = true;
            this.ClauseListBox.Location = new System.Drawing.Point(0, 0);
            this.ClauseListBox.Name = "ClauseListBox";
            this.ClauseListBox.Size = new System.Drawing.Size(149, 316);
            this.ClauseListBox.TabIndex = 0;
            // 
            // AgentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 380);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "AgentForm";
            this.Text = "AgentForm";
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ConsolePage.ResumeLayout(false);
            this.ConsolePage.PerformLayout();
            this.ExplorerPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton playButton;
        private System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.TabPage ConsolePage;
        public System.Windows.Forms.TabPage ExplorerPage;
        public System.Windows.Forms.TextBox ConsoleTextBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public ContextGraphPanel ContextGraphPanel;
        private System.Windows.Forms.ListBox ClauseListBox;
    }
}