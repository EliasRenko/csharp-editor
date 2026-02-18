using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace csharp_editor.UserControls {
    public partial class TilesetViewer : UserControl {
        private Externs.TextureDataStruct _textureData;
        private Externs.TilesetInfoStruct _tilesetInfo;
        private Bitmap? _bitmap;
        private Point _selectedTile = new Point(-1, -1);
        private Rectangle _selectionRect = Rectangle.Empty;
        private float _zoomLevel = 1.0f;
        private Point _scrollOffset = Point.Empty;
        
        // Region selection mode
        private bool _regionSelectionMode = false;
        private Point _regionStart = new Point(-1, -1);
        private Point _regionEnd = new Point(-1, -1);
        private Rectangle _selectedRegion = Rectangle.Empty;
        private bool _isDragging = false;
        private bool _snapToGrid = true;

        public Point SelectedTile => _selectedTile;
        public bool HasSelection => _selectedTile.X >= 0 && _selectedTile.Y >= 0;
        
        public int SelectedRegionId {
            get {
                if (!HasSelection) return -1;
                return _selectedTile.Y * _tilesetInfo.tilesPerRow + _selectedTile.X;
            }
        }
        
        // Region selection properties
        public bool RegionSelectionMode {
            get => _regionSelectionMode;
            set {
                _regionSelectionMode = value;
                if (!value) {
                    // Clear region selection when exiting mode
                    _regionStart = new Point(-1, -1);
                    _regionEnd = new Point(-1, -1);
                    _selectedRegion = Rectangle.Empty;
                    _isDragging = false;
                }
                pictureBoxTexture.Invalidate();
            }
        }
        
        public bool SnapToGrid {
            get => _snapToGrid;
            set => _snapToGrid = value;
        }
        
        public Rectangle SelectedRegionInTiles {
            get {
                if (_regionStart.X < 0 || _regionStart.Y < 0 || _regionEnd.X < 0 || _regionEnd.Y < 0) {
                    return Rectangle.Empty;
                }
                
                int x = Math.Min(_regionStart.X, _regionEnd.X);
                int y = Math.Min(_regionStart.Y, _regionEnd.Y);
                int width = Math.Abs(_regionEnd.X - _regionStart.X) + 1;
                int height = Math.Abs(_regionEnd.Y - _regionStart.Y) + 1;
                
                return new Rectangle(x, y, width, height);
            }
        }
        
        // Event for when tile selection changes
        public event EventHandler<int>? SelectionChanged;

        public TilesetViewer() {
            InitializeComponent();
            pictureBoxTexture.MouseDown += PictureBoxTexture_MouseDown;
            pictureBoxTexture.MouseMove += PictureBoxTexture_MouseMove;
            pictureBoxTexture.MouseUp += PictureBoxTexture_MouseUp;
            pictureBoxTexture.Paint += PictureBoxTexture_Paint;
            pictureBoxTexture.MouseWheel += PictureBoxTexture_MouseWheel;
            
            InitializeZoomComboBox();
        }
        
        private void InitializeZoomComboBox() {
            toolStripComboBoxZoom.Items.AddRange(new object[] {
                "25%", "50%", "75%", "100%", "150%", "200%", "300%", "400%"
            });
            toolStripComboBoxZoom.SelectedIndex = 3; // 100%
            toolStripComboBoxZoom.SelectedIndexChanged += ToolStripComboBoxZoom_SelectedIndexChanged;
        }
        
        private void ToolStripComboBoxZoom_SelectedIndexChanged(object? sender, EventArgs e) {
            if (toolStripComboBoxZoom.SelectedItem == null) return;
            
            string zoomText = toolStripComboBoxZoom.SelectedItem.ToString() ?? "100%";
            if (int.TryParse(zoomText.TrimEnd('%'), out int zoomPercent)) {
                _zoomLevel = zoomPercent / 100f;
                UpdateDisplay();
            }
        }
        
        private void PictureBoxTexture_MouseWheel(object? sender, MouseEventArgs e) {
            if (e.Delta > 0) {
                // Zoom in
                int currentIndex = toolStripComboBoxZoom.SelectedIndex;
                if (currentIndex < toolStripComboBoxZoom.Items.Count - 1) {
                    toolStripComboBoxZoom.SelectedIndex = currentIndex + 1;
                }
            } else {
                // Zoom out
                int currentIndex = toolStripComboBoxZoom.SelectedIndex;
                if (currentIndex > 0) {
                    toolStripComboBoxZoom.SelectedIndex = currentIndex - 1;
                }
            }
        }

        public void SetTextureData(Externs.TextureDataStruct textureData, Externs.TilesetInfoStruct tilesetInfo) {
            _textureData = textureData;
            _tilesetInfo = tilesetInfo;
            UpdateDisplay();
        }

        public void Clear() {
            _bitmap?.Dispose();
            _bitmap = null;
            _selectedTile = new Point(-1, -1);
            _selectionRect = Rectangle.Empty;
            _regionStart = new Point(-1, -1);
            _regionEnd = new Point(-1, -1);
            _selectedRegion = Rectangle.Empty;
            _isDragging = false;
            pictureBoxTexture.Invalidate();
        }
        
        public void SetInitialRegion(int tileX, int tileY, int tileWidth, int tileHeight) {
            if (_regionSelectionMode && _tilesetInfo.tileSize > 0) {
                _regionStart = new Point(tileX, tileY);
                _regionEnd = new Point(tileX + tileWidth - 1, tileY + tileHeight - 1);
                UpdateRegionRectangle();
                pictureBoxTexture.Invalidate();
            }
        }

        private void UpdateDisplay() {
            // Create bitmap from texture data
            if (_textureData.Data != IntPtr.Zero && _textureData.Width > 0 && _textureData.Height > 0) {
                try {
                    _bitmap?.Dispose();
                    _bitmap = CreateBitmapFromTextureData(_textureData);
                    
                    // Set PictureBox size to zoomed dimensions (this determines the scrollable area)
                    int zoomedWidth = (int)(_bitmap.Width * _zoomLevel);
                    int zoomedHeight = (int)(_bitmap.Height * _zoomLevel);
                    pictureBoxTexture.Width = zoomedWidth;
                    pictureBoxTexture.Height = zoomedHeight;
                    
                    // Don't set the Image property - we'll draw manually in Paint event
                    pictureBoxTexture.Invalidate();
                }
                catch (Exception ex) {
                    MessageBox.Show($"Error creating bitmap: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PictureBoxTexture_MouseDown(object? sender, MouseEventArgs e) {
            if (_bitmap == null || _tilesetInfo.tileSize <= 0) return;

            // Convert mouse coordinates to image coordinates
            Point imagePoint = GetImageCoordinates(e.Location);
            if (imagePoint.X < 0 || imagePoint.Y < 0) return;

            // Calculate tile position
            int tileX = imagePoint.X / _tilesetInfo.tileSize;
            int tileY = imagePoint.Y / _tilesetInfo.tileSize;

            // Validate tile position
            if (tileX >= 0 && tileX < _tilesetInfo.tilesPerRow && 
                tileY >= 0 && tileY < _tilesetInfo.tilesPerCol) {
                
                if (_regionSelectionMode) {
                    // Start region selection
                    _isDragging = true;
                    _regionStart = new Point(tileX, tileY);
                    _regionEnd = new Point(tileX, tileY);
                    UpdateRegionRectangle();
                    pictureBoxTexture.Invalidate();
                } else {
                    // Single tile selection (existing behavior)
                    _selectedTile = new Point(tileX, tileY);
                    
                    // Calculate selection rectangle in image coordinates
                    _selectionRect = new Rectangle(
                        tileX * _tilesetInfo.tileSize,
                        tileY * _tilesetInfo.tileSize,
                        _tilesetInfo.tileSize,
                        _tilesetInfo.tileSize
                    );
                    
                    pictureBoxTexture.Invalidate();
                    
                    // Raise selection changed event
                    SelectionChanged?.Invoke(this, SelectedRegionId);
                }
            }
        }
        
        private void PictureBoxTexture_MouseMove(object? sender, MouseEventArgs e) {
            if (!_regionSelectionMode || !_isDragging || _bitmap == null || _tilesetInfo.tileSize <= 0) return;

            // Convert mouse coordinates to image coordinates
            Point imagePoint = GetImageCoordinates(e.Location);
            if (imagePoint.X < 0 || imagePoint.Y < 0) return;

            // Calculate tile position
            int tileX = imagePoint.X / _tilesetInfo.tileSize;
            int tileY = imagePoint.Y / _tilesetInfo.tileSize;

            // Clamp to valid range
            tileX = Math.Max(0, Math.Min(tileX, _tilesetInfo.tilesPerRow - 1));
            tileY = Math.Max(0, Math.Min(tileY, _tilesetInfo.tilesPerCol - 1));

            if (_regionEnd.X != tileX || _regionEnd.Y != tileY) {
                _regionEnd = new Point(tileX, tileY);
                UpdateRegionRectangle();
                pictureBoxTexture.Invalidate();
            }
        }
        
        private void PictureBoxTexture_MouseUp(object? sender, MouseEventArgs e) {
            if (_regionSelectionMode && _isDragging) {
                _isDragging = false;
                // Region is set, keep it selected
            }
        }
        
        private void UpdateRegionRectangle() {
            if (_regionStart.X < 0 || _regionStart.Y < 0 || _regionEnd.X < 0 || _regionEnd.Y < 0) {
                _selectedRegion = Rectangle.Empty;
                return;
            }
            
            int x1 = Math.Min(_regionStart.X, _regionEnd.X) * _tilesetInfo.tileSize;
            int y1 = Math.Min(_regionStart.Y, _regionEnd.Y) * _tilesetInfo.tileSize;
            int x2 = (Math.Max(_regionStart.X, _regionEnd.X) + 1) * _tilesetInfo.tileSize;
            int y2 = (Math.Max(_regionStart.Y, _regionEnd.Y) + 1) * _tilesetInfo.tileSize;
            
            _selectedRegion = new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        private Point GetImageCoordinates(Point pictureBoxPoint) {
            if (_bitmap == null) return new Point(-1, -1);

            // Since we're using AutoScroll and Normal size mode, just scale by zoom
            int imageX = (int)(pictureBoxPoint.X / _zoomLevel);
            int imageY = (int)(pictureBoxPoint.Y / _zoomLevel);

            // Validate bounds
            if (imageX < 0 || imageX >= _bitmap.Width || imageY < 0 || imageY >= _bitmap.Height) {
                return new Point(-1, -1);
            }

            return new Point(imageX, imageY);
        }

        private void PictureBoxTexture_Paint(object? sender, PaintEventArgs e) {
            // Draw the zoomed bitmap
            if (_bitmap != null) {
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                
                int zoomedWidth = (int)(_bitmap.Width * _zoomLevel);
                int zoomedHeight = (int)(_bitmap.Height * _zoomLevel);
                
                e.Graphics.DrawImage(_bitmap, 0, 0, zoomedWidth, zoomedHeight);
            }
            
            // Draw region selection (in region mode)
            if (_regionSelectionMode && _selectedRegion != Rectangle.Empty && _bitmap != null) {
                Rectangle displayRect = GetDisplayRectangle(_selectedRegion);
                if (displayRect != Rectangle.Empty) {
                    // Draw semi-transparent overlay
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(64, 0, 120, 215))) {
                        e.Graphics.FillRectangle(brush, displayRect);
                    }
                    // Draw border
                    using (Pen pen = new Pen(Color.FromArgb(255, 0, 120, 215), 2)) {
                        e.Graphics.DrawRectangle(pen, displayRect);
                    }
                }
            }
            // Draw single tile selection (in normal mode)
            else if (!_regionSelectionMode && _selectionRect != Rectangle.Empty && _bitmap != null) {
                Rectangle displayRect = GetDisplayRectangle(_selectionRect);
                if (displayRect != Rectangle.Empty) {
                    using (Pen pen = new Pen(Color.Yellow, 2)) {
                        e.Graphics.DrawRectangle(pen, displayRect);
                    }
                }
            }
        }

        private Rectangle GetDisplayRectangle(Rectangle imageRect) {
            if (_bitmap == null) return Rectangle.Empty;

            return new Rectangle(
                (int)(imageRect.X * _zoomLevel),
                (int)(imageRect.Y * _zoomLevel),
                (int)(imageRect.Width * _zoomLevel),
                (int)(imageRect.Height * _zoomLevel)
            );
        }

        private Bitmap CreateBitmapFromTextureData(Externs.TextureDataStruct textureData) {
            // Create bitmap based on bytes per pixel
            PixelFormat format = textureData.BytesPerPixel switch {
                1 => PixelFormat.Format8bppIndexed,
                3 => PixelFormat.Format24bppRgb,
                4 => PixelFormat.Format32bppArgb,
                _ => PixelFormat.Format32bppArgb
            };

            Bitmap bitmap = new Bitmap(textureData.Width, textureData.Height, format);

            // Lock the bitmap's bits
            BitmapData bmpData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.WriteOnly,
                bitmap.PixelFormat);

            // Calculate stride
            int stride = Math.Abs(bmpData.Stride);
            int imageSize = stride * bitmap.Height;

            // Copy the data
            if (textureData.BytesPerPixel == 3) {
                // BGR to RGB conversion for 24-bit
                byte[] pixelData = new byte[textureData.DataLength];
                Marshal.Copy(textureData.Data, pixelData, 0, textureData.DataLength);

                byte[] convertedData = new byte[imageSize];
                for (int y = 0; y < bitmap.Height; y++) {
                    for (int x = 0; x < bitmap.Width; x++) {
                        int srcIdx = (y * bitmap.Width + x) * 3;
                        int dstIdx = y * stride + x * 3;

                        if (srcIdx + 2 < pixelData.Length && dstIdx + 2 < convertedData.Length) {
                            // Swap BGR to RGB
                            convertedData[dstIdx] = pixelData[srcIdx + 2];     // R
                            convertedData[dstIdx + 1] = pixelData[srcIdx + 1]; // G
                            convertedData[dstIdx + 2] = pixelData[srcIdx];     // B
                        }
                    }
                }

                Marshal.Copy(convertedData, 0, bmpData.Scan0, Math.Min(convertedData.Length, imageSize));
            }
            else if (textureData.BytesPerPixel == 4) {
                // BGRA to RGBA conversion for 32-bit
                byte[] pixelData = new byte[textureData.DataLength];
                Marshal.Copy(textureData.Data, pixelData, 0, textureData.DataLength);

                byte[] convertedData = new byte[imageSize];
                for (int y = 0; y < bitmap.Height; y++) {
                    for (int x = 0; x < bitmap.Width; x++) {
                        int srcIdx = (y * bitmap.Width + x) * 4;
                        int dstIdx = y * stride + x * 4;

                        if (srcIdx + 3 < pixelData.Length && dstIdx + 3 < convertedData.Length) {
                            // Swap BGRA to RGBA
                            convertedData[dstIdx] = pixelData[srcIdx + 2];     // R
                            convertedData[dstIdx + 1] = pixelData[srcIdx + 1]; // G
                            convertedData[dstIdx + 2] = pixelData[srcIdx];     // B
                            convertedData[dstIdx + 3] = pixelData[srcIdx + 3]; // A
                        }
                    }
                }

                Marshal.Copy(convertedData, 0, bmpData.Scan0, Math.Min(convertedData.Length, imageSize));
            }
            else {
                // Direct copy for other formats
                int bytesToCopy = Math.Min(textureData.DataLength, imageSize);
                byte[] tempBuffer = new byte[bytesToCopy];
                Marshal.Copy(textureData.Data, tempBuffer, 0, bytesToCopy);
                Marshal.Copy(tempBuffer, 0, bmpData.Scan0, bytesToCopy);
            }

            bitmap.UnlockBits(bmpData);

            return bitmap;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _bitmap?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
