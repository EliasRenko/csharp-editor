using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace csharp_editor.UserControls {
    public partial class TextureViewer : UserControl {
        public Point SelectedTile => tilesetViewer.SelectedTile;
        public bool HasSelection => tilesetViewer.HasSelection;
        public int SelectedRegionId => tilesetViewer.SelectedRegionId;
        
        // Event for when tile selection changes
        public event EventHandler<int>? SelectionChanged {
            add { tilesetViewer.SelectionChanged += value; }
            remove { tilesetViewer.SelectionChanged -= value; }
        }

        public TextureViewer() {
            InitializeComponent();
        }

        public void SetTextureData(Externs.TextureDataStruct textureData, Externs.TilesetInfoStruct tilesetInfo) {
            // Update info labels
            labelWidth.Text = $"Width: {textureData.Width}";
            labelHeight.Text = $"Height: {textureData.Height}";
            labelBPP.Text = $"Bytes Per Pixel: {textureData.BytesPerPixel}";
            labelDataLength.Text = $"Data Length: {textureData.DataLength}";
            labelTransparent.Text = $"Transparent: {(textureData.Transparent != 0 ? "Yes" : "No")}";
            
            // Update tileset viewer
            tilesetViewer.SetTextureData(textureData, tilesetInfo);
        }

        public void Clear() {
            tilesetViewer.Clear();
            
            labelWidth.Text = "Width: -";
            labelHeight.Text = "Height: -";
            labelBPP.Text = "Bytes Per Pixel: -";
            labelDataLength.Text = "Data Length: -";
            labelTransparent.Text = "Transparent: -";
        }
    }
}
