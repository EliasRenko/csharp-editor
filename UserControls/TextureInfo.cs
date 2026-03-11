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

        public void SetTextureData(Externs.TextureDataStruct textureData, int tileSize = 0) {
            // Update tileset viewer and clear any prior region selection
            textureViewer.RegionSelectionMode = false;
            textureViewer.Clear(); // clear previous image first just in case
            textureViewer.SetTextureData(textureData, tileSize);
        }

        public void SetTextureRegion(Externs.TextureDataStruct textureData, int tileSize, Rectangle region) {
            // Load full texture but turn on region selection mode and define initial region
            textureViewer.RegionSelectionMode = true;
            textureViewer.SetTextureData(textureData, tileSize);
            if (tileSize > 0) {
                int tileX = region.X / tileSize;
                int tileY = region.Y / tileSize;
                int tileW = region.Width / tileSize;
                int tileH = region.Height / tileSize;
                textureViewer.SetInitialRegion(tileX, tileY, tileW, tileH);
            }
        }


        public void Clear() {
            textureViewer.Clear();
        }
    }
}
