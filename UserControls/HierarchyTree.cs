using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using csharp_editor.Dialogs;
using csharp_editor.Models;
using ToolStripRenderer = csharp_editor.Styles.ToolStripRenderer;

namespace csharp_editor.UserControls {
    public partial class HierarchyTree : UserControl {
        
        /// <summary>
        /// Holds information about a batch group inside an entity layer.
        /// Used as the Tag for batch child nodes.
        /// </summary>
        private class BatchInfo {
            public string TilesetName { get; set; } = "";
            public int Index { get; set; }
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
        private const int TriangleSize = 8;       // expand/collapse glyph size
        private const int IndentWidth = 20;       // pixels per tree-level indent
        private const int BaseIndent = 4;         // left margin for the root level
        private static Image? _stateIcon = null; // loaded lazily from Icons/map.png
        
        public event EventHandler<LayerNode>? LayerSelected;
        public event EventHandler? StateSelected;
        public event EventHandler<(string TilesetName, int BatchIndex)>? BatchSelected;
        public event EventHandler? LayersChanged;

        public HierarchyTree() {
            InitializeComponent();

            toolStrip_layers.Renderer = new ToolStripRenderer();
            
            InitializeTreeView();
            // create the immutable "State" root up front so it shows even when no layers exist
            GetStateNode();
        }

        public void SetExternView(ExternView externView) {
            _externView = externView;
        }

        private void InitializeTreeView() {
            treeViewLayers.HideSelection = false;
            treeViewLayers.FullRowSelect = true;
            treeviewLayersBeforeExpandHook();
            treeViewLayers.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            treeViewLayers.ShowPlusMinus = false;
            treeViewLayers.ShowLines = false;
            treeViewLayers.ShowRootLines = false;
            treeViewLayers.Indent = IndentWidth;
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
            int batchCount = CExternsEditor.GetEntityLayerBatchCount(layer.Name);
            for (int i = 0; i < batchCount; i++) {
                string? tilesetName = CExternsEditor.GetEntityLayerBatchTilesetName(layer.Name, i);
                int entityCount = CExternsEditor.GetEntityLayerBatchCountAt(i);
                string display = tilesetName ?? "<unknown>";
                // create node with BatchInfo tag so we can style it later
                TreeNode batchNode = new TreeNode(display + $" ({entityCount})");
                batchNode.Tag = new BatchInfo {
                    TilesetName = tilesetName ?? "",
                    Index = i,
                    EntityCount = entityCount
                };
                // make batches visually distinct
                batchNode.ForeColor = Color.Gray;
                batchNode.NodeFont = new Font(treeViewLayers.Font, FontStyle.Italic);
                node.Nodes.Add(batchNode);
            }
        }

        public void AddLayer(string name, LayerType type, string tilesetName = "", int tileSize = 0) {
            // Determine insert index from current selection BEFORE touching the tree
            TreeNode parent = GetStateNode();
            int insertIndex;
            if (treeViewLayers.SelectedNode != null &&
                treeViewLayers.SelectedNode.Tag is LayerNode &&
                treeViewLayers.SelectedNode.Parent == parent) {
                insertIndex = treeViewLayers.SelectedNode.Index;
            } else {
                insertIndex = parent.Nodes.Count;
            }

            // Create in backend FIRST so the layer exists when AfterSelect fires
            if (type == LayerType.TileLayer) {
                if (!CExternsEditor.CreateTilemapLayer(name, tilesetName, tileSize, insertIndex)) {
                    string error = _externView?.GetLastErrorMessage() ?? "Unknown error.";
                    MessageBox.Show($"Failed to create tile layer '{name}':\n{error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            } else if (type == LayerType.EntityLayer) {
                // API no longer requires a tileset for entity layers
                CExternsEditor.CreateEntityLayer(name);
            }

            // Now add to the tree — layer is guaranteed to exist in the backend
            LayerNode layer = new LayerNode {
                Name = name,
                Type = type,
                Visible = true,
                TilesetName = tilesetName,
                TileSize = tileSize
            };

            TreeNode treeNode = new TreeNode(layer.ToString());
            treeNode.Tag = layer;
            layer.TreeNodeRef = treeNode;

            parent.Nodes.Insert(insertIndex, treeNode);
            _layers.Insert(insertIndex, layer);
            // make sure the state container remains expanded so the new child is visible
            parent.Expand();
            // selecting fires AfterSelect which calls SetActiveLayer — layer already exists
            treeViewLayers.SelectedNode = treeNode;
            // request a repaint in case owner-draw hasn't been triggered yet
            treeViewLayers.Invalidate();

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
                    bool removeOk = CExternsEditor.RemoveLayer(layer.Name);
                    if (!removeOk) {
                        string error = _externView?.GetLastErrorMessage() ?? "Unknown error.";
                        MessageBox.Show($"Failed to remove layer '{layer.Name}':\n{error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
                    bool moved = CExternsEditor.MoveLayerUpByIndex(index);
                    if (!moved) {
                        string error = _externView?.GetLastErrorMessage() ?? "Unknown error.";
                        MessageBox.Show($"Failed to move layer up: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
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
                bool moved = CExternsEditor.MoveEntityLayerBatchUp(layer.Name, currentIndex);
                if (!moved) {
                    string error = _externView?.GetLastErrorMessage() ?? "Unknown error.";
                    MessageBox.Show($"Failed to move batch up on layer '{layer.Name}': {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
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
                    bool moved = CExternsEditor.MoveEntityLayerBatchDown(layer.Name, currentIndex);
                    if (!moved) {
                        string error = _externView?.GetLastErrorMessage() ?? "Unknown error.";
                        MessageBox.Show($"Failed to move batch down on layer '{layer.Name}': {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
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
                    bool moved = CExternsEditor.MoveLayerDownByIndex(index);
                    if (!moved) {
                        string error = _externView?.GetLastErrorMessage() ?? "Unknown error.";
                        MessageBox.Show($"Failed to move layer down: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
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
        /// Refreshes batch groups for every entity layer. Call this after deleting an entity
        /// definition so that batches that became empty are removed from the tree.
        /// </summary>
        public void RefreshAllEntityBatches() {
            foreach (var layer in _layers) {
                if (layer.Type == LayerType.EntityLayer) {
                    UpdateEntityLayerBatches(layer);
                }
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
            int count = CExternsEditor.GetLayerCount();
            System.Diagnostics.Debug.WriteLine($"[LoadLayersFromBackend] GetLayerCount={count}");

            treeViewLayers.BeginUpdate();
            for (int i = 0; i < count; i++) {
                CExternsEditor.LayerStruct layerStruct = new CExternsEditor.LayerStruct();
                bool result = CExternsEditor.GetLayerInfoAt(i, out layerStruct);
                System.Diagnostics.Debug.WriteLine($"[LoadLayersFromBackend] Layer[{i}]: GetLayerInfoAt result={result}");
                
                if (result) {
                    string layerName = Marshal.PtrToStringAnsi(layerStruct.name) ?? "";
                    string tilesetName = Marshal.PtrToStringAnsi(layerStruct.tilesetName) ?? "";
                    System.Diagnostics.Debug.WriteLine($"[LoadLayersFromBackend] Layer[{i}]: name='{layerName}', type={layerStruct.type}, tilesetName='{tilesetName}'");
                    
                    if (!string.IsNullOrEmpty(layerName)) {
                        LayerNode layer = new LayerNode {
                            Name = layerName,
                            Type = (LayerType)layerStruct.type,
                            Visible = layerStruct.visible != 0,
                            TilesetName = tilesetName,
                            TileSize = layerStruct.tileSize,
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
            treeViewLayers.EndUpdate();
            // Expand AFTER children have been added — Win32 TVM_EXPAND is a no-op on empty nodes,
            // so any earlier Expand() call (when the node had 0 children) was silently ignored.
            _stateNode!.Expand();
            treeViewLayers.Refresh(); // force synchronous repaint
            System.Diagnostics.Debug.WriteLine($"[LoadLayersFromBackend] Done. _layers.Count={_layers.Count}, treeViewLayers.Nodes.Count={treeViewLayers.Nodes.Count}, stateNode.Nodes.Count={_stateNode?.Nodes.Count}, stateNode.IsExpanded={_stateNode?.IsExpanded}");
            
            LayersChanged?.Invoke(this, EventArgs.Empty);
            UpdateButtonStates();
        }

        private void UpdateButtonStates() {
            bool hasSelection = treeViewLayers.SelectedNode != null;
            toolStripButton_remove.Enabled = false;
            toolStripButton_editLayer.Enabled = false;
            bool layerSelected = false;
            bool batchSelected = false;
            TreeNode? sel = treeViewLayers.SelectedNode;
            if (sel != null) {
                layerSelected = sel.Tag is LayerNode;
                batchSelected = sel.Tag is BatchInfo;
                toolStripButton_remove.Enabled = layerSelected;
                toolStripButton_editLayer.Enabled = layerSelected;
            }
            // Determine move up/down enablement
            if (batchSelected) {
                TreeNode? parent = sel!.Parent;
                if (parent != null) {
                    toolStripButton_moveUp.Enabled = sel.Index > 0;
                    toolStripButton_moveDown.Enabled = sel.Index < parent.Nodes.Count - 1;
                } else {
                    toolStripButton_moveUp.Enabled = false;
                    toolStripButton_moveDown.Enabled = false;
                }
            } else if (layerSelected) {
                TreeNode? parent = sel!.Parent;
                if (parent != null) {
                    toolStripButton_moveUp.Enabled = sel.Index > 0;
                    toolStripButton_moveDown.Enabled = sel.Index < parent.Nodes.Count - 1;
                } else {
                    toolStripButton_moveUp.Enabled = false;
                    toolStripButton_moveDown.Enabled = false;
                }
            } else {
                toolStripButton_moveUp.Enabled = false;
                toolStripButton_moveDown.Enabled = false;
            }
        }

        private void toolStripButton_addTileLayer_Click(object sender, EventArgs e) {
            using (var dialog = new TileLayerDialog(_externView!)) {
                if (dialog.ShowDialog(this) == DialogResult.OK) {
                    AddLayer(dialog.LayerName, LayerType.TileLayer, dialog.SelectedTileset, dialog.TileSize);
                }
            }
        }

        private void toolStripButton_addEntityLayer_Click(object sender, EventArgs e) {
            using (var dialog = new EntityLayerDialog()) {
                if (dialog.ShowDialog(this) == DialogResult.OK) {
                    AddLayer(dialog.LayerName, LayerType.EntityLayer);
                }
            }
        }

        private void toolStripButton_remove_Click(object sender, EventArgs e) {
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

        private void toolStripButton_editLayer_Click(object sender, EventArgs e) {
            LayerNode? layer = GetSelectedLayer();
            if (layer == null) return;

            if (layer.Type == LayerType.TileLayer) {
                using (var dialog = new TileLayerDialog(_externView!, layer.Name, layer.TilesetName, layer.TileSize)) {
                    if (dialog.ShowDialog(this) == DialogResult.OK) {
                        string newName    = dialog.LayerName;
                        string newTileset = dialog.SelectedTileset;
                        int    newSize    = dialog.TileSize;

                        // Apply tileset change first (uses original layer name)
                        if (newTileset != layer.TilesetName) {
                            bool replaceOk = CExternsEditor.ReplaceLayerTileset(layer.Name, newTileset);
                            if (!replaceOk) {
                                string error = _externView?.GetLastErrorMessage() ?? "Unknown error.";
                                MessageBox.Show($"Failed to replace tileset for layer '{layer.Name}':\n{error}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            layer.TilesetName = newTileset;
                        }

                        // Apply name change
                        if (!string.IsNullOrWhiteSpace(newName) && newName != layer.Name) {
                            bool propsOk = CExternsEditor.SetLayerProperties(layer.Name, newName, layer.Visible, layer.TilesetName);
                            if (!propsOk) {
                                string error = _externView?.GetLastErrorMessage() ?? "Unknown error.";
                                MessageBox.Show($"Failed to set layer properties for '{layer.Name}':\n{error}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            RenameLayer(layer.Name, newName);
                        }

                        layer.TileSize = newSize;

                        // Refresh property grid and texture viewer in Editor
                        LayerSelected?.Invoke(this, layer);
                    }
                }
            } else {
                using (var dialog = new EntityLayerDialog(layer.Name)) {
                    if (dialog.ShowDialog(this) == DialogResult.OK) {
                        string newName = dialog.LayerName;
                        if (!string.IsNullOrWhiteSpace(newName) && newName != layer.Name) {
                            CExternsEditor.SetLayerProperties(layer.Name, newName, layer.Visible);
                            RenameLayer(layer.Name, newName);
                        }
                        LayerSelected?.Invoke(this, layer);
                    }
                }
            }
        }

        private void toolStripButton_moveUp_Click(object sender, EventArgs e) {
            MoveLayerUp();
        }

        private void toolStripButton_moveDown_Click(object sender, EventArgs e) {
            MoveLayerDown();
        }

        private void toolStripButton_toggleVisibility_Click(object sender, EventArgs e) {
            ToggleLayerVisibility();
        }

        private void treeViewLayers_AfterSelect(object sender, TreeViewEventArgs e) {
            UpdateButtonStates();

            if (e.Node?.Tag is LayerNode layer) {
                // Notify backend that this layer is now active
                bool activeOK = CExternsEditor.SetActiveLayer(layer.Name);
                if (!activeOK) {
                    string error = _externView?.GetLastErrorMessage() ?? "Unknown error.";
                    MessageBox.Show($"Failed to activate layer '{layer.Name}':\n{error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

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
                BatchSelected?.Invoke(this, (batch.TilesetName, batch.Index));
            }
        }

        private void treeViewLayers_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            ToggleLayerVisibility();
        }

        private void TreeViewLayers_DrawNode(object? sender, DrawTreeNodeEventArgs e) {
            if (e.Node == null) return;

            int fullRowWidth = treeViewLayers.ClientSize.Width;
            bool selected = (e.State & TreeNodeStates.Selected) != 0;
            Color backColor = selected ? Color.FromArgb(51, 153, 255) : treeViewLayers.BackColor;

            // Fill full-row background
            using (SolidBrush brush = new SolidBrush(backColor))
                e.Graphics.FillRectangle(brush, 0, e.Bounds.Top, fullRowWidth, e.Bounds.Height);

            int iconY  = e.Bounds.Top + (e.Bounds.Height - IconSize) / 2;
            int cx     = BaseIndent + e.Node.Level * IndentWidth; // current x, advances left→right

            // ── expand / collapse glyph ──────────────────────────────────────────────
            int triangleAreaWidth = TriangleSize + IconSpacing;
            if (e.Node.Nodes.Count > 0) {
                int tx = cx + (triangleAreaWidth - TriangleSize) / 2;
                int ty = e.Bounds.Top + (e.Bounds.Height - TriangleSize) / 2;
                Color arrowColor = selected ? Color.White : Color.FromArgb(130, 130, 130);
                using (SolidBrush arrowBrush = new SolidBrush(arrowColor)) {
                    Point[] tri = e.Node.IsExpanded
                        ? new[] { new Point(tx, ty), new Point(tx + TriangleSize, ty), new Point(tx + TriangleSize / 2, ty + TriangleSize) }
                        : new[] { new Point(tx, ty), new Point(tx, ty + TriangleSize), new Point(tx + TriangleSize, ty + TriangleSize / 2) };
                    e.Graphics.FillPolygon(arrowBrush, tri);
                }
            }
            cx += triangleAreaWidth;

            // ── State node ───────────────────────────────────────────────────────────
            if (_stateNode != null && e.Node == _stateNode) {
                if (_stateIcon == null) {
                    try { _stateIcon = Properties.Resources.map; } catch { }
                    if (_stateIcon == null) {
                        try {
                            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "map.png");
                            if (File.Exists(path)) _stateIcon = Image.FromFile(path);
                        } catch { }
                    }
                }
                if (_stateIcon != null) {
                    e.Graphics.DrawImage(_stateIcon, cx, iconY, IconSize, IconSize);
                    cx += IconSize + IconSpacing;
                }
                Font font = e.Node.NodeFont ?? treeViewLayers.Font;
                Color stateTextColor = selected ? Color.White : e.Node.ForeColor;
                TextRenderer.DrawText(e.Graphics, e.Node.Text, font,
                    new Rectangle(cx, e.Bounds.Top, fullRowWidth - cx - IconSpacing, e.Bounds.Height),
                    stateTextColor, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
                return;
            }

            // ── Layer node ───────────────────────────────────────────────────────────
            if (e.Node.Tag is LayerNode layer) {
                // visibility icon — acts as an inline checkbox, left of the type icon
                Image visIcon = layer.Visible ? Properties.Resources.visible : Properties.Resources.invisible;
                e.Graphics.DrawImage(visIcon, cx, iconY, IconSize, IconSize);
                cx += IconSize + IconSpacing;

                // type icon
                Image typeIcon = layer.Type == LayerType.TileLayer
                    ? Properties.Resources.tiles
                    : Properties.Resources.entities;
                e.Graphics.DrawImage(typeIcon, cx, iconY, IconSize, IconSize);
                cx += IconSize + IconSpacing;

                // lock icon — right-aligned
                int lockIconX = fullRowWidth - IconSize - IconSpacing;
                Image lockIcon = layer.Locked ? Properties.Resources._lock : Properties.Resources.unlock;
                e.Graphics.DrawImage(lockIcon, lockIconX, iconY, IconSize, IconSize);

                // name text between left icons and lock
                Color layerTextColor = selected ? Color.White : treeViewLayers.ForeColor;
                int textWidth = lockIconX - cx - IconSpacing;
                TextRenderer.DrawText(e.Graphics, layer.Name, treeViewLayers.Font,
                    new Rectangle(cx, e.Bounds.Top, textWidth, e.Bounds.Height),
                    layerTextColor, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
                return;
            }

            // ── Batch node ───────────────────────────────────────────────────────────
            if (e.Node.Tag is BatchInfo batch) {
                // tile icon
                e.Graphics.DrawImage(Properties.Resources.tiles, cx, iconY, IconSize, IconSize);
                cx += IconSize + IconSpacing;

                Color batchTextColor = selected ? Color.White : treeViewLayers.ForeColor;
                TextRenderer.DrawText(e.Graphics, e.Node.Text, treeViewLayers.Font,
                    new Rectangle(cx, e.Bounds.Top, fullRowWidth - cx - IconSpacing, e.Bounds.Height),
                    batchTextColor, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
                return;
            }

            // fallback: default drawing for unrecognised nodes
        }

        private void TreeViewLayers_MouseDown(object? sender, MouseEventArgs e) {
            TreeNode node = treeViewLayers.GetNodeAt(e.Location);
            if (node == null) return;

            // allow selecting the state root node
            if (_stateNode != null && node == _stateNode) {
                treeViewLayers.SelectedNode = node;
                return;
            }

            // Compute positions that exactly match DrawNode's layout
            int nodeLeft  = BaseIndent + node.Level * IndentWidth;
            int triangleAreaWidth = TriangleSize + IconSpacing;
            int iconY     = node.Bounds.Top + (node.Bounds.Height - IconSize) / 2;

            // Expand / collapse area (the triangle glyph zone)
            if (node.Nodes.Count > 0) {
                Rectangle expandRect = new Rectangle(nodeLeft, node.Bounds.Top, triangleAreaWidth, node.Bounds.Height);
                if (expandRect.Contains(e.Location)) {
                    node.Toggle();
                    return;
                }
            }

            LayerNode? layer = node.Tag as LayerNode;
            if (layer == null) return;

            int cx = nodeLeft + triangleAreaWidth;

            // Visibility icon (inline, left side)
            Rectangle visibilityIconBounds = new Rectangle(cx, iconY, IconSize, IconSize);
            if (visibilityIconBounds.Contains(e.Location)) {
                treeViewLayers.SelectedNode = node;
                layer.Visible = !layer.Visible;
                treeViewLayers.Invalidate();
                LayersChanged?.Invoke(this, EventArgs.Empty);
                return;
            }

            // Lock icon (right-aligned)
            int fullRowWidth = treeViewLayers.ClientSize.Width;
            int lockIconX = fullRowWidth - IconSize - IconSpacing;
            Rectangle lockIconBounds = new Rectangle(lockIconX, iconY, IconSize, IconSize);
            if (lockIconBounds.Contains(e.Location)) {
                treeViewLayers.SelectedNode = node;
                layer.Locked = !layer.Locked;
                treeViewLayers.Invalidate();
                LayersChanged?.Invoke(this, EventArgs.Empty);
                return;
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
                bool moved = CExternsEditor.MoveLayerTo(layer?.Name ?? "", toIndex);
                if (!moved) {
                    string error = _externView?.GetLastErrorMessage() ?? "Unknown error.";
                    MessageBox.Show($"Failed to move layer '{layer?.Name}' to index {toIndex}: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                    bool moved = CExternsEditor.MoveEntityLayerBatchTo(layer.Name, fromIndex, toIndex);
                    if (!moved) {
                        string error = _externView?.GetLastErrorMessage() ?? "Unknown error.";
                        MessageBox.Show($"Failed to move batch to index {toIndex} on layer '{layer.Name}': {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        // --- end drag/drop ----------------------------------------------------
    }
}
