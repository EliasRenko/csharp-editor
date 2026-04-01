using csharp_editor.Helpers;

namespace csharp_editor.UserControls {
    /// <summary>
    /// Welcome / start screen shown when no project is currently open.
    /// Lets the user create a new project (with a path picker) or open an
    /// existing one, and shows the most-recently-used project list.
    /// </summary>
    public sealed partial class WelcomePanel : UserControl {

        // ── Events ────────────────────────────────────────────────
        /// <summary>Fired when the user picks a save path for a brand-new project.</summary>
        public event EventHandler<string>? NewProjectRequested;
        /// <summary>Fired when the user wants to open a project file.</summary>
        public event EventHandler<string>? OpenProjectRequested;
        /// <summary>Fired when the user double-clicks or opens a map from the recent-maps list.</summary>
        public event EventHandler<string>? OpenMapRequested;

        // ── Constructor ───────────────────────────────────────────
        public WelcomePanel() {
            InitializeComponent();
            RefreshRecent();
            RefreshRecentMaps();
        }

        // ── Event Handlers ────────────────────────────────────────
        private void btnNew_Click(object sender, EventArgs e) {
            using var dlg = new SaveFileDialog {
                Title        = "Create New Project",
                Filter       = "JSON Project (*.json)|*.json|All Files (*.*)|*.*",
                DefaultExt   = "json",
                FileName     = "NewProject",
                AddExtension = true
            };
            if (dlg.ShowDialog() == DialogResult.OK)
                NewProjectRequested?.Invoke(this, dlg.FileName);
        }

        private void btnOpen_Click(object sender, EventArgs e) {
            using var dlg = new OpenFileDialog {
                Title  = "Open Project",
                Filter = "JSON Project (*.json)|*.json|All Files (*.*)|*.*"
            };
            if (dlg.ShowDialog() == DialogResult.OK)
                OpenProjectRequested?.Invoke(this, dlg.FileName);
        }

        private void recentList_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (_recentList.SelectedItems.Count > 0)
                OpenProjectRequested?.Invoke(this, (string)_recentList.SelectedItems[0].Tag!);
        }

        private void mnuOpen_Click(object sender, EventArgs e) {
            if (_recentList.SelectedItems.Count > 0)
                OpenProjectRequested?.Invoke(this, (string)_recentList.SelectedItems[0].Tag!);
        }

        private void mnuRemove_Click(object sender, EventArgs e) {
            if (_recentList.SelectedItems.Count > 0) {
                RecentProjectsManager.Remove((string)_recentList.SelectedItems[0].Tag!);
                RefreshRecent();
            }
        }

        private void recentMapsList_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (_recentMapsList.SelectedItems.Count > 0)
                OpenMapRequested?.Invoke(this, (string)_recentMapsList.SelectedItems[0].Tag!);
        }

        private void mnuMapsOpen_Click(object sender, EventArgs e) {
            if (_recentMapsList.SelectedItems.Count > 0)
                OpenMapRequested?.Invoke(this, (string)_recentMapsList.SelectedItems[0].Tag!);
        }

        private void mnuMapsRemove_Click(object sender, EventArgs e) {
            if (_recentMapsList.SelectedItems.Count > 0) {
                RecentMapsManager.Remove((string)_recentMapsList.SelectedItems[0].Tag!);
                RefreshRecentMaps();
            }
        }

        // ── Public API ────────────────────────────────────────────────
        /// <summary>Reloads the recent-projects list from disk and updates the UI.</summary>
        public void RefreshRecent() {
            _recentList.Items.Clear();
            var paths = RecentProjectsManager.Load();
            foreach (var p in paths) {
                var item = new ListViewItem(p) { Tag = p, ToolTipText = p };
                _recentList.Items.Add(item);
            }
            bool any = paths.Count > 0;
            _recentList.Visible    = any;
            _noRecentLabel.Visible = !any;

            // Resize the single column to fill the list width
            if (any && _recentList.Columns.Count > 0)
                _recentList.Columns[0].Width = -2;
        }

        /// <summary>Reloads the recent-maps list from disk and updates the UI.</summary>
        public void RefreshRecentMaps() {
            _recentMapsList.Items.Clear();
            var paths = RecentMapsManager.Load();
            foreach (var p in paths) {
                var item = new ListViewItem(p) { Tag = p, ToolTipText = p };
                _recentMapsList.Items.Add(item);
            }
            bool any = paths.Count > 0;
            _recentMapsList.Visible       = any;
            _noRecentMapsLabel.Visible    = !any;
            if (any && _recentMapsList.Columns.Count > 0)
                _recentMapsList.Columns[0].Width = -2;
        }
    }
}
