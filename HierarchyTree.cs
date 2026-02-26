using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace csharp_editor {
    public partial class HierarchyTree : UserControl {
        
        public class LayerNode {
            public string Name { get; set; } = "";
            public LayerType Type { get; set; } = LayerType.TileLayer;
            public bool Visible { get; set; } = true;
            public bool Locked { get; set; } = false;
            public string TilesetName { get; set; } = ""; // For TileLayer only
            public TreeNode TreeNodeRef { get; set; } = null!; // assigned when node created
            
            public override string ToString() {
                return Name;
            }
        }

        /// <summary>
        /// Holds information about a batch group inside an entity layer.
        /// Used as the Tag for batch child nodes.
        /// </summary>
        private class BatchInfo {
            public string TilesetName { get; set; } = "";
            public int EntityCount { get; set; }

            public override string ToString() {
                string text = TilesetName;
                if (EntityCount >= 0)
                    text += $" ({EntityCount})";
                return text;
            }
        }

        private List<LayerNode> _layers = new List<LayerNode>();
        private ExternView? _externView; // may be null until SetExternView called
        private TreeNode? _stateNode; // root node holding all layers

        /// <summary>
        /// Ensure the special immutable "State" parent exists and return it.
        /// </summary>
        private TreeNode GetStateNode() {
            if (_stateNode != null && treeViewLayers.Nodes.Contains(_stateNode)) {
                _stateNode.Expand();
                return _stateNode;
            }
            // create node only once, always at index 0
            _stateNode = new TreeNode("State");
            _stateNode.Tag = new object(); // sentinel
            _stateNode.NodeFont = new Font(treeViewLayers.Font, FontStyle.Bold);
            _stateNode.ForeColor = Color.DarkBlue;
            treeViewLayers.Nodes.Insert(0, _stateNode);
            _stateNode.Expand();
            return _stateNode;
        }
        private const int IconSize = 16;
        private const int IconSpacing = 4;
        private static Image? _stateIcon = null; // loaded lazily from Icons/map.png
        
        public event EventHandler<LayerNode>? LayerSelected;
        public event EventHandler? StateSelected;
        /// <summary>Raised when a batch group node is selected; argument is the tileset name.</summary>
        public event EventHandler<string>? BatchSelected;
        public event EventHandler? LayersChanged;
        public event EventHandler? ReplaceTilesetClicked;

        public HierarchyTree() {
            InitializeComponent();
            InitializeTreeView();
        }

        public void SetExternView(ExternView externView) {
            _externView = externView;
        }

        private void InitializeTreeView() {
            treeViewLayers.HideSelection = false;
            treeViewLayers.FullRowSelect = true;
            treeviewLayersBeforeExpandHook();
            treeViewLayers.DrawMode = TreeViewDrawMode.OwnerDrawText;
            treeViewLayers.DrawNode += TreeViewLayers_DrawNode;
            treeViewLayers.MouseDown += TreeViewLayers_MouseDown;
            treeViewLayers.KeyDown += TreeViewLayers_KeyDown;
            // prevent state node collapse
            treeViewLayers.BeforeCollapse += TreeViewLayers_BeforeCollapse;
            // enable drag/drop of nodes
            treeViewLayers.AllowDrop = true;
            treeViewLayers.ItemDrag += TreeViewLayers_ItemDrag;
            treeViewLayers.DragEnter += TreeViewLayers_DragEnter;
            treeViewLayers.DragOver += TreeViewLayers_DragOver;
            treeViewLayers.DragDrop += TreeViewLayers_DragDrop;
            UpdateButtonStates();
        }

        private void treeviewLayersBeforeExpandHook() {
            treeViewLayers.BeforeExpand += TreeViewLayers_BeforeExpand;
        }

        private void TreeViewLayers_BeforeExpand(object? sender, TreeViewCancelEventArgs e) {
            if (e.Node?.Tag is LayerNode layer && layer.Type == LayerType.EntityLayer) {
                UpdateEntityLayerBatches(layer);
            }
        }

        private void UpdateEntityLayerBatches(LayerNode layer) {
            if (_externView == null) return;
            TreeNode node = layer.TreeNodeRef;
            node.Nodes.Clear();
            int batchCount = _externView.GetEntityLayerBatchCount(layer.Name);
            for (int i = 0; i < batchCount; i++) {
                string? tilesetName = _externView.GetEntityLayerBatchTilesetName(layer.Name, i);
                int entityCount = _externView.GetEntityLayerBatchCountAt(i);
                string display = tilesetName ?? "<unknown>";
                // create node with BatchInfo tag so we can style it later
                TreeNode batchNode = new TreeNode(display + $" ({entityCount})");
                batchNode.Tag = new BatchInfo {
                    TilesetName = tilesetName ?? "",
                    EntityCount = entityCount
                };
                // make batches visually distinct
                batchNode.ForeColor = Color.Gray;
                batchNode.NodeFont = new Font(treeViewLayers.Font, FontStyle.Italic);
                node.Nodes.Add(batchNode);
            }
        }

        public void AddLayer(string name, LayerType type, string tilesetName = "") {
            LayerNode layer = new LayerNode {
                Name = name,
                Type = type,
                Visible = true,
                TilesetName = tilesetName
            };

            TreeNode treeNode = new TreeNode(layer.ToString());
            treeNode.Tag = layer;
            layer.TreeNodeRef = treeNode;

            // Insert into the State parent at selected layer position, or at end if nothing is selected
            TreeNode parent = GetStateNode();
            int insertIndex;
            if (treeViewLayers.SelectedNode != null &&
                treeViewLayers.SelectedNode.Tag is LayerNode selLayer &&
                treeViewLayers.SelectedNode.Parent == parent) {
                insertIndex = treeViewLayers.SelectedNode.Index;
            } else {
                insertIndex = parent.Nodes.Count;
            }
            parent.Nodes.Insert(insertIndex, treeNode);
            _layers.Insert(insertIndex, layer);

            // Notify backend - pass index for insertion
            if (type == LayerType.TileLayer) {
                _externView?.CreateTilemapLayer(name, tilesetName, insertIndex);
            } else if (type == LayerType.EntityLayer) {
                // API no longer requires a tileset for entity layers
                _externView?.CreateEntityLayer(name);
            }

            LayersChanged?.Invoke(this, EventArgs.Empty);
            UpdateButtonStates();
        }

        public void RemoveSelectedLayer() {
            if (treeViewLayers.SelectedNode != null) {
                TreeNode selectedNode = treeViewLayers.SelectedNode;
                LayerNode? layer = selectedNode.Tag as LayerNode;

                if (layer != null) {
                    _layers.Remove(layer);
                    
                    // Notify backend
                    _externView?.RemoveLayer(layer.Name);
                }

                treeViewLayers.Nodes.Remove(selectedNode);
                LayersChanged?.Invoke(this, EventArgs.Empty);
                UpdateButtonStates();
            }
        }

        public void MoveLayerUp() {
            if (treeViewLayers.SelectedNode == null) return;
            
            TreeNode node = treeViewLayers.SelectedNode;
            // if a batch is selected, move it within its parent
            if (node.Tag is BatchInfo) {
                MoveBatchUp();
                return;
            }

            int index = node.Index;
            
            if (index > 0) {
                TreeNode parent = node.Parent;
                TreeNodeCollection nodes = parent?.Nodes ?? treeViewLayers.Nodes;
                
                nodes.RemoveAt(index);
                nodes.Insert(index - 1, node);
                treeViewLayers.SelectedNode = node;

                // Update layer list order
                LayerNode? layer = node.Tag as LayerNode;
                if (layer != null && _layers.Contains(layer)) {
                    int layerIndex = _layers.IndexOf(layer);
                    if (layerIndex > 0) {
                        _layers.RemoveAt(layerIndex);
                        _layers.Insert(layerIndex - 1, layer);
                    }
                    
                    // Notify backend
                    _externView?.MoveLayerUpByIndex(index);
                }

                LayersChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void MoveBatchUp() {
            // assumes SelectedNode is a batch info node
            TreeNode? node = treeViewLayers.SelectedNode;
            if (node == null || !(node.Tag is BatchInfo)) return;
            TreeNode? parent = node.Parent;
            if (parent == null) return;
            int currentIndex = node.Index;
            if (currentIndex <= 0) return;
            // swap visually
            parent.Nodes.RemoveAt(currentIndex);
            parent.Nodes.Insert(currentIndex - 1, node);
            treeViewLayers.SelectedNode = node;

            // notify backend: use layer name from parent
            if (parent.Tag is LayerNode layer) {
                _externView?.MoveEntityLayerBatchUp(layer.Name, currentIndex);
            }
        }

        private void MoveBatchDown() {
            TreeNode? node = treeViewLayers.SelectedNode;
            if (node == null || !(node.Tag is BatchInfo)) return;
            TreeNode? parent = node.Parent;
            if (parent == null) return;
            int currentIndex = node.Index;
            if (currentIndex < parent.Nodes.Count - 1) {
                parent.Nodes.RemoveAt(currentIndex);
                parent.Nodes.Insert(currentIndex + 1, node);
                treeViewLayers.SelectedNode = node;

                if (parent.Tag is LayerNode layer) {
                    _externView?.MoveEntityLayerBatchDown(layer.Name, currentIndex);
                }
            }
        }

        public void MoveLayerDown() {
            if (treeViewLayers.SelectedNode == null) return;
            
            TreeNode node = treeViewLayers.SelectedNode;
            if (node.Tag is BatchInfo) {
                MoveBatchDown();
                return;
            }
            TreeNode parent = node.Parent;
            TreeNodeCollection nodes = parent?.Nodes ?? treeViewLayers.Nodes;
            int index = node.Index;
            
            if (index < nodes.Count - 1) {
                nodes.RemoveAt(index);
                nodes.Insert(index + 1, node);
                treeViewLayers.SelectedNode = node;

                // Update layer list order
                LayerNode? layer = node.Tag as LayerNode;
                if (layer != null && _layers.Contains(layer)) {
                    int layerIndex = _layers.IndexOf(layer);
                    if (layerIndex < _layers.Count - 1) {
                        _layers.RemoveAt(layerIndex);
                        _layers.Insert(layerIndex + 1, layer);
                    }
                    
                    // Notify backend
                    _externView?.MoveLayerDownByIndex(index);
                }

                LayersChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void ToggleLayerVisibility() {
            if (treeViewLayers.SelectedNode != null) {
                TreeNode node = treeViewLayers.SelectedNode;
                LayerNode? layer = node.Tag as LayerNode;

                if (layer != null) {
                    layer.Visible = !layer.Visible;
                    node.Text = layer.ToString();
                    treeViewLayers.Invalidate(); // Refresh to update icons

                    LayersChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void ToggleLayerLock() {
            if (treeViewLayers.SelectedNode != null) {
                TreeNode node = treeViewLayers.SelectedNode;
                LayerNode? layer = node.Tag as LayerNode;

                if (layer != null) {
                    layer.Locked = !layer.Locked;
                    treeViewLayers.Invalidate(); // Refresh to update icons

                    LayersChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public LayerNode? GetSelectedLayer() {
            if (treeViewLayers.SelectedNode != null) {
                return treeViewLayers.SelectedNode.Tag as LayerNode;
            }
            return null;
        }

        /// <summary>
        /// Refreshes the batch-group children for the current selection if it is an entity layer.
        /// This can be invoked externally when the backend state changes (e.g. after placement).
        /// </summary>
        public void RefreshSelectedEntityBatches() {
            var layer = GetSelectedLayer();
            if (layer != null && layer.Type == LayerType.EntityLayer) {
                UpdateEntityLayerBatches(layer);
            }
        }

        /// <summary>
        /// If the currently selected node represents a batch group, returns its tileset name.
        /// Otherwise returns null.
        /// </summary>
        public string? GetSelectedBatchTilesetName() {
            if (treeViewLayers.SelectedNode?.Tag is BatchInfo batch) {
                return batch.TilesetName;
            }
            return null;
        }

        /// <summary>
        /// Number of entities in the selected batch group (or -1 if none).
        /// </summary>
        public int GetSelectedBatchCount() {
            if (treeViewLayers.SelectedNode?.Tag is BatchInfo batch) {
                return batch.EntityCount;
            }
            return -1;
        }

        public void RenameLayer(string oldName, string newName) {
            var layer = _layers.Find(l => l.Name == oldName);
            if (layer != null) {
                layer.Name = newName;
                if (layer.TreeNodeRef != null)
                    layer.TreeNodeRef.Text = newName;
            }
        }

        public List<LayerNode> GetAllLayers() {
            return new List<LayerNode>(_layers);
        }

        public void ClearLayers() {
            // preserve state node
            GetStateNode();
            _stateNode!.Nodes.Clear();
            _layers.Clear();

            LayersChanged?.Invoke(this, EventArgs.Empty);
            UpdateButtonStates();
        }
        
        public void LoadLayersFromBackend() {
            // Clear existing layers (preserve state node)
            GetStateNode();
            _stateNode!.Nodes.Clear();
            _layers.Clear();
            
            if (_externView == null) return;
            
            // Get layer count from backend
            int count = _externView.GetLayerCount();
            
            for (int i = 0; i < count; i++) {
                Externs.LayerInfoStruct layerInfo = new Externs.LayerInfoStruct();
                int result = _externView.GetLayerInfoAt(i, out layerInfo);
                
                if (result != 0) {
                    string layerName = Marshal.PtrToStringAnsi(layerInfo.name) ?? "";
                    string tilesetName = Marshal.PtrToStringAnsi(layerInfo.tilesetName) ?? "";
                    
                    if (!string.IsNullOrEmpty(layerName)) {
                        LayerNode layer = new LayerNode {
                            Name = layerName,
                            Type = (LayerType)layerInfo.type,
                            Visible = layerInfo.visible != 0,
                            TilesetName = tilesetName,
                        };

                        TreeNode treeNode = new TreeNode(layer.ToString());
                        treeNode.Tag = layer;
                        layer.TreeNodeRef = treeNode;

                        TreeNode container = GetStateNode();
                        container.Nodes.Add(treeNode);
                        _layers.Add(layer);
                        // if it's an entity layer, populate and expand it so batches are visible
                        if (layer.Type == LayerType.EntityLayer) {
                            UpdateEntityLayerBatches(layer);
                            treeNode.Expand();
                        }
                    }
                }
            }
            
            LayersChanged?.Invoke(this, EventArgs.Empty);
            UpdateButtonStates();
        }

        private void UpdateButtonStates() {
            bool hasSelection = treeViewLayers.SelectedNode != null;
            buttonRemove.Enabled = false;
            bool layerSelected = false;
            bool batchSelected = false;
            TreeNode? sel = treeViewLayers.SelectedNode;
            if (sel != null) {
                layerSelected = sel.Tag is LayerNode;
                batchSelected = sel.Tag is BatchInfo;
                buttonRemove.Enabled = layerSelected;
            }
            // Determine move up/down enablement
            if (batchSelected) {
                TreeNode? parent = sel!.Parent;
                if (parent != null) {
                    buttonMoveUp.Enabled = sel.Index > 0;
                    buttonMoveDown.Enabled = sel.Index < parent.Nodes.Count - 1;
                } else {
                    buttonMoveUp.Enabled = false;
                    buttonMoveDown.Enabled = false;
                }
            } else if (layerSelected) {
                TreeNode? parent = sel!.Parent;
                if (parent != null) {
                    buttonMoveUp.Enabled = sel.Index > 0;
                    buttonMoveDown.Enabled = sel.Index < parent.Nodes.Count - 1;
                } else {
                    buttonMoveUp.Enabled = false;
                    buttonMoveDown.Enabled = false;
                }
            } else {
                buttonMoveUp.Enabled = false;
                buttonMoveDown.Enabled = false;
            }
            buttonToggleVisibility.Enabled = layerSelected || batchSelected;
        }

        private void button_replaceTileset_Click(object sender, EventArgs e)
        {
            ReplaceTilesetClicked?.Invoke(this, EventArgs.Empty);
        }

        private void buttonAddTileLayer_Click(object sender, EventArgs e) {
            using (var dialog = new Form()) {
                dialog.Text = "Add Tile Layer";
                dialog.Size = new Size(350, 210);
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;

                Label labelName = new Label {
                    Text = "Name:",
                    Location = new Point(10, 20),
                    Size = new Size(60, 20)
                };

                TextBox textBoxName = new TextBox {
                    Location = new Point(80, 18),
                    Size = new Size(240, 23)
                };

                Label labelTileset = new Label {
                    Text = "Tileset:",
                    Location = new Point(10, 55),
                    Size = new Size(60, 20)
                };

                ComboBox comboBoxTileset = new ComboBox {
                    Location = new Point(80, 53),
                    Size = new Size(240, 23),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                
                // Load available tilesets
                int count = _externView?.GetTilesetCount() ?? 0;
                for (int i = 0; i < count; i++) {
                    Externs.TilesetInfoStruct tilesetInfo = new Externs.TilesetInfoStruct();
                    int result = _externView?.GetTilesetAt(i, out tilesetInfo) ?? 0;
                    
                    if (result != 0) {
                        string tilesetName = Marshal.PtrToStringAnsi(tilesetInfo.name) ?? "";
                        if (!string.IsNullOrEmpty(tilesetName)) {
                            comboBoxTileset.Items.Add(tilesetName);
                        }
                    }
                }
                if (comboBoxTileset.Items.Count > 0) {
                    comboBoxTileset.SelectedIndex = 0;
                }

                Button buttonOk = new Button {
                    Text = "Add",
                    DialogResult = DialogResult.OK,
                    Location = new Point(165, 130),
                    Size = new Size(75, 30)
                };

                Button buttonCancel = new Button {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(245, 130),
                    Size = new Size(75, 30)
                };

                dialog.Controls.AddRange(new Control[] { 
                    labelName, textBoxName, labelTileset, comboBoxTileset, buttonOk, buttonCancel 
                });
                dialog.AcceptButton = buttonOk;
                dialog.CancelButton = buttonCancel;

                if (dialog.ShowDialog(this) == DialogResult.OK && 
                    !string.IsNullOrWhiteSpace(textBoxName.Text) &&
                    comboBoxTileset.SelectedItem != null) {
                    AddLayer(textBoxName.Text.Trim(), LayerType.TileLayer, comboBoxTileset.SelectedItem?.ToString() ?? "");
                } else if (dialog.DialogResult == DialogResult.OK) {
                    MessageBox.Show("Please enter a name and select a tileset.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void buttonAddEntityLayer_Click(object sender, EventArgs e) {
            using (var dialog = new Form()) {
                dialog.Text = "Add Entity Layer";
                dialog.Size = new Size(350, 130);
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;

                Label labelName = new Label {
                    Text = "Name:",
                    Location = new Point(10, 20),
                    Size = new Size(60, 20)
                };

                TextBox textBoxName = new TextBox {
                    Location = new Point(80, 18),
                    Size = new Size(240, 23)
                };

                Button buttonOk = new Button {
                    Text = "Add",
                    DialogResult = DialogResult.OK,
                    Location = new Point(165, 70),
                    Size = new Size(75, 30)
                };

                Button buttonCancel = new Button {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(245, 70),
                    Size = new Size(75, 30)
                };

                dialog.Controls.AddRange(new Control[] { labelName, textBoxName, buttonOk, buttonCancel });
                dialog.AcceptButton = buttonOk;
                dialog.CancelButton = buttonCancel;

                if (dialog.ShowDialog(this) == DialogResult.OK && 
                    !string.IsNullOrWhiteSpace(textBoxName.Text)) {
                    AddLayer(textBoxName.Text.Trim(), LayerType.EntityLayer);
                } else if (dialog.DialogResult == DialogResult.OK) {
                    MessageBox.Show("Please enter a name.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e) {
            if (treeViewLayers.SelectedNode != null) {
                LayerNode? layer = treeViewLayers.SelectedNode.Tag as LayerNode;
                if (layer != null) {
                    DialogResult result = MessageBox.Show(
                        $"Are you sure you want to delete layer '{layer.Name}'?",
                        "Confirm Delete", 
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes) {
                        RemoveSelectedLayer();
                    }
                }
            }
        }

        private void buttonMoveUp_Click(object sender, EventArgs e) {
            MoveLayerUp();
        }

        private void buttonMoveDown_Click(object sender, EventArgs e) {
            MoveLayerDown();
        }

        private void buttonToggleVisibility_Click(object sender, EventArgs e) {
            ToggleLayerVisibility();
        }

        private void treeViewLayers_AfterSelect(object sender, TreeViewEventArgs e) {
            UpdateButtonStates();
            
            if (e.Node?.Tag is LayerNode layer) {
                // Notify backend that this layer is now active
                _externView?.SetActiveLayer(layer.Name);

                // If the entity layer is already expanded, refresh its batch children
                if (layer.Type == LayerType.EntityLayer && layer.TreeNodeRef.IsExpanded) {
                    UpdateEntityLayerBatches(layer);
                }

                // Notify any listeners in C#
                LayerSelected?.Invoke(this, layer);
            } else if (_stateNode != null && e.Node == _stateNode) {
                // state row selected
                StateSelected?.Invoke(this, EventArgs.Empty);
            } else if (e.Node?.Tag is BatchInfo batch) {
                // user clicked a batch group under an entity layer
                BatchSelected?.Invoke(this, batch.TilesetName);
            }
        }

        private void treeViewLayers_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            ToggleLayerVisibility();
        }

        private void TreeViewLayers_DrawNode(object? sender, DrawTreeNodeEventArgs e) {
            if (e.Node == null) return;

            // special fixed parent node
            if (_stateNode != null && e.Node == _stateNode) {
                int fullRowWidth = treeViewLayers.ClientSize.Width;
                Rectangle fullRowBounds = new Rectangle(0, e.Bounds.Top, fullRowWidth, e.Bounds.Height);
                Color backColor = (e.State & TreeNodeStates.Selected) != 0
                    ? Color.FromArgb(51, 153, 255)
                    : treeViewLayers.BackColor;
                using (SolidBrush brush = new SolidBrush(backColor)) {
                    e.Graphics.FillRectangle(brush, fullRowBounds);
                }
                // draw its icon on left
                int iconY = e.Bounds.Top + (e.Bounds.Height - IconSize) / 2;
                if (_stateIcon == null) {
                    // try resource first
                    try {
                        _stateIcon = Properties.Resources.map;
                    } catch {
                        _stateIcon = null;
                    }
                    // if resource not available, fall back to file
                    if (_stateIcon == null) {
                        try {
                            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "map.png");
                            if (File.Exists(path))
                                _stateIcon = Image.FromFile(path);
                        } catch {
                            _stateIcon = null;
                        }
                    }
                }
                if (_stateIcon != null) {
                    e.Graphics.DrawImage(_stateIcon, 2, iconY, IconSize, IconSize);
                }

                // draw its text using its nodefont/color (shifted right of icon)
                Font font = e.Node.NodeFont ?? treeViewLayers.Font;
                Color textColor = (e.State & TreeNodeStates.Selected) != 0
                    ? Color.White
                    : e.Node.ForeColor;
                TextRenderer.DrawText(e.Graphics, e.Node.Text, font,
                    new Rectangle(IconSize + IconSpacing, e.Bounds.Top, fullRowWidth - (IconSize + IconSpacing), e.Bounds.Height),
                    textColor, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
                return;
            }

            // If this is a layer node, draw the usual icons
            if (e.Node.Tag is LayerNode layer) {
                // Use full row width
                int fullRowWidth = treeViewLayers.ClientSize.Width;
                Rectangle fullRowBounds = new Rectangle(0, e.Bounds.Top, fullRowWidth, e.Bounds.Height);

                // Draw background for full row
                Color backColor = (e.State & TreeNodeStates.Selected) != 0 
                    ? Color.FromArgb(51, 153, 255) 
                    : treeViewLayers.BackColor;
                using (SolidBrush brush = new SolidBrush(backColor)) {
                    e.Graphics.FillRectangle(brush, fullRowBounds);
                }

                // Draw expand/collapse triangle in the empty space on the left if node has children
                // Draw triangle and icon at a fixed position from the control's left edge
                int leftEdge = 2; // 2px margin from the very left
                int triangleSize = 8;
                int triangleX = leftEdge;
                int triangleY = e.Bounds.Top + (e.Bounds.Height - triangleSize) / 2;
                int iconY = e.Bounds.Top + (e.Bounds.Height - IconSize) / 2;
                int typeIconX = triangleX + triangleSize + 2; // icon immediately after triangle, 2px gap
                if (e.Node.Nodes.Count > 0) {
                    Point[] triangle;
                    if (e.Node.IsExpanded) {
                        triangle = new[] {
                            new Point(triangleX, triangleY),
                            new Point(triangleX + triangleSize, triangleY),
                            new Point(triangleX + triangleSize/2, triangleY + triangleSize)
                        };
                    } else {
                        triangle = new[] {
                            new Point(triangleX, triangleY),
                            new Point(triangleX, triangleY + triangleSize),
                            new Point(triangleX + triangleSize, triangleY + triangleSize/2)
                        };
                    }
                    e.Graphics.FillPolygon(Brushes.Black, triangle);
                }
                Image typeIcon = layer.Type == LayerType.TileLayer 
                    ? Properties.Resources.tiles 
                    : Properties.Resources.entities;
                e.Graphics.DrawImage(typeIcon, typeIconX, iconY, IconSize, IconSize);

                // Calculate space needed for icons (2 icons + spacing on right)
                int iconsWidth = (IconSize * 2) + (IconSpacing * 3);

                // Calculate text bounds (leave room for type icon on left and action icons on right)
                int textStartX = typeIconX + IconSize + IconSpacing;
                Rectangle textBounds = new Rectangle(
                    textStartX,
                    e.Bounds.Top,
                    fullRowWidth - textStartX - iconsWidth,
                    e.Bounds.Height
                );

                // Draw node text with clipping
                Color textColor = (e.State & TreeNodeStates.Selected) != 0 
                    ? Color.White 
                    : treeViewLayers.ForeColor;
                TextRenderer.DrawText(e.Graphics, e.Node.Text, treeViewLayers.Font, 
                    textBounds, textColor, 
                    TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);

                // Calculate icon positions (from right to left, docked to control edge)

                // Draw lock icon (rightmost)
                int lockIconX = fullRowWidth - IconSize - IconSpacing;
                Image lockIcon = layer.Locked 
                    ? Properties.Resources._lock 
                    : Properties.Resources.unlock;
                e.Graphics.DrawImage(lockIcon, lockIconX, iconY, IconSize, IconSize);

                // Draw visibility icon (second from right)
                int visibilityIconX = lockIconX - IconSize - IconSpacing;
                Image visibilityIcon = layer.Visible 
                    ? Properties.Resources.visible 
                    : Properties.Resources.invisible;
                e.Graphics.DrawImage(visibilityIcon, visibilityIconX, iconY, IconSize, IconSize);
                return;
            }

            // If this node is a batch, draw a tile icon on the left with arrow overlay
            if (e.Node.Tag is BatchInfo batch) {
                // draw background normally
                int fullRowWidth = treeViewLayers.ClientSize.Width;
                Rectangle fullRowBounds = new Rectangle(0, e.Bounds.Top, fullRowWidth, e.Bounds.Height);
                Color backColor = (e.State & TreeNodeStates.Selected) != 0 
                    ? Color.FromArgb(51, 153, 255) 
                    : treeViewLayers.BackColor;
                using (SolidBrush brush = new SolidBrush(backColor)) {
                    e.Graphics.FillRectangle(brush, fullRowBounds);
                }

                int indent = 0;
                if (e.Node.Nodes.Count > 0) {
                    int hitWidth = IconSize + IconSpacing + 4;
                    int iconX = e.Bounds.Left + (hitWidth - IconSize) / 2;
                    int iconY = e.Bounds.Top + (e.Bounds.Height - IconSize) / 2;
                    // tile icon inside hit area
                    e.Graphics.DrawImage(Properties.Resources.tiles, iconX, iconY, IconSize, IconSize);
                    // arrow overlay
                    int arrowSize = 6;
                    int ax = iconX + (IconSize - arrowSize) / 2;
                    int ay = iconY + (IconSize - arrowSize) / 2;
                    Point[] arrow;
                    if (e.Node.IsExpanded) {
                        arrow = new[] {
                            new Point(ax, ay),
                            new Point(ax + arrowSize, ay),
                            new Point(ax + arrowSize/2, ay + arrowSize)
                        };
                    } else {
                        arrow = new[] {
                            new Point(ax, ay),
                            new Point(ax, ay + arrowSize),
                            new Point(ax + arrowSize, ay + arrowSize/2)
                        };
                    }
                    e.Graphics.FillPolygon(Brushes.Black, arrow);
                    indent = hitWidth;
                }

                // draw small icon to distinguish (shifted if glyph drawn)
                int iconY2 = e.Bounds.Top + (e.Bounds.Height - IconSize) / 2;
                e.Graphics.DrawImage(Properties.Resources.tiles, e.Bounds.Left + indent, iconY2, IconSize, IconSize);

                // draw text offset by icon
                Rectangle textBounds = new Rectangle(
                    e.Bounds.Left + indent + IconSize + IconSpacing,
                    e.Bounds.Top,
                    fullRowWidth - (indent + IconSize + IconSpacing),
                    e.Bounds.Height
                );
                Color textColor = (e.State & TreeNodeStates.Selected) != 0 
                    ? Color.White 
                    : treeViewLayers.ForeColor;
                TextRenderer.DrawText(e.Graphics, e.Node.Text, treeViewLayers.Font, 
                    textBounds, textColor, 
                    TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
                return;
            }

            // nothing else to draw for other nodes (fallback to default appearance)
        }

        private void TreeViewLayers_MouseDown(object? sender, MouseEventArgs e) {
            TreeNode node = treeViewLayers.GetNodeAt(e.Location);
            if (node == null) return;

            // allow selecting the state root node
            if (_stateNode != null && node == _stateNode) {
                treeViewLayers.SelectedNode = node;
                return;
            }

            LayerNode? layer = node.Tag as LayerNode;
            if (layer == null) return;

            // Use full control width for icon positioning
            int fullRowWidth = treeViewLayers.ClientSize.Width;
            
            // Calculate icon bounds (from right to left, docked to control edge)
            int lockIconX = fullRowWidth - IconSize - IconSpacing;
            int visibilityIconX = lockIconX - IconSize - IconSpacing;
            int iconY = node.Bounds.Top + (node.Bounds.Height - IconSize) / 2;

            Rectangle lockIconBounds = new Rectangle(lockIconX, iconY, IconSize, IconSize);
            Rectangle visibilityIconBounds = new Rectangle(visibilityIconX, iconY, IconSize, IconSize);

            // Check if click was on lock icon
            if (lockIconBounds.Contains(e.Location)) {
                treeViewLayers.SelectedNode = node;
                layer.Locked = !layer.Locked;
                treeViewLayers.Invalidate();
                LayersChanged?.Invoke(this, EventArgs.Empty);
                return;
            }

            // Check if click was on visibility icon
            if (visibilityIconBounds.Contains(e.Location)) {
                treeViewLayers.SelectedNode = node;
                layer.Visible = !layer.Visible;
                treeViewLayers.Invalidate();
                LayersChanged?.Invoke(this, EventArgs.Empty);
                return;
            }

            // support clicking anywhere in the left-hand hit zone (triangle + padding)
            if (node.Nodes.Count > 0) {
                int hitWidth = IconSize + IconSpacing + 4;
                int leftEdge = 2; // match drawing offset
                Rectangle hitRect = new Rectangle(leftEdge, node.Bounds.Top, hitWidth, node.Bounds.Height);
                if (hitRect.Contains(e.Location)) {
                    node.Toggle();
                    return;
                }
            }
        }

        private void TreeViewLayers_KeyDown(object? sender, KeyEventArgs e) {
            // Suppress all default TreeView keyboard behavior to prevent error sounds
            // The main form handles all keyboard input via KeyPreview
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void TreeViewLayers_BeforeCollapse(object? sender, TreeViewCancelEventArgs e) {
            if (_stateNode != null && e.Node == _stateNode) {
                e.Cancel = true;
            }
        }

        // --- drag/drop helpers ------------------------------------------------
        private void TreeViewLayers_ItemDrag(object? sender, ItemDragEventArgs e) {
            if (e.Item is TreeNode node) {
                // only allow dragging real layers or batches
                if (node.Tag is LayerNode || node.Tag is BatchInfo) {
                    DoDragDrop(node, DragDropEffects.Move);
                }
            }
        }

        private void TreeViewLayers_DragEnter(object? sender, DragEventArgs e) {
            e.Effect = DragDropEffects.Move;
        }

        private void TreeViewLayers_DragOver(object? sender, DragEventArgs e) {
            if (e == null) return;
            Point pt = treeViewLayers.PointToClient(new Point(e.X, e.Y));
            TreeNode? target = treeViewLayers.GetNodeAt(pt);
            if (e.Data == null) { e.Effect = DragDropEffects.None; return; }
            TreeNode? dragged = e.Data.GetData(typeof(TreeNode)) as TreeNode;
            if (dragged == null) {
                e.Effect = DragDropEffects.None;
                return;
            }
            bool canDrop = false;
            if (dragged.Tag is LayerNode) {
                // drop onto another layer or onto whitespace at bottom within state node
                TreeNode parent = GetStateNode();
                if (target != null) {
                    // allow drop if we hit a layer or the state node itself
                    canDrop = target.Tag is LayerNode || target == parent;
                } else {
                    // check if cursor below last layer
                    if (parent.Nodes.Count > 0) {
                        TreeNode last = parent.Nodes[parent.Nodes.Count - 1];
                        if (pt.Y > last.Bounds.Bottom) {
                            canDrop = true;
                        }
                    }
                }
            } else if (dragged.Tag is BatchInfo) {
                TreeNode parent = dragged.Parent!;
                // allow drop anywhere within vertical range spanned by batches of same parent
                if (parent.Nodes.Count > 0) {
                    int top = parent.Nodes[0].Bounds.Top;
                    int bottom = parent.Nodes[parent.Nodes.Count - 1].Bounds.Bottom;
                    if (pt.Y >= top && pt.Y <= bottom) {
                        canDrop = true;
                    }
                }
            }
            e.Effect = canDrop ? DragDropEffects.Move : DragDropEffects.None;
        }

        private void TreeViewLayers_DragDrop(object? sender, DragEventArgs e) {
            if (e == null) return;
            Point pt = treeViewLayers.PointToClient(new Point(e.X, e.Y));
            TreeNode? target = treeViewLayers.GetNodeAt(pt);
            if (e.Data == null) return;
            TreeNode? dragged = e.Data.GetData(typeof(TreeNode)) as TreeNode;
            if (dragged == null || dragged == target) return;

            if (dragged.Tag is LayerNode) {
                TreeNode parent = dragged.Parent ?? GetStateNode();
                int fromIndex = dragged.Index;
                int toIndex;
                if (target != null) {
                    TreeNode dest = (target.Tag is LayerNode) ? target : target.Parent ?? target;
                    toIndex = dest.Index;
                } else {
                    // drop at end of parent
                    toIndex = parent.Nodes.Count - 1;
                }
                if (fromIndex == toIndex) return;
                if (target != null && fromIndex < toIndex) {
                    toIndex--;
                }
                TreeNodeCollection nodes = parent.Nodes;
                nodes.RemoveAt(fromIndex);
                nodes.Insert(toIndex, dragged);
                treeViewLayers.SelectedNode = dragged;
                LayerNode? layer = dragged.Tag as LayerNode;
                if (layer != null && _layers.Contains(layer)) {
                    _layers.RemoveAt(fromIndex);
                    _layers.Insert(toIndex, layer);
                }
                _externView?.MoveLayerTo(layer?.Name ?? "", toIndex);
                LayersChanged?.Invoke(this, EventArgs.Empty);
            } else if (dragged.Tag is BatchInfo) {
                TreeNode parent = dragged.Parent!;
                int fromIndex = dragged.Index;
                int toIndex = parent.Nodes.Count;
                // calculate insertion index by y position
                for (int i = 0; i < parent.Nodes.Count; i++) {
                    var childBounds = parent.Nodes[i].Bounds;
                    if (pt.Y < childBounds.Top + childBounds.Height / 2) {
                        toIndex = i;
                        break;
                    }
                }
                if (fromIndex == toIndex) return;
                if (fromIndex < toIndex) toIndex--;
                parent.Nodes.RemoveAt(fromIndex);
                parent.Nodes.Insert(toIndex, dragged);
                treeViewLayers.SelectedNode = dragged;
                if (parent.Tag is LayerNode layer) {
                    _externView?.MoveEntityLayerBatchTo(layer.Name, fromIndex, toIndex);
                }
            }
        }

        // --- end drag/drop ----------------------------------------------------
    }
}
