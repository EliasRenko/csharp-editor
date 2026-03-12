namespace csharp_editor.Dialogs {
    partial class TilesetRegionDialog {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.textureViewer = new csharp_editor.UserControls.TextureViewer();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.labelRegion = new System.Windows.Forms.Label();
            this.labelSuggestion = new System.Windows.Forms.Label();
            this.checkBoxSnapToGrid = new System.Windows.Forms.CheckBox();
            this.labelGridSize = new System.Windows.Forms.Label();
            this.numericUpDownGridSize = new System.Windows.Forms.NumericUpDown();
            this.checkBoxShowGrid = new System.Windows.Forms.CheckBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.timerUpdateLabel = new System.Windows.Forms.Timer(this.components);
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.numericUpDownGridSize).BeginInit();
            this.SuspendLayout();
            // 
            // textureViewer
            // 
            this.textureViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textureViewer.Location = new System.Drawing.Point(0, 0);
            this.textureViewer.Name = "textureViewer";
            this.textureViewer.Size = new System.Drawing.Size(784, 481);
            this.textureViewer.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.labelRegion);
            this.panelBottom.Controls.Add(this.labelSuggestion);
            this.panelBottom.Controls.Add(this.checkBoxSnapToGrid);
            this.panelBottom.Controls.Add(this.labelGridSize);
            this.panelBottom.Controls.Add(this.numericUpDownGridSize);
            this.panelBottom.Controls.Add(this.checkBoxShowGrid);
            this.panelBottom.Controls.Add(this.buttonOK);
            this.panelBottom.Controls.Add(this.buttonCancel);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 481);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(784, 80);
            this.panelBottom.TabIndex = 1;
            // 
            // labelRegion
            // 
            this.labelRegion.AutoSize = true;
            this.labelRegion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelRegion.Location = new System.Drawing.Point(12, 15);
            this.labelRegion.Name = "labelRegion";
            this.labelRegion.Size = new System.Drawing.Size(95, 15);
            this.labelRegion.TabIndex = 2;
            this.labelRegion.Text = "Selected: None";
            // 
            // labelSuggestion
            // 
            this.labelSuggestion.AutoSize = true;
            this.labelSuggestion.Location = new System.Drawing.Point(12, 40);
            this.labelSuggestion.Name = "labelSuggestion";
            this.labelSuggestion.Size = new System.Drawing.Size(67, 15);
            this.labelSuggestion.TabIndex = 3;
            this.labelSuggestion.Text = "Suggestion:";
            // 
            // checkBoxSnapToGrid
            // 
            this.checkBoxSnapToGrid.AutoSize = true;
            this.checkBoxSnapToGrid.Checked = true;
            this.checkBoxSnapToGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSnapToGrid.Location = new System.Drawing.Point(250, 15);
            this.checkBoxSnapToGrid.Name = "checkBoxSnapToGrid";
            this.checkBoxSnapToGrid.Size = new System.Drawing.Size(110, 19);
            this.checkBoxSnapToGrid.TabIndex = 4;
            this.checkBoxSnapToGrid.Text = "Snap to grid";
            this.checkBoxSnapToGrid.UseVisualStyleBackColor = true;
            // 
            // labelGridSize
            // 
            this.labelGridSize.AutoSize = true;
            this.labelGridSize.Location = new System.Drawing.Point(375, 16);
            this.labelGridSize.Name = "labelGridSize";
            this.labelGridSize.Size = new System.Drawing.Size(55, 15);
            this.labelGridSize.TabIndex = 5;
            this.labelGridSize.Text = "Grid size:";
            // 
            // numericUpDownGridSize
            // 
            this.numericUpDownGridSize.Location = new System.Drawing.Point(438, 12);
            this.numericUpDownGridSize.Maximum = new decimal(new int[] { 512, 0, 0, 0 });
            this.numericUpDownGridSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericUpDownGridSize.Name = "numericUpDownGridSize";
            this.numericUpDownGridSize.Size = new System.Drawing.Size(65, 23);
            this.numericUpDownGridSize.TabIndex = 6;
            this.numericUpDownGridSize.Value = new decimal(new int[] { 32, 0, 0, 0 });
            // 
            // checkBoxShowGrid
            // 
            this.checkBoxShowGrid.AutoSize = true;
            this.checkBoxShowGrid.Location = new System.Drawing.Point(516, 15);
            this.checkBoxShowGrid.Name = "checkBoxShowGrid";
            this.checkBoxShowGrid.Size = new System.Drawing.Size(85, 19);
            this.checkBoxShowGrid.TabIndex = 7;
            this.checkBoxShowGrid.Text = "Show grid";
            this.checkBoxShowGrid.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(586, 43);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(90, 25);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(682, 43);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(90, 25);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // timerUpdateLabel
            // 
            this.timerUpdateLabel.Enabled = true;
            this.timerUpdateLabel.Interval = 100;
            this.timerUpdateLabel.Tick += new System.EventHandler(this.timerUpdateLabel_Tick);
            // 
            // TilesetRegionDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.textureViewer);
            this.Controls.Add(this.panelBottom);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "TilesetRegionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Tileset Region";
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.numericUpDownGridSize).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private csharp_editor.UserControls.TextureViewer textureViewer;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label labelRegion;
        private System.Windows.Forms.Label labelSuggestion;
        private System.Windows.Forms.CheckBox checkBoxSnapToGrid;
        private System.Windows.Forms.Label labelGridSize;
        private System.Windows.Forms.NumericUpDown numericUpDownGridSize;
        private System.Windows.Forms.CheckBox checkBoxShowGrid;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Timer timerUpdateLabel;
    }
}
