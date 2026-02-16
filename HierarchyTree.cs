using System;
using System.Collections.Generic;
using System.Drawing;
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
            public TreeNode TreeNodeRef { get; set; }
            
            public override string ToString() {
                return Name;
            }
        }

        private List<LayerNode> _layers = new List<LayerNode>();
        private ExternView _externView;
        private const int IconSize = 16;
        private const int IconSpacing = 4;
        
        public event EventHandler<LayerNode>? LayerSelected;
        public event EventHandler? LayersChanged;

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
            treeViewLayers.DrawMode = TreeViewDrawMode.OwnerDrawText;
            treeViewLayers.DrawNode += TreeViewLayers_DrawNode;
            treeViewLayers.MouseDown += TreeViewLayers_MouseDown;
            treeViewLayers.KeyDown += TreeViewLayers_KeyDown;
            UpdateButtonStates();
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

            treeViewLayers.Nodes.Add(treeNode);
            _layers.Add(layer);

            // Notify backend
            if (type == LayerType.TileLayer) {
                _externView?.CreateTilemapLayer(name, tilesetName);
            } else if (type == LayerType.EntityLayer) {
                _externView?.CreateEntityLayer(name);
            }

            LayersChanged?.Invoke(this, EventArgs.Empty);
            UpdateButtonStates();
        }

        public void RemoveSelectedLayer() {
            if (treeViewLayers.SelectedNode != null) {
                TreeNode selectedNode = treeViewLayers.SelectedNode;
                LayerNode layer = selectedNode.Tag as LayerNode;

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
            int index = node.Index;
            
            if (index > 0) {
                TreeNode parent = node.Parent;
                TreeNodeCollection nodes = parent?.Nodes ?? treeViewLayers.Nodes;
                
                nodes.RemoveAt(index);
                nodes.Insert(index - 1, node);
                treeViewLayers.SelectedNode = node;

                // Update layer list order
                LayerNode layer = node.Tag as LayerNode;
                if (layer != null && _layers.Contains(layer)) {
                    int layerIndex = _layers.IndexOf(layer);
                    if (layerIndex > 0) {
                        _layers.RemoveAt(layerIndex);
                        _layers.Insert(layerIndex - 1, layer);
                    }
                }

                LayersChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void MoveLayerDown() {
            if (treeViewLayers.SelectedNode == null) return;
            
            TreeNode node = treeViewLayers.SelectedNode;
            TreeNode parent = node.Parent;
            TreeNodeCollection nodes = parent?.Nodes ?? treeViewLayers.Nodes;
            int index = node.Index;
            
            if (index < nodes.Count - 1) {
                nodes.RemoveAt(index);
                nodes.Insert(index + 1, node);
                treeViewLayers.SelectedNode = node;

                // Update layer list order
                LayerNode layer = node.Tag as LayerNode;
                if (layer != null && _layers.Contains(layer)) {
                    int layerIndex = _layers.IndexOf(layer);
                    if (layerIndex < _layers.Count - 1) {
                        _layers.RemoveAt(layerIndex);
                        _layers.Insert(layerIndex + 1, layer);
                    }
                }

                LayersChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void ToggleLayerVisibility() {
            if (treeViewLayers.SelectedNode != null) {
                TreeNode node = treeViewLayers.SelectedNode;
                LayerNode layer = node.Tag as LayerNode;

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
                LayerNode layer = node.Tag as LayerNode;

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

        public List<LayerNode> GetAllLayers() {
            return new List<LayerNode>(_layers);
        }

        public void ClearLayers() {
            treeViewLayers.Nodes.Clear();
            _layers.Clear();

            LayersChanged?.Invoke(this, EventArgs.Empty);
            UpdateButtonStates();
        }
        
        public void LoadLayersFromBackend() {
            // Clear existing layers
            treeViewLayers.Nodes.Clear();
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
                            TilesetName = tilesetName
                        };

                        TreeNode treeNode = new TreeNode(layer.ToString());
                        treeNode.Tag = layer;
                        layer.TreeNodeRef = treeNode;

                        treeViewLayers.Nodes.Add(treeNode);
                        _layers.Add(layer);
                    }
                }
            }
            
            LayersChanged?.Invoke(this, EventArgs.Empty);
            UpdateButtonStates();
        }

        private void UpdateButtonStates() {
            bool hasSelection = treeViewLayers.SelectedNode != null;
            bool hasLayers = treeViewLayers.Nodes.Count > 0;
            
            buttonRemove.Enabled = hasSelection;
            buttonMoveUp.Enabled = hasSelection && treeViewLayers.SelectedNode?.Index > 0;
            buttonMoveDown.Enabled = hasSelection && 
                treeViewLayers.SelectedNode?.Index < (treeViewLayers.Nodes.Count - 1);
            buttonToggleVisibility.Enabled = hasSelection;
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
                    int result = _externView.GetTilesetAt(i, out tilesetInfo);
                    
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
                    AddLayer(textBoxName.Text.Trim(), LayerType.TileLayer, comboBoxTileset.SelectedItem.ToString());
                } else if (dialog.DialogResult == DialogResult.OK) {
                    MessageBox.Show("Please enter a name and select a tileset.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void buttonAddEntityLayer_Click(object sender, EventArgs e) {
            using (var dialog = new Form()) {
                dialog.Text = "Add Entity Layer";
                dialog.Size = new Size(350, 150);
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

                dialog.Controls.AddRange(new Control[] { 
                    labelName, textBoxName, buttonOk, buttonCancel 
                });
                dialog.AcceptButton = buttonOk;
                dialog.CancelButton = buttonCancel;

                if (dialog.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(textBoxName.Text)) {
                    AddLayer(textBoxName.Text.Trim(), LayerType.EntityLayer);
                } else if (dialog.DialogResult == DialogResult.OK) {
                    MessageBox.Show("Please enter a name.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e) {
            if (treeViewLayers.SelectedNode != null) {
                LayerNode layer = treeViewLayers.SelectedNode.Tag as LayerNode;
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
                
                // Notify any listeners in C#
                LayerSelected?.Invoke(this, layer);
            }
        }

        private void treeViewLayers_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            ToggleLayerVisibility();
        }

        private void TreeViewLayers_DrawNode(object sender, DrawTreeNodeEventArgs e) {
            if (e.Node == null) return;

            LayerNode layer = e.Node.Tag as LayerNode;
            if (layer == null) return;

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

            // Draw type icon on the left
            int typeIconX = e.Bounds.Left;
            int iconY = e.Bounds.Top + (e.Bounds.Height - IconSize) / 2;
            
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
            Color textColor = treeViewLayers.ForeColor;
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
        }

        private void TreeViewLayers_MouseDown(object sender, MouseEventArgs e) {
            TreeNode node = treeViewLayers.GetNodeAt(e.Location);
            if (node == null) return;

            LayerNode layer = node.Tag as LayerNode;
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
        }

        private void TreeViewLayers_KeyDown(object sender, KeyEventArgs e) {
            // Suppress all default TreeView keyboard behavior to prevent error sounds
            // The main form handles all keyboard input via KeyPreview
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }
}
