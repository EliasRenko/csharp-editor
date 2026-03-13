namespace csharp_editor.UserControls {
    partial class HierarchyTree {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            treeViewLayers = new System.Windows.Forms.TreeView();
            toolStrip_layers = new System.Windows.Forms.ToolStrip();
            toolStripButton_addLayer = new System.Windows.Forms.ToolStripDropDownButton();
            toolStripMenuItem_addTileLayer = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_addEntityLayer = new System.Windows.Forms.ToolStripMenuItem();
            toolStripButton_remove = new System.Windows.Forms.ToolStripButton();
            toolStripButton_editLayer = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButton_moveUp = new System.Windows.Forms.ToolStripButton();
            toolStripButton_moveDown = new System.Windows.Forms.ToolStripButton();
            labelTitle = new System.Windows.Forms.Label();
            toolStrip_layers.SuspendLayout();
            SuspendLayout();
            // 
            // treeViewLayers
            // 
            treeViewLayers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            treeViewLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            treeViewLayers.Location = new System.Drawing.Point(0, 25);
            treeViewLayers.Name = "treeViewLayers";
            treeViewLayers.Size = new System.Drawing.Size(250, 450);
            treeViewLayers.TabIndex = 0;
            treeViewLayers.AfterSelect += treeViewLayers_AfterSelect;
            treeViewLayers.NodeMouseDoubleClick += treeViewLayers_NodeMouseDoubleClick;
            // 
            // toolStrip_layers
            // 
            toolStrip_layers.Dock = System.Windows.Forms.DockStyle.Bottom;
            toolStrip_layers.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip_layers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripButton_addLayer, toolStripButton_remove, toolStripButton_editLayer, toolStripSeparator2, toolStripButton_moveUp, toolStripButton_moveDown });
            toolStrip_layers.Location = new System.Drawing.Point(0, 475);
            toolStrip_layers.Name = "toolStrip_layers";
            toolStrip_layers.Size = new System.Drawing.Size(250, 25);
            toolStrip_layers.TabIndex = 1;
            // 
            // toolStripButton_addLayer
            // 
            toolStripButton_addLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton_addLayer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem_addTileLayer, toolStripMenuItem_addEntityLayer });
            toolStripButton_addLayer.Image = global::csharp_editor.Properties.Resources.layer_add;
            toolStripButton_addLayer.Name = "toolStripButton_addLayer";
            toolStripButton_addLayer.Size = new System.Drawing.Size(29, 22);
            toolStripButton_addLayer.ToolTipText = "Add Layer";
            // 
            // toolStripMenuItem_addTileLayer
            // 
            toolStripMenuItem_addTileLayer.Name = "toolStripMenuItem_addTileLayer";
            toolStripMenuItem_addTileLayer.Size = new System.Drawing.Size(160, 22);
            toolStripMenuItem_addTileLayer.Text = "Add Tile Layer";
            toolStripMenuItem_addTileLayer.Click += toolStripButton_addTileLayer_Click;
            // 
            // toolStripMenuItem_addEntityLayer
            // 
            toolStripMenuItem_addEntityLayer.Name = "toolStripMenuItem_addEntityLayer";
            toolStripMenuItem_addEntityLayer.Size = new System.Drawing.Size(160, 22);
            toolStripMenuItem_addEntityLayer.Text = "Add Entity Layer";
            toolStripMenuItem_addEntityLayer.Click += toolStripButton_addEntityLayer_Click;
            // 
            // toolStripButton_remove
            // 
            toolStripButton_remove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton_remove.Image = global::csharp_editor.Properties.Resources.layer_delete;
            toolStripButton_remove.Name = "toolStripButton_remove";
            toolStripButton_remove.Size = new System.Drawing.Size(23, 22);
            toolStripButton_remove.ToolTipText = "Delete Layer";
            toolStripButton_remove.Click += toolStripButton_remove_Click;
            // 
            // toolStripButton_editLayer
            // 
            toolStripButton_editLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton_editLayer.Image = global::csharp_editor.Properties.Resources.layer_edit;
            toolStripButton_editLayer.Name = "toolStripButton_editLayer";
            toolStripButton_editLayer.Size = new System.Drawing.Size(23, 22);
            toolStripButton_editLayer.ToolTipText = "Edit Layer";
            toolStripButton_editLayer.Click += toolStripButton_editLayer_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_moveUp
            // 
            toolStripButton_moveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton_moveUp.Image = global::csharp_editor.Properties.Resources.arrow_up;
            toolStripButton_moveUp.Name = "toolStripButton_moveUp";
            toolStripButton_moveUp.Size = new System.Drawing.Size(23, 22);
            toolStripButton_moveUp.ToolTipText = "Move Up";
            toolStripButton_moveUp.Click += toolStripButton_moveUp_Click;
            // 
            // toolStripButton_moveDown
            // 
            toolStripButton_moveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton_moveDown.Image = global::csharp_editor.Properties.Resources.arrow_down;
            toolStripButton_moveDown.Name = "toolStripButton_moveDown";
            toolStripButton_moveDown.Size = new System.Drawing.Size(23, 22);
            toolStripButton_moveDown.ToolTipText = "Move Down";
            toolStripButton_moveDown.Click += toolStripButton_moveDown_Click;
            // 
            // labelTitle
            // 
            labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            labelTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            labelTitle.Location = new System.Drawing.Point(0, 0);
            labelTitle.Name = "labelTitle";
            labelTitle.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            labelTitle.Size = new System.Drawing.Size(250, 25);
            labelTitle.TabIndex = 2;
            labelTitle.Text = "Hierarchy";
            labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // HierarchyTree
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(treeViewLayers);
            Controls.Add(toolStrip_layers);
            Controls.Add(labelTitle);
            Size = new System.Drawing.Size(250, 500);
            toolStrip_layers.ResumeLayout(false);
            toolStrip_layers.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TreeView treeViewLayers;
        private System.Windows.Forms.ToolStrip toolStrip_layers;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton_addLayer;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_addTileLayer;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_addEntityLayer;
        private System.Windows.Forms.ToolStripButton toolStripButton_remove;
        private System.Windows.Forms.ToolStripButton toolStripButton_editLayer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton_moveUp;
        private System.Windows.Forms.ToolStripButton toolStripButton_moveDown;
        private System.Windows.Forms.Label labelTitle;
    }
}
