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
            treeViewLayers = new TreeView();
            panelButtons = new Panel();
            buttonToggleVisibility = new Button();
            buttonMoveDown = new Button();
            buttonMoveUp = new Button();
            buttonRemove = new Button();
            buttonAddTileLayer = new Button();
            buttonAddEntityLayer = new Button();
            labelTitle = new Label();
            button_replaceTileset = new Button();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // treeViewLayers
            // 
            treeViewLayers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            treeViewLayers.BorderStyle = BorderStyle.FixedSingle;
            treeViewLayers.Location = new Point(0, 25);
            treeViewLayers.Name = "treeViewLayers";
            treeViewLayers.Size = new Size(250, 375);
            treeViewLayers.TabIndex = 0;
            treeViewLayers.AfterSelect += treeViewLayers_AfterSelect;
            treeViewLayers.NodeMouseDoubleClick += treeViewLayers_NodeMouseDoubleClick;
            // 
            // panelButtons
            // 
            panelButtons.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelButtons.Controls.Add(button_replaceTileset);
            panelButtons.Controls.Add(buttonToggleVisibility);
            panelButtons.Controls.Add(buttonMoveDown);
            panelButtons.Controls.Add(buttonMoveUp);
            panelButtons.Controls.Add(buttonRemove);
            panelButtons.Controls.Add(buttonAddTileLayer);
            panelButtons.Controls.Add(buttonAddEntityLayer);
            panelButtons.Location = new Point(0, 405);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(250, 95);
            panelButtons.TabIndex = 1;
            // 
            // buttonToggleVisibility
            // 
            buttonToggleVisibility.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonToggleVisibility.Location = new Point(5, 65);
            buttonToggleVisibility.Name = "buttonToggleVisibility";
            buttonToggleVisibility.Size = new Size(240, 25);
            buttonToggleVisibility.TabIndex = 4;
            buttonToggleVisibility.Text = "Toggle Visibility";
            buttonToggleVisibility.Click += buttonToggleVisibility_Click;
            // 
            // buttonMoveDown
            // 
            buttonMoveDown.Location = new Point(125, 35);
            buttonMoveDown.Name = "buttonMoveDown";
            buttonMoveDown.Size = new Size(60, 25);
            buttonMoveDown.TabIndex = 3;
            buttonMoveDown.Text = "Down";
            buttonMoveDown.Click += buttonMoveDown_Click;
            // 
            // buttonMoveUp
            // 
            buttonMoveUp.Location = new Point(65, 35);
            buttonMoveUp.Name = "buttonMoveUp";
            buttonMoveUp.Size = new Size(55, 25);
            buttonMoveUp.TabIndex = 2;
            buttonMoveUp.Text = "Up";
            buttonMoveUp.Click += buttonMoveUp_Click;
            // 
            // buttonRemove
            // 
            buttonRemove.Location = new Point(5, 35);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new Size(55, 25);
            buttonRemove.TabIndex = 1;
            buttonRemove.Text = "Delete";
            buttonRemove.Click += buttonRemove_Click;
            // 
            // buttonAddTileLayer
            // 
            buttonAddTileLayer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonAddTileLayer.Location = new Point(5, 5);
            buttonAddTileLayer.Name = "buttonAddTileLayer";
            buttonAddTileLayer.Size = new Size(115, 25);
            buttonAddTileLayer.TabIndex = 0;
            buttonAddTileLayer.Text = "Add Tile Layer";
            buttonAddTileLayer.Click += buttonAddTileLayer_Click;
            // 
            // buttonAddEntityLayer
            // 
            buttonAddEntityLayer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonAddEntityLayer.Location = new Point(125, 5);
            buttonAddEntityLayer.Name = "buttonAddEntityLayer";
            buttonAddEntityLayer.Size = new Size(120, 25);
            buttonAddEntityLayer.TabIndex = 5;
            buttonAddEntityLayer.Text = "Add Entity Layer";
            buttonAddEntityLayer.Click += buttonAddEntityLayer_Click;
            // 
            // labelTitle
            // 
            labelTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelTitle.Location = new Point(0, 0);
            labelTitle.Name = "labelTitle";
            labelTitle.Padding = new Padding(5, 0, 0, 0);
            labelTitle.Size = new Size(250, 25);
            labelTitle.TabIndex = 2;
            labelTitle.Text = "Hierarchy";
            labelTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // button_replaceTileset
            // 
            button_replaceTileset.Location = new Point(187, 35);
            button_replaceTileset.Name = "button_replaceTileset";
            button_replaceTileset.Size = new Size(60, 25);
            button_replaceTileset.TabIndex = 6;
            button_replaceTileset.Text = "Replace";
            button_replaceTileset.Click += button_replaceTileset_Click;
            // 
            // HierarchyTree
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(labelTitle);
            Controls.Add(panelButtons);
            Controls.Add(treeViewLayers);
            Name = "HierarchyTree";
            Size = new Size(250, 500);
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);

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
        private Button button_replaceTileset;
    }
}
