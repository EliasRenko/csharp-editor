namespace csharp_editor.UserControls {
    public partial class TextureInfo : UserControl {

        public Point SelectedTile => textureViewer._selectedTile;
        public bool HasSelection => textureViewer.HasSelection;
        public int SelectedRegionId => textureViewer.SelectedRegionId;

        // Event for when tile selection changes
        public event EventHandler<int>? SelectionChanged {
            add { textureViewer.SelectionChanged += value; }
            remove { textureViewer.SelectionChanged -= value; }
        }

        public TextureInfo() {
            InitializeComponent();
        }

        public void SetTextureData(Externs.TextureDataStruct textureData, Externs.TilesetInfoStruct tilesetInfo) {
            // Update tileset viewer and clear any prior region selection
            textureViewer.RegionSelectionMode = false;
            textureViewer.Clear(); // clear previous image first just in case
            textureViewer.SetTextureData(textureData, tilesetInfo);
        }

        public void SetTextureRegion(Externs.TextureDataStruct textureData, Externs.TilesetInfoStruct tilesetInfo, Rectangle region) {
            // Load full texture but turn on region selection mode and define initial region
            textureViewer.RegionSelectionMode = true;
            textureViewer.SetTextureData(textureData, tilesetInfo);
            if (tilesetInfo.tileSize > 0) {
                int tileX = region.X / tilesetInfo.tileSize;
                int tileY = region.Y / tilesetInfo.tileSize;
                int tileW = region.Width / tilesetInfo.tileSize;
                int tileH = region.Height / tilesetInfo.tileSize;
                textureViewer.SetInitialRegion(tileX, tileY, tileW, tileH);
            }
        }


        public void Clear() {
            textureViewer.Clear();
        }
    }
}
