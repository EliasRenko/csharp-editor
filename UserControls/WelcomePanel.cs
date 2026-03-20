using csharp_editor.Helpers;

namespace csharp_editor.UserControls {
    /// <summary>
    /// Welcome / start screen shown when no project is currently open.
    /// Lets the user create a new project (with a path picker) or open an
    /// existing one, and shows the most-recently-used project list.
    /// </summary>
    public sealed class WelcomePanel : UserControl {

        // ── Events ────────────────────────────────────────────────────────
        /// <summary>Fired when the user picks a save path for a brand-new project.</summary>
        public event EventHandler<string>? NewProjectRequested;
        /// <summary>Fired when the user wants to open a project file.</summary>
        public event EventHandler<string>? OpenProjectRequested;

        // ── Controls ──────────────────────────────────────────────────────
        private readonly ListView _recentList;
        private readonly Label    _noRecentLabel;

        // ── Constructor ───────────────────────────────────────────────────
        public WelcomePanel() {
            Dock      = DockStyle.Fill;
            BackColor = Color.FromArgb(248, 248, 250);

            // ── Left sidebar (dark) ───────────────────────────────────────
            var sidePanel = new Panel {
                Dock      = DockStyle.Left,
                Width     = 310,
                BackColor = Color.FromArgb(32, 32, 44)
            };

            sidePanel.Controls.Add(new Label {
                Text      = "csharp-editor",
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 17f, FontStyle.Bold),
                Location  = new Point(28, 52),
                AutoSize  = true
            });

            sidePanel.Controls.Add(new Label {
                Text      = "Map & Scene Editor",
                ForeColor = Color.FromArgb(138, 142, 165),
                Font      = new Font("Segoe UI", 9.5f),
                Location  = new Point(30, 87),
                AutoSize  = true
            });

            sidePanel.Controls.Add(new Panel {       // thin separator line
                Location  = new Point(28, 118),
                Size      = new Size(254, 1),
                BackColor = Color.FromArgb(54, 54, 70)
            });

            var btnNew  = MakeSideButton("＋  New Project",  new Point(28, 140), primary: true);
            var btnOpen = MakeSideButton("📂  Open Project", new Point(28, 194), primary: false);
            sidePanel.Controls.Add(btnNew);
            sidePanel.Controls.Add(btnOpen);

            btnNew.Click += (_, _) => {
                using var dlg = new SaveFileDialog {
                    Title        = "Create New Project",
                    Filter       = "JSON Project (*.json)|*.json|All Files (*.*)|*.*",
                    DefaultExt   = "json",
                    FileName     = "NewProject",
                    AddExtension = true
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                    NewProjectRequested?.Invoke(this, dlg.FileName);
            };

            btnOpen.Click += (_, _) => {
                using var dlg = new OpenFileDialog {
                    Title  = "Open Project",
                    Filter = "JSON Project (*.json)|*.json|All Files (*.*)|*.*"
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                    OpenProjectRequested?.Invoke(this, dlg.FileName);
            };

            // ── Divider ───────────────────────────────────────────────────
            var divider = new Panel {
                Dock      = DockStyle.Left,
                Width     = 1,
                BackColor = Color.FromArgb(208, 210, 224)
            };

            // ── Right area (recent projects) ──────────────────────────────
            var rightPanel = new Panel {
                Dock    = DockStyle.Fill,
                Padding = new Padding(36, 0, 36, 24)
            };

            var lblHeader = new Label {
                Text      = "Recent Projects",
                Font      = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.FromArgb(38, 40, 58),
                Dock      = DockStyle.Top,
                Height    = 72,
                TextAlign = ContentAlignment.BottomLeft,
                Padding   = new Padding(0, 0, 0, 6)
            };

            var headerLine = new Panel {
                Dock      = DockStyle.Top,
                Height    = 1,
                BackColor = Color.FromArgb(216, 218, 230)
            };

            // Container so both list and empty-label occupy the same region
            var contentPanel = new Panel { Dock = DockStyle.Fill };

            _noRecentLabel = new Label {
                Text      = "No recent projects.\n\nUse  ＋ New Project  to create one, or  📂 Open Project  to browse.",
                ForeColor = Color.FromArgb(152, 156, 175),
                Font      = new Font("Segoe UI", 9.5f, FontStyle.Italic),
                Dock      = DockStyle.Fill,
                TextAlign = ContentAlignment.TopLeft,
                Padding   = new Padding(2, 14, 0, 0),
                Visible   = false
            };

            _recentList = new ListView {
                Dock          = DockStyle.Fill,
                View          = View.Details,
                FullRowSelect = true,
                HeaderStyle   = ColumnHeaderStyle.None,
                BorderStyle   = BorderStyle.None,
                Font          = new Font("Segoe UI", 10f),
                BackColor     = Color.FromArgb(248, 248, 250),
                HideSelection = false,
                MultiSelect   = false,
                ShowItemToolTips = true
            };
            _recentList.Columns.Add("Path", -2);

            _recentList.MouseDoubleClick += (_, _) => {
                if (_recentList.SelectedItems.Count > 0)
                    OpenProjectRequested?.Invoke(this, (string)_recentList.SelectedItems[0].Tag!);
            };

            // Context menu: open or remove
            var ctx    = new ContextMenuStrip();
            var mnuOpen = new ToolStripMenuItem("Open");
            mnuOpen.Font = new Font(mnuOpen.Font, FontStyle.Bold);
            mnuOpen.Click += (_, _) => {
                if (_recentList.SelectedItems.Count > 0)
                    OpenProjectRequested?.Invoke(this, (string)_recentList.SelectedItems[0].Tag!);
            };
            var mnuRemove = new ToolStripMenuItem("Remove from Recent");
            mnuRemove.Click += (_, _) => {
                if (_recentList.SelectedItems.Count > 0) {
                    RecentProjectsManager.Remove((string)_recentList.SelectedItems[0].Tag!);
                    RefreshRecent();
                }
            };
            ctx.Items.Add(mnuOpen);
            ctx.Items.Add(new ToolStripSeparator());
            ctx.Items.Add(mnuRemove);
            _recentList.ContextMenuStrip = ctx;

            // Populate contentPanel — both Fill, one visible at a time
            contentPanel.Controls.Add(_noRecentLabel);
            contentPanel.Controls.Add(_recentList);

            // Populate rightPanel — Top items first, Fill last
            rightPanel.Controls.Add(lblHeader);
            rightPanel.Controls.Add(headerLine);
            rightPanel.Controls.Add(contentPanel);

            // Root — Left controls first, Fill last
            Controls.Add(sidePanel);
            Controls.Add(divider);
            Controls.Add(rightPanel);

            RefreshRecent();
        }

        // ── Public API ────────────────────────────────────────────────────
        /// <summary>Reloads the recent-projects list from disk and updates the UI.</summary>
        public void RefreshRecent() {
            _recentList.Items.Clear();
            var paths = RecentProjectsManager.Load();
            foreach (var p in paths) {
                var item = new ListViewItem(p) { Tag = p, ToolTipText = p };
                _recentList.Items.Add(item);
            }
            bool any = paths.Count > 0;
            _recentList.Visible      = any;
            _noRecentLabel.Visible   = !any;

            // Resize the single column to fill the list width
            if (any && _recentList.Columns.Count > 0)
                _recentList.Columns[0].Width = -2;
        }

        // ── Helpers ───────────────────────────────────────────────────────
        private static Button MakeSideButton(string text, Point loc, bool primary) {
            var btn = new Button {
                Text                    = text,
                Location                = loc,
                Size                    = new Size(254, 42),
                FlatStyle               = FlatStyle.Flat,
                BackColor               = primary ? Color.FromArgb(0, 120, 215)
                                                  : Color.FromArgb(50, 50, 65),
                ForeColor               = Color.White,
                Font                    = new Font("Segoe UI", 10f),
                Cursor                  = Cursors.Hand,
                TextAlign               = ContentAlignment.MiddleLeft,
                Padding                 = new Padding(12, 0, 0, 0),
                UseVisualStyleBackColor = false
            };
            btn.FlatAppearance.BorderColor        = primary ? Color.FromArgb(0,  96, 195)
                                                             : Color.FromArgb(64, 64, 82);
            btn.FlatAppearance.MouseOverBackColor = primary ? Color.FromArgb(0, 102, 200)
                                                             : Color.FromArgb(62, 62, 80);
            return btn;
        }
    }
}
