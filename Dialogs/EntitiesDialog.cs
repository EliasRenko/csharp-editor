using System.Runtime.InteropServices;
using csharp_editor.Models;
using csharp_editor.UserControls;

namespace csharp_editor.Dialogs {
    public partial class EntitiesDialog : Form {

        public string SelectedEntityName { get; private set; } = "";

        private List<EntityEntry> _entities = new List<EntityEntry>();
        private ExternView _externView;
        private Action<string>? _onEntitySelected;
        private Action? _onEntityDefDeleted;

        public EntitiesDialog(ExternView externView, Action<string>? onEntitySelected = null, Action? onEntityDefDeleted = null) {
            InitializeComponent();
            _externView = externView;
            _onEntitySelected = onEntitySelected;
            _onEntityDefDeleted = onEntityDefDeleted;
            entityEditor.Initialize(_externView);
            entityEditor.SaveCompleted += (s, e) => LoadExistingEntities();
            LoadExistingEntities();
        }

        private void LoadExistingEntities() {
            listBoxEntities.Items.Clear();
            _entities.Clear();

            int count = CExternsEditor.GetEntityCount();
            for (int i = 0; i < count; i++) {
                CExternsEditor.EntityDefStruct entityDef = new CExternsEditor.EntityDefStruct();
                CExternsEditor.GetEntityAt(i, out entityDef);

                string name = Marshal.PtrToStringAnsi(entityDef.name) ?? "";
                string tilesetName = Marshal.PtrToStringAnsi(entityDef.tilesetName) ?? "";

                if (!string.IsNullOrEmpty(name)) {
                    EntityEntry entry = new EntityEntry {
                        Name = name,
                        Width = entityDef.width,
                        Height = entityDef.height,
                        TilemapName = tilesetName,
                        TileX = entityDef.regionX,
                        TileY = entityDef.regionY,
                        TileWidth = entityDef.regionWidth,
                        TileHeight = entityDef.regionHeight,
                        PivotX = entityDef.pivotX,
                        PivotY = entityDef.pivotY,
                        PivotName = EntityEditor.FloatsToPivot(entityDef.pivotX, entityDef.pivotY)
                    };
                    _entities.Add(entry);
                    listBoxEntities.Items.Add(entry);
                }
            }
        }

        private void listBoxEntities_SelectedIndexChanged(object sender, EventArgs e) {
            bool hasSelection = listBoxEntities.SelectedIndex >= 0;
            buttonDelete.Enabled = hasSelection;

            if (!hasSelection) {
                entityEditor.ResetForNew();
                return;
            }

            EntityEntry entry = _entities[listBoxEntities.SelectedIndex];
            entityEditor.Populate(entry);
        }

        private void buttonNew_Click(object sender, EventArgs e) {
            listBoxEntities.SelectedIndex = -1;
            entityEditor.ResetForNew();
        }

        private void buttonDelete_Click(object sender, EventArgs e) {
            if (listBoxEntities.SelectedIndex < 0) {
                MessageBox.Show("Please select an entity to delete.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EntityEntry entity = _entities[listBoxEntities.SelectedIndex];
            DialogResult confirm = MessageBox.Show(
                $"Are you sure you want to delete entity '{entity.Name}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            int index = listBoxEntities.SelectedIndex;
            try {
                bool success = CExternsEditor.DeleteEntityDef(entity.Name);
                if (success) {
                    _entities.RemoveAt(index);
                    listBoxEntities.Items.RemoveAt(index);
                    _onEntityDefDeleted?.Invoke();
                } else {
                    string error = _externView.GetLastErrorMessage();
                    MessageBox.Show($"Failed to delete entity '{entity.Name}': {error}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Error deleting entity: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
