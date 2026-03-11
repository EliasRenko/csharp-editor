using System.Runtime.InteropServices;
using csharp_editor.UserControls;

namespace csharp_editor.Dialogs {
    public partial class EntitiesDialog : Form {

        public class EntityEntry {
            public string Name { get; set; } = "";
            public int Width { get; set; } = 32;
            public int Height { get; set; } = 32;
            public string TilemapName { get; set; } = "";
            public int TileX { get; set; } = 0;
            public int TileY { get; set; } = 0;
            public int TileWidth { get; set; } = 1;
            public int TileHeight { get; set; } = 1;
            public float PivotX { get; set; } = 0.5f;
            public float PivotY { get; set; } = 1.0f;

            public override string ToString() {
                string tilemapPart = string.IsNullOrEmpty(TilemapName) ? "" : $" [{TilemapName}]";
                return $"{Name} ({Width}x{Height}px){tilemapPart}";
            }
        }

        public string SelectedEntityName { get; private set; } = "";

        private List<EntityEntry> _entities = new List<EntityEntry>();
        private ExternView _externView;
        private Action<string>? _onEntitySelected;
        private Action? _onEntityDefDeleted;

        // cache the last loaded texture data to avoid expensive reloads
        private string _lastTexturePath = string.Empty;
        private Externs.TextureDataStruct _lastTextureData;
        private bool _hasCachedTexture = false;

        public EntitiesDialog(ExternView externView, Action<string>? onEntitySelected = null, Action? onEntityDefDeleted = null) {
            InitializeComponent();
            _externView = externView;
            _onEntitySelected = onEntitySelected;
            _onEntityDefDeleted = onEntityDefDeleted;
            LoadExistingEntities();
        }

        private void LoadExistingEntities() {
            listBoxEntities.Items.Clear();
            _entities.Clear();
            textureViewer.Clear();

            int count = _externView.GetEntityCount();
            for (int i = 0; i < count; i++) {
                Externs.EntityDataStruct entityData = new Externs.EntityDataStruct();
                _externView.GetEntityAt(i, out entityData);

                string name = Marshal.PtrToStringAnsi(entityData.name) ?? "";
                string tilesetName = Marshal.PtrToStringAnsi(entityData.tilesetName) ?? "";

                if (!string.IsNullOrEmpty(name)) {
                    EntityEntry entry = new EntityEntry {
                        Name = name,
                        Width = entityData.width,
                        Height = entityData.height,
                        TilemapName = tilesetName,
                        TileX = entityData.regionX,
                        TileY = entityData.regionY,
                        TileWidth = entityData.regionWidth,
                        TileHeight = entityData.regionHeight,
                        PivotX = entityData.pivotX,
                        PivotY = entityData.pivotY
                    };
                    _entities.Add(entry);
                    listBoxEntities.Items.Add(entry);
                }
            }
        }

        private void listBoxEntities_SelectedIndexChanged(object sender, EventArgs e) {
            bool hasSelection = listBoxEntities.SelectedIndex >= 0;
            buttonDelete.Enabled = hasSelection;
            buttonEdit.Enabled = hasSelection;

            if (!hasSelection) {
                textureViewer.Clear();
                labelRegionInfo.Text = "";
                return;
            }

            EntityEntry entry = _entities[listBoxEntities.SelectedIndex];
            labelRegionInfo.Text = $"Region: tile ({entry.TileX}, {entry.TileY})  size {entry.TileWidth}×{entry.TileHeight} tiles   tileset: {entry.TilemapName}";
            ShowEntityRegion(entry);
        }

        private void ShowEntityRegion(EntityEntry entry) {
            if (string.IsNullOrEmpty(entry.TilemapName)) {
                textureViewer.Clear();
                return;
            }

            // Find the tileset by name using GetTilesetAt (same proven pattern as TilesetRegionDialog)
            int count = _externView.GetTilesetCount();
            int tilesetIndex = -1;
            for (int i = 0; i < count; i++) {
                Externs.TilesetInfoStruct info = new Externs.TilesetInfoStruct();
                if (_externView.GetTilesetAt(i, out info) != 0) {
                    string name = Marshal.PtrToStringAnsi(info.name) ?? "";
                    if (name == entry.TilemapName) {
                        tilesetIndex = i;
                        break;
                    }
                }
            }

            if (tilesetIndex < 0) {
                textureViewer.Clear();
                return;
            }

            Externs.TilesetInfoStruct tilesetInfo = new Externs.TilesetInfoStruct();
            if (_externView.GetTilesetAt(tilesetIndex, out tilesetInfo) == 0) {
                textureViewer.Clear();
                return;
            }

            string texturePath = Marshal.PtrToStringAnsi(tilesetInfo.texturePath) ?? "";
            if (string.IsNullOrEmpty(texturePath)) {
                textureViewer.Clear();
                return;
            }

            // if the newly requested texture is the same as cached, reuse it
            Externs.TextureDataStruct textureData;
            if (_hasCachedTexture && texturePath == _lastTexturePath) {
                textureData = _lastTextureData;
            } else {
                _externView.GetTextureData(texturePath, out textureData);
                // update cache
                _lastTexturePath = texturePath;
                _lastTextureData = textureData;
                _hasCachedTexture = true;
            }

            // Region values from C++ are pixel coords, pass them directly
            textureViewer.SetRegionPreview(textureData, 0,
                entry.TileX, entry.TileY, entry.TileWidth, entry.TileHeight);
        }

        private void buttonNew_Click(object sender, EventArgs e) {
            using (var createDialog = new EntityCreateDialog(_externView)) {
                if (createDialog.ShowDialog(this) == DialogResult.OK) {
                    LoadExistingEntities();
                }
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e) {
            if (listBoxEntities.SelectedIndex < 0) return;
            EntityEntry entry = _entities[listBoxEntities.SelectedIndex];
            using (var editDialog = new EntityCreateDialog(_externView)) {
                editDialog.Populate(entry);
                if (editDialog.ShowDialog(this) == DialogResult.OK) {
                    LoadExistingEntities();
                }
            }
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
                string? error = _externView.DeleteEntityDef(entity.Name);
                if (error == null) {
                    _entities.RemoveAt(index);
                    listBoxEntities.Items.RemoveAt(index);
                    _onEntityDefDeleted?.Invoke();
                } else {
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
