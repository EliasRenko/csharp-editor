using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ToolStripRenderer = csharp_editor.Styles.ToolStripRenderer;

namespace csharp_editor.UserControls {
    public partial class TextureViewer : UserControl {
        private CExternsEditor.TextureDataStruct _textureData;
        private int _tileSize;
        private int _tilesPerRow;
        private int _tilesPerCol;
        private Bitmap? _bitmap;
        public Point _selectedTile = new Point(-1, -1);
        private Rectangle _selectionRect = Rectangle.Empty;
        private float _zoomLevel = 1.0f;
        
        // Region selection mode
        private bool _regionSelectionMode = false;
        private Point _regionStart = new Point(-1, -1);
        private Point _regionEnd = new Point(-1, -1);
        private Rectangle _selectedRegion = Rectangle.Empty;
        private bool _isDragging = false;
        private bool _snapToGrid = true;
        private bool _showGrid = false;

        // animation for marching-ants selection rectangle
        private System.Windows.Forms.Timer _dashTimer = null!;
        private float _dashOffset = 0f;

        // background checker toggle
        private bool _checkerEnabled = false;

        // anti-alias toggle
        private bool _antiAliasEnabled = false;

        // region-only preview (set by SetRegionPreview, cleared by Clear/UpdateDisplay)
        private Rectangle _previewRegion = Rectangle.Empty;

        public bool HasSelection => _selectedTile.X >= 0 && _selectedTile.Y >= 0;
        
        public int SelectedRegionId {
            get {
                if (!HasSelection) return -1;
                return _selectedTile.Y * _tilesPerRow + _selectedTile.X;
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

        public bool ShowGrid {
            get => _showGrid;
            set { _showGrid = value; pictureBoxTexture.Invalidate(); }
        }

        /// <summary>Region selected by the user, in pixels. Valid in region-selection mode.</summary>
        public Rectangle SelectedRegionInPixels => _selectedRegion;

        /// <summary>Region in tile units (only meaningful when tileSize &gt; 0 and snap is on).</summary>
        public Rectangle SelectedRegionInTiles {
            get {
                var px = SelectedRegionInPixels;
                if (px == Rectangle.Empty || _tileSize <= 0) return Rectangle.Empty;
                return new Rectangle(px.X / _tileSize, px.Y / _tileSize,
                                     px.Width / _tileSize, px.Height / _tileSize);
            }
        }

        /// <summary>Gets or sets the snap-grid tile size. Setting it recomputes tilesPerRow/Col.</summary>
        public int TileSize {
            get => _tileSize;
            set {
                _tileSize = value;
                _tilesPerRow = value > 0 ? _textureData.Width  / value : 0;
                _tilesPerCol = value > 0 ? _textureData.Height / value : 0;
                pictureBoxTexture.Invalidate();
            }
        }
        
        public event EventHandler<int>? SelectionChanged;

        public TextureViewer() {
            InitializeComponent();

            toolStrip.Renderer = new ToolStripRenderer();
            
            pictureBoxTexture.MouseDown += PictureBoxTexture_MouseDown;
            pictureBoxTexture.MouseMove += PictureBoxTexture_MouseMove;
            pictureBoxTexture.MouseUp += PictureBoxTexture_MouseUp;
            pictureBoxTexture.Paint += PictureBoxTexture_Paint;
            pictureBoxTexture.MouseWheel += PictureBoxTexture_MouseWheel;
            
            InitializeZoomComboBox();

            // set up animation timer for dashed selection rectangle
            _dashTimer = new System.Windows.Forms.Timer();
            _dashTimer.Interval = 100; // milliseconds
            _dashTimer.Tick += (s, e) => {
                _dashOffset += 1f;
                if (_dashOffset > 6f) _dashOffset = 0f;
                pictureBoxTexture.Invalidate();
            };
            _dashTimer.Start();

            // checker toggle button action
            toolStripButtonChecker.CheckedChanged += (s, e) => {
                _checkerEnabled = toolStripButtonChecker.Checked;
                pictureBoxTexture.Invalidate();
            };

            // anti-alias toggle button action
            toolStripButtonAntiAlias.CheckedChanged += (s, e) => {
                _antiAliasEnabled = toolStripButtonAntiAlias.Checked;
                pictureBoxTexture.Invalidate();
            };

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

                if (_bitmap != null && _previewRegion != Rectangle.Empty) {
                    // Preview mode: just resize to the full bitmap at new zoom, then repaint
                    pictureBoxTexture.Width  = (int)(_bitmap.Width  * _zoomLevel);
                    pictureBoxTexture.Height = (int)(_bitmap.Height * _zoomLevel);
                    pictureBoxTexture.Invalidate();
                } else {
                    UpdateDisplay();
                }
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

        public void SetTextureData(CExternsEditor.TextureDataStruct textureData, int tileSize = 0) {
            _textureData = textureData;
            _tileSize = tileSize;
            _tilesPerRow = tileSize > 0 ? textureData.Width / tileSize : 0;
            _tilesPerCol = tileSize > 0 ? textureData.Height / tileSize : 0;
            UpdateDisplay();
        }
        
        public void SetSelectedTile(int regionId) {
            // Only set selection if not in region selection mode
            if (_regionSelectionMode) return;
            
            // Calculate tile position from region ID
            if (_tilesPerRow > 0 && regionId >= 0) {
                int tileX = regionId % _tilesPerRow;
                int tileY = regionId / _tilesPerRow;
                
                _selectedTile = new Point(tileX, tileY);
                
                // Calculate selection rectangle
                _selectionRect = new Rectangle(
                    tileX * _tileSize,
                    tileY * _tileSize,
                    _tileSize,
                    _tileSize
                );
                
                pictureBoxTexture.Invalidate();
            } else {
                // Clear selection for invalid region ID
                _selectedTile = new Point(-1, -1);
                _selectionRect = Rectangle.Empty;
                pictureBoxTexture.Invalidate();
            }

            CenterOnSelectedTile();
        }

        private void CenterOnSelectedTile() {
            if (pictureBoxTexture.Parent is ScrollableControl scrollPanel && _selectedTile.X >= 0 && _selectedTile.Y >= 0) {
                int tilePixelX = _selectedTile.X * _tileSize;
                int tilePixelY = _selectedTile.Y * _tileSize;

                int zoomedTileX = (int)(tilePixelX * _zoomLevel);
                int zoomedTileY = (int)(tilePixelY * _zoomLevel);

                int viewportWidth = scrollPanel.ClientSize.Width;
                int viewportHeight = scrollPanel.ClientSize.Height;

                int scrollX = Math.Max(0, zoomedTileX - viewportWidth / 2 + (int)(_tileSize * _zoomLevel) / 2);
                int scrollY = Math.Max(0, zoomedTileY - viewportHeight / 2 + (int)(_tileSize * _zoomLevel) / 2);

                scrollPanel.AutoScrollPosition = new Point(scrollX, scrollY);
            }
        }

        public void Clear() {
            _bitmap?.Dispose();
            _bitmap = null;
            _previewRegion = Rectangle.Empty;
            _selectedTile = new Point(-1, -1);
            _selectionRect = Rectangle.Empty;
            _regionStart = new Point(-1, -1);
            _regionEnd = new Point(-1, -1);
            _selectedRegion = Rectangle.Empty;
            _isDragging = false;
            pictureBoxTexture.Invalidate();
        }
        
        /// <summary>Pre-set the selection to a pixel rectangle. Snaps to grid if snap is enabled.</summary>
        public void SetInitialRegion(int pixelX, int pixelY, int pixelW, int pixelH) {
            if (!_regionSelectionMode || pixelW <= 0 || pixelH <= 0) return;
            int snapGrid = (_snapToGrid && _tileSize > 0) ? _tileSize : 1;
            _regionStart = new Point(
                (pixelX / snapGrid) * snapGrid,
                (pixelY / snapGrid) * snapGrid);
            int endX = ((pixelX + pixelW - 1) / snapGrid) * snapGrid;
            int endY = ((pixelY + pixelH - 1) / snapGrid) * snapGrid;
            _regionEnd = new Point(
                Math.Max(_regionStart.X, endX),
                Math.Max(_regionStart.Y, endY));
            UpdateRegionRectangle();
            pictureBoxTexture.Invalidate();
        }

        /// <summary>
        /// Loads the full texture and stores the entity region so the paint handler
        /// can dim everything outside it, leaving only the region at full brightness.
        /// Coordinates are in PIXELS (as stored by the C++ engine).
        /// </summary>
        public void SetRegionPreview(CExternsEditor.TextureDataStruct textureData, int tileSize, int pixelX, int pixelY, int pixelW, int pixelH) {
            _textureData = textureData;
            _tileSize = tileSize;
            _tilesPerRow = tileSize > 0 ? textureData.Width / tileSize : 0;
            _tilesPerCol = tileSize > 0 ? textureData.Height / tileSize : 0;
            _regionSelectionMode = false;
            _previewRegion = Rectangle.Empty;

            if (textureData.Data == IntPtr.Zero) return;

            pixelW = Math.Max(tileSize > 0 ? tileSize : 1, pixelW);
            pixelH = Math.Max(tileSize > 0 ? tileSize : 1, pixelH);

            try {
                _bitmap?.Dispose();
                _bitmap = CreateBitmapFromTextureData(textureData);

                int clampedX = Math.Max(0, Math.Min(pixelX, _bitmap.Width  - 1));
                int clampedY = Math.Max(0, Math.Min(pixelY, _bitmap.Height - 1));
                int clampedW = Math.Min(pixelW, _bitmap.Width  - clampedX);
                int clampedH = Math.Min(pixelH, _bitmap.Height - clampedY);

                if (clampedW > 0 && clampedH > 0)
                    _previewRegion = new Rectangle(clampedX, clampedY, clampedW, clampedH);

                pictureBoxTexture.Width  = (int)(_bitmap.Width  * _zoomLevel);
                pictureBoxTexture.Height = (int)(_bitmap.Height * _zoomLevel);
                pictureBoxTexture.Invalidate();
            }
            catch (Exception ex) {
                MessageBox.Show($"Region preview error: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDisplay() {
            _previewRegion = Rectangle.Empty;
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
            if (_bitmap == null) return;

            Point imagePoint = GetImageCoordinates(e.Location);
            if (imagePoint.X < 0 || imagePoint.Y < 0) return;

            if (_regionSelectionMode) {
                _isDragging = true;
                int snapGrid = (_snapToGrid && _tileSize > 0) ? _tileSize : 1;
                int snappedX = (imagePoint.X / snapGrid) * snapGrid;
                int snappedY = (imagePoint.Y / snapGrid) * snapGrid;
                _regionStart = new Point(snappedX, snappedY);
                _regionEnd   = new Point(snappedX, snappedY);
                UpdateRegionRectangle();
                pictureBoxTexture.Invalidate();
            } else {
                // Single tile selection — requires tileSize
                if (_tileSize <= 0) return;
                int tileX = imagePoint.X / _tileSize;
                int tileY = imagePoint.Y / _tileSize;
                if (tileX >= 0 && tileX < _tilesPerRow && tileY >= 0 && tileY < _tilesPerCol) {
                    _selectedTile  = new Point(tileX, tileY);
                    _selectionRect = new Rectangle(tileX * _tileSize, tileY * _tileSize, _tileSize, _tileSize);
                    pictureBoxTexture.Invalidate();
                    SelectionChanged?.Invoke(this, SelectedRegionId);
                }
            }
        }
        
        private void PictureBoxTexture_MouseMove(object? sender, MouseEventArgs e) {
            if (!_regionSelectionMode || !_isDragging || _bitmap == null) return;

            Point imagePoint = GetImageCoordinates(e.Location);
            if (imagePoint.X < 0 || imagePoint.Y < 0) return;

            int snapGrid = (_snapToGrid && _tileSize > 0) ? _tileSize : 1;
            int snappedX = (imagePoint.X / snapGrid) * snapGrid;
            int snappedY = (imagePoint.Y / snapGrid) * snapGrid;

            // Clamp so the last grid-aligned position stays inside the bitmap
            int maxX = ((_bitmap.Width  / snapGrid) - 1) * snapGrid;
            int maxY = ((_bitmap.Height / snapGrid) - 1) * snapGrid;
            snappedX = Math.Max(0, Math.Min(snappedX, maxX));
            snappedY = Math.Max(0, Math.Min(snappedY, maxY));

            if (_regionEnd.X != snappedX || _regionEnd.Y != snappedY) {
                _regionEnd = new Point(snappedX, snappedY);
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
            // _regionStart/_regionEnd are already pixel coords (snapped when applicable)
            int snapGrid = (_snapToGrid && _tileSize > 0) ? _tileSize : 1;
            int x1 = Math.Min(_regionStart.X, _regionEnd.X);
            int y1 = Math.Min(_regionStart.Y, _regionEnd.Y);
            int x2 = Math.Max(_regionStart.X, _regionEnd.X) + snapGrid;
            int y2 = Math.Max(_regionStart.Y, _regionEnd.Y) + snapGrid;
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
                if (_antiAliasEnabled) {
                    e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                } else {
                    e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                }

                int zoomedWidth  = (int)(_bitmap.Width  * _zoomLevel);
                int zoomedHeight = (int)(_bitmap.Height * _zoomLevel);
                if (_checkerEnabled) DrawCheckerBackground(e.Graphics, zoomedWidth, zoomedHeight);
                e.Graphics.DrawImage(_bitmap, 0, 0, zoomedWidth, zoomedHeight);

                // Dim everything outside the preview region
                if (_previewRegion != Rectangle.Empty) {
                    int rx = (int)(_previewRegion.X      * _zoomLevel);
                    int ry = (int)(_previewRegion.Y      * _zoomLevel);
                    int rw = (int)(_previewRegion.Width  * _zoomLevel);
                    int rh = (int)(_previewRegion.Height * _zoomLevel);
                    using (SolidBrush dim = new SolidBrush(Color.FromArgb(160, 0, 0, 0))) {
                        if (ry > 0)                          e.Graphics.FillRectangle(dim, 0,      0,       zoomedWidth,           ry);           // top
                        if (ry + rh < zoomedHeight)          e.Graphics.FillRectangle(dim, 0,      ry + rh, zoomedWidth,           zoomedHeight - (ry + rh)); // bottom
                        if (rx > 0)                          e.Graphics.FillRectangle(dim, 0,      ry,      rx,                    rh);           // left
                        if (rx + rw < zoomedWidth)           e.Graphics.FillRectangle(dim, rx + rw, ry,     zoomedWidth - (rx + rw), rh);         // right
                    }
                }
                // Draw grid overlay
                if (_showGrid && _tileSize > 0) {
                    int zw = (int)(_bitmap.Width  * _zoomLevel);
                    int zh = (int)(_bitmap.Height * _zoomLevel);
                    int step = (int)(_tileSize * _zoomLevel);
                    if (step >= 2) { // skip if grid lines would merge
                        using Pen gridPen = new Pen(Color.FromArgb(80, 255, 255, 255), 1f);
                        for (int gx = 0; gx <= zw; gx += step)
                            e.Graphics.DrawLine(gridPen, gx, 0, gx, zh);
                        for (int gy = 0; gy <= zh; gy += step)
                            e.Graphics.DrawLine(gridPen, 0, gy, zw, gy);
                    }
                }

            }
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
                        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        pen.DashOffset = _dashOffset;
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

        /// <summary>
        /// Fills the provided area with a simple checkerboard pattern.
        /// Size of each square scales with zoom level so the pattern stays visible.
        /// </summary>

        private void DrawCheckerBackground(Graphics g, int width, int height) {
            int baseSize = 8; // pixels
            int size = Math.Max(1, (int)(baseSize * _zoomLevel));
            Color c1 = Color.LightGray;
            Color c2 = Color.White;

            using (Brush b1 = new SolidBrush(c1))
            using (Brush b2 = new SolidBrush(c2)) {
                for (int y = 0; y < height; y += size) {
                    for (int x = 0; x < width; x += size) {
                        bool odd = ((x / size) + (y / size)) % 2 == 1;
                        g.FillRectangle(odd ? b1 : b2, x, y, size, size);
                    }
                }
            }
        }

        private Bitmap CreateBitmapFromTextureData(CExternsEditor.TextureDataStruct textureData) {
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
