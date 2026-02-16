namespace csharp_editor {
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
        private void InitializeComponent() {
            this.treeViewLayers = new System.Windows.Forms.TreeView();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonToggleVisibility = new System.Windows.Forms.Button();
            this.buttonMoveDown = new System.Windows.Forms.Button();
            this.buttonMoveUp = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAddTileLayer = new System.Windows.Forms.Button();
            this.buttonAddEntityLayer = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewLayers
            // 
            this.treeViewLayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewLayers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeViewLayers.Location = new System.Drawing.Point(0, 25);
            this.treeViewLayers.Name = "treeViewLayers";
            this.treeViewLayers.Size = new System.Drawing.Size(250, 375);
            this.treeViewLayers.TabIndex = 0;
            this.treeViewLayers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewLayers_AfterSelect);
            this.treeViewLayers.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewLayers_NodeMouseDoubleClick);
            // 
            // panelButtons
            // 
            this.panelButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelButtons.Controls.Add(this.buttonToggleVisibility);
            this.panelButtons.Controls.Add(this.buttonMoveDown);
            this.panelButtons.Controls.Add(this.buttonMoveUp);
            this.panelButtons.Controls.Add(this.buttonRemove);
            this.panelButtons.Controls.Add(this.buttonAddTileLayer);
            this.panelButtons.Controls.Add(this.buttonAddEntityLayer);
            this.panelButtons.Location = new System.Drawing.Point(0, 405);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(250, 95);
            this.panelButtons.TabIndex = 1;
            // 
            // buttonToggleVisibility
            // 
            this.buttonToggleVisibility.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));

            this.buttonToggleVisibility.Location = new System.Drawing.Point(5, 65);
            this.buttonToggleVisibility.Name = "buttonToggleVisibility";
            this.buttonToggleVisibility.Size = new System.Drawing.Size(240, 25);
            this.buttonToggleVisibility.TabIndex = 4;
            this.buttonToggleVisibility.Text = "Toggle Visibility";
            this.buttonToggleVisibility.Click += new System.EventHandler(this.buttonToggleVisibility_Click);
            // 
            // buttonMoveDown
            // 

            this.buttonMoveDown.Location = new System.Drawing.Point(125, 35);
            this.buttonMoveDown.Name = "buttonMoveDown";
            this.buttonMoveDown.Size = new System.Drawing.Size(60, 25);
            this.buttonMoveDown.TabIndex = 3;
            this.buttonMoveDown.Text = "Down";
            this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
            // 
            // buttonMoveUp
            // 

            this.buttonMoveUp.Location = new System.Drawing.Point(65, 35);
            this.buttonMoveUp.Name = "buttonMoveUp";
            this.buttonMoveUp.Size = new System.Drawing.Size(55, 25);
            this.buttonMoveUp.TabIndex = 2;
            this.buttonMoveUp.Text = "Up";
            this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
            // 
            // buttonRemove
            // 

            this.buttonRemove.Location = new System.Drawing.Point(5, 35);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(55, 25);
            this.buttonRemove.TabIndex = 1;
            this.buttonRemove.Text = "Delete";
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAddTileLayer
            // 
            this.buttonAddTileLayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));

            this.buttonAddTileLayer.Location = new System.Drawing.Point(5, 5);
            this.buttonAddTileLayer.Name = "buttonAddTileLayer";
            this.buttonAddTileLayer.Size = new System.Drawing.Size(115, 25);
            this.buttonAddTileLayer.TabIndex = 0;
            this.buttonAddTileLayer.Text = "Add Tile Layer";
            this.buttonAddTileLayer.Click += new System.EventHandler(this.buttonAddTileLayer_Click);
            // 
            // buttonAddEntityLayer
            // 
            this.buttonAddEntityLayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));

            this.buttonAddEntityLayer.Location = new System.Drawing.Point(125, 5);
            this.buttonAddEntityLayer.Name = "buttonAddEntityLayer";
            this.buttonAddEntityLayer.Size = new System.Drawing.Size(120, 25);
            this.buttonAddEntityLayer.TabIndex = 5;
            this.buttonAddEntityLayer.Text = "Add Entity Layer";
            this.buttonAddEntityLayer.Click += new System.EventHandler(this.buttonAddEntityLayer_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.labelTitle.Size = new System.Drawing.Size(250, 25);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "Hierarchy";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // HierarchyTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.treeViewLayers);
            this.Name = "HierarchyTree";
            this.Size = new System.Drawing.Size(250, 500);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewLayers;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button buttonAddTileLayer;
        private System.Windows.Forms.Button buttonAddEntityLayer;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonMoveUp;
        private System.Windows.Forms.Button buttonMoveDown;
        private System.Windows.Forms.Button buttonToggleVisibility;
        private System.Windows.Forms.Label labelTitle;
    }
}
