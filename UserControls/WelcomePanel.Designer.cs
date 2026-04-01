namespace csharp_editor.UserControls {
    partial class WelcomePanel {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent() {
            components       = new System.ComponentModel.Container();
            sidePanel        = new System.Windows.Forms.Panel();
            lblTitle         = new System.Windows.Forms.Label();
            lblSubtitle      = new System.Windows.Forms.Label();
            sideSeparator    = new System.Windows.Forms.Panel();
            btnNew           = new System.Windows.Forms.Button();
            btnOpen          = new System.Windows.Forms.Button();
            divider          = new System.Windows.Forms.Panel();
            rightPanel       = new System.Windows.Forms.Panel();
            splitContainer   = new System.Windows.Forms.SplitContainer();
            lblHeader        = new System.Windows.Forms.Label();
            headerLine       = new System.Windows.Forms.Panel();
            contentPanel     = new System.Windows.Forms.Panel();
            _noRecentLabel   = new System.Windows.Forms.Label();
            _recentList      = new System.Windows.Forms.ListView();
            colPath          = new System.Windows.Forms.ColumnHeader();
            ctxMenu          = new System.Windows.Forms.ContextMenuStrip(components);
            mnuOpen          = new System.Windows.Forms.ToolStripMenuItem();
            mnuSeparator     = new System.Windows.Forms.ToolStripSeparator();
            mnuRemove        = new System.Windows.Forms.ToolStripMenuItem();
            lblMapsHeader    = new System.Windows.Forms.Label();
            mapsHeaderLine   = new System.Windows.Forms.Panel();
            mapsContentPanel = new System.Windows.Forms.Panel();
            _noRecentMapsLabel = new System.Windows.Forms.Label();
            _recentMapsList  = new System.Windows.Forms.ListView();
            colMapsPath      = new System.Windows.Forms.ColumnHeader();
            ctxMapsMenu      = new System.Windows.Forms.ContextMenuStrip(components);
            mnuMapsOpen      = new System.Windows.Forms.ToolStripMenuItem();
            mnuMapsSeparator = new System.Windows.Forms.ToolStripSeparator();
            mnuMapsRemove    = new System.Windows.Forms.ToolStripMenuItem();
            sidePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            rightPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            ctxMenu.SuspendLayout();
            mapsContentPanel.SuspendLayout();
            ctxMapsMenu.SuspendLayout();
            SuspendLayout();
            //
            // sidePanel
            //
            sidePanel.BackColor = System.Drawing.Color.FromArgb(250, 250, 252);
            sidePanel.Controls.Add(lblTitle);
            sidePanel.Controls.Add(lblSubtitle);
            sidePanel.Controls.Add(sideSeparator);
            sidePanel.Controls.Add(btnNew);
            sidePanel.Controls.Add(btnOpen);
            sidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            sidePanel.Name = "sidePanel";
            sidePanel.Width = 310;
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 17f, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(38, 40, 58);
            lblTitle.Location = new System.Drawing.Point(28, 52);
            lblTitle.Name = "lblTitle";
            lblTitle.Text = "csharp-editor";
            //
            // lblSubtitle
            //
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 104, 120);
            lblSubtitle.Location = new System.Drawing.Point(30, 87);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Text = "Map & Scene Editor";
            //
            // sideSeparator
            //
            sideSeparator.BackColor = System.Drawing.Color.FromArgb(220, 223, 232);
            sideSeparator.Location = new System.Drawing.Point(28, 118);
            sideSeparator.Name = "sideSeparator";
            sideSeparator.Size = new System.Drawing.Size(254, 1);
            //
            // btnNew
            //
            btnNew.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            btnNew.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNew.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 206, 218);
            btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(245, 247, 251);
            btnNew.Font = new System.Drawing.Font("Segoe UI", 10f);
            btnNew.ForeColor = System.Drawing.Color.FromArgb(38, 40, 58);
            btnNew.Location = new System.Drawing.Point(28, 140);
            btnNew.Name = "btnNew";
            btnNew.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            btnNew.Size = new System.Drawing.Size(254, 42);
            btnNew.Text = "＋  New Project";
            btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnNew.UseVisualStyleBackColor = false;
            btnNew.Click += btnNew_Click;
            //
            // btnOpen
            //
            btnOpen.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            btnOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnOpen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 206, 218);
            btnOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(245, 247, 251);
            btnOpen.Font = new System.Drawing.Font("Segoe UI", 10f);
            btnOpen.ForeColor = System.Drawing.Color.FromArgb(38, 40, 58);
            btnOpen.Location = new System.Drawing.Point(28, 194);
            btnOpen.Name = "btnOpen";
            btnOpen.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            btnOpen.Size = new System.Drawing.Size(254, 42);
            btnOpen.Text = "📂  Open Project";
            btnOpen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnOpen.UseVisualStyleBackColor = false;
            btnOpen.Click += btnOpen_Click;
            //
            // divider
            //
            divider.BackColor = System.Drawing.Color.FromArgb(208, 210, 224);
            divider.Dock = System.Windows.Forms.DockStyle.Left;
            divider.Name = "divider";
            divider.Width = 1;
            //
            // rightPanel
            //
            rightPanel.Controls.Add(splitContainer);
            rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            rightPanel.Name = "rightPanel";
            rightPanel.Padding = new System.Windows.Forms.Padding(36, 0, 36, 24);
            //
            // splitContainer
            //
            splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer.Name = "splitContainer";
            splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            splitContainer.SplitterDistance = 240;
            //
            // splitContainer.Panel1 — Recent Projects
            //
            splitContainer.Panel1.Controls.Add(contentPanel);
            splitContainer.Panel1.Controls.Add(headerLine);
            splitContainer.Panel1.Controls.Add(lblHeader);
            //
            // lblHeader
            //
            lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Bold);
            lblHeader.ForeColor = System.Drawing.Color.FromArgb(38, 40, 58);
            lblHeader.Height = 72;
            lblHeader.Name = "lblHeader";
            lblHeader.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            lblHeader.Text = "Recent Projects";
            lblHeader.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // headerLine
            //
            headerLine.BackColor = System.Drawing.Color.FromArgb(216, 218, 230);
            headerLine.Dock = System.Windows.Forms.DockStyle.Top;
            headerLine.Height = 1;
            headerLine.Name = "headerLine";
            //
            // contentPanel
            //
            contentPanel.Controls.Add(_noRecentLabel);
            contentPanel.Controls.Add(_recentList);
            contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            contentPanel.Name = "contentPanel";
            //
            // _noRecentLabel
            //
            _noRecentLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            _noRecentLabel.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Italic);
            _noRecentLabel.ForeColor = System.Drawing.Color.FromArgb(152, 156, 175);
            _noRecentLabel.Name = "noRecentLabel";
            _noRecentLabel.Padding = new System.Windows.Forms.Padding(2, 14, 0, 0);
            _noRecentLabel.Text = "No recent projects.\n\nUse  ＋ New Project  to create one, or  📂 Open Project  to browse.";
            _noRecentLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            _noRecentLabel.Visible = false;
            //
            // _recentList
            //
            _recentList.BackColor = System.Drawing.Color.FromArgb(248, 248, 250);
            _recentList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            _recentList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { colPath });
            _recentList.ContextMenuStrip = ctxMenu;
            _recentList.Dock = System.Windows.Forms.DockStyle.Fill;
            _recentList.Font = new System.Drawing.Font("Segoe UI", 10f);
            _recentList.FullRowSelect = true;
            _recentList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            _recentList.HideSelection = false;
            _recentList.MultiSelect = false;
            _recentList.Name = "recentList";
            _recentList.ShowItemToolTips = true;
            _recentList.View = System.Windows.Forms.View.Details;
            _recentList.MouseDoubleClick += recentList_MouseDoubleClick;
            //
            // colPath
            //
            colPath.Text = "Path";
            colPath.Width = -2;
            //
            // ctxMenu
            //
            ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                mnuOpen,
                mnuSeparator,
                mnuRemove
            });
            ctxMenu.Name = "ctxMenu";
            //
            // mnuOpen
            //
            mnuOpen.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            mnuOpen.Name = "mnuOpen";
            mnuOpen.Text = "Open";
            mnuOpen.Click += mnuOpen_Click;
            //
            // mnuSeparator
            //
            mnuSeparator.Name = "mnuSeparator";
            //
            // mnuRemove
            //
            mnuRemove.Name = "mnuRemove";
            mnuRemove.Text = "Remove from Recent";
            mnuRemove.Click += mnuRemove_Click;
            //
            // splitContainer.Panel2 — Recent Maps
            //
            splitContainer.Panel2.Controls.Add(mapsContentPanel);
            splitContainer.Panel2.Controls.Add(mapsHeaderLine);
            splitContainer.Panel2.Controls.Add(lblMapsHeader);
            //
            // lblMapsHeader
            //
            lblMapsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            lblMapsHeader.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Bold);
            lblMapsHeader.ForeColor = System.Drawing.Color.FromArgb(38, 40, 58);
            lblMapsHeader.Height = 52;
            lblMapsHeader.Name = "lblMapsHeader";
            lblMapsHeader.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            lblMapsHeader.Text = "Recent Maps";
            lblMapsHeader.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // mapsHeaderLine
            //
            mapsHeaderLine.BackColor = System.Drawing.Color.FromArgb(216, 218, 230);
            mapsHeaderLine.Dock = System.Windows.Forms.DockStyle.Top;
            mapsHeaderLine.Height = 1;
            mapsHeaderLine.Name = "mapsHeaderLine";
            //
            // mapsContentPanel
            //
            mapsContentPanel.Controls.Add(_noRecentMapsLabel);
            mapsContentPanel.Controls.Add(_recentMapsList);
            mapsContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mapsContentPanel.Name = "mapsContentPanel";
            //
            // _noRecentMapsLabel
            //
            _noRecentMapsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            _noRecentMapsLabel.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Italic);
            _noRecentMapsLabel.ForeColor = System.Drawing.Color.FromArgb(152, 156, 175);
            _noRecentMapsLabel.Name = "noRecentMapsLabel";
            _noRecentMapsLabel.Padding = new System.Windows.Forms.Padding(2, 14, 0, 0);
            _noRecentMapsLabel.Text = "No recent maps.\n\nOpen a map file (.json) to see it here.";
            _noRecentMapsLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            _noRecentMapsLabel.Visible = false;
            //
            // _recentMapsList
            //
            _recentMapsList.BackColor = System.Drawing.Color.FromArgb(248, 248, 250);
            _recentMapsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            _recentMapsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { colMapsPath });
            _recentMapsList.ContextMenuStrip = ctxMapsMenu;
            _recentMapsList.Dock = System.Windows.Forms.DockStyle.Fill;
            _recentMapsList.Font = new System.Drawing.Font("Segoe UI", 10f);
            _recentMapsList.FullRowSelect = true;
            _recentMapsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            _recentMapsList.HideSelection = false;
            _recentMapsList.MultiSelect = false;
            _recentMapsList.Name = "recentMapsList";
            _recentMapsList.ShowItemToolTips = true;
            _recentMapsList.View = System.Windows.Forms.View.Details;
            _recentMapsList.MouseDoubleClick += recentMapsList_MouseDoubleClick;
            //
            // colMapsPath
            //
            colMapsPath.Text = "Path";
            colMapsPath.Width = -2;
            //
            // ctxMapsMenu
            //
            ctxMapsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                mnuMapsOpen,
                mnuMapsSeparator,
                mnuMapsRemove
            });
            ctxMapsMenu.Name = "ctxMapsMenu";
            //
            // mnuMapsOpen
            //
            mnuMapsOpen.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            mnuMapsOpen.Name = "mnuMapsOpen";
            mnuMapsOpen.Text = "Open";
            mnuMapsOpen.Click += mnuMapsOpen_Click;
            //
            // mnuMapsSeparator
            //
            mnuMapsSeparator.Name = "mnuMapsSeparator";
            //
            // mnuMapsRemove
            //
            mnuMapsRemove.Name = "mnuMapsRemove";
            mnuMapsRemove.Text = "Remove from Recent";
            mnuMapsRemove.Click += mnuMapsRemove_Click;
            //
            // WelcomePanel
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(248, 248, 250);
            Controls.Add(rightPanel);
            Controls.Add(divider);
            Controls.Add(sidePanel);
            Dock = System.Windows.Forms.DockStyle.Fill;
            Name = "WelcomePanel";
            sidePanel.ResumeLayout(false);
            sidePanel.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            rightPanel.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            ctxMenu.ResumeLayout(false);
            mapsContentPanel.ResumeLayout(false);
            ctxMapsMenu.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel           sidePanel;
        private System.Windows.Forms.Label           lblTitle;
        private System.Windows.Forms.Label           lblSubtitle;
        private System.Windows.Forms.Panel           sideSeparator;
        private System.Windows.Forms.Button          btnNew;
        private System.Windows.Forms.Button          btnOpen;
        private System.Windows.Forms.Panel           divider;
        private System.Windows.Forms.Panel           rightPanel;
        private System.Windows.Forms.SplitContainer  splitContainer;
        private System.Windows.Forms.Label           lblHeader;
        private System.Windows.Forms.Panel           headerLine;
        private System.Windows.Forms.Panel           contentPanel;
        private System.Windows.Forms.Label           _noRecentLabel;
        private System.Windows.Forms.ListView        _recentList;
        private System.Windows.Forms.ColumnHeader    colPath;
        private System.Windows.Forms.ContextMenuStrip   ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem  mnuOpen;
        private System.Windows.Forms.ToolStripSeparator mnuSeparator;
        private System.Windows.Forms.ToolStripMenuItem  mnuRemove;
        private System.Windows.Forms.Label           lblMapsHeader;
        private System.Windows.Forms.Panel           mapsHeaderLine;
        private System.Windows.Forms.Panel           mapsContentPanel;
        private System.Windows.Forms.Label           _noRecentMapsLabel;
        private System.Windows.Forms.ListView        _recentMapsList;
        private System.Windows.Forms.ColumnHeader    colMapsPath;
        private System.Windows.Forms.ContextMenuStrip   ctxMapsMenu;
        private System.Windows.Forms.ToolStripMenuItem  mnuMapsOpen;
        private System.Windows.Forms.ToolStripSeparator mnuMapsSeparator;
        private System.Windows.Forms.ToolStripMenuItem  mnuMapsRemove;
    }
}
