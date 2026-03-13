namespace csharp_editor.Dialogs {
    partial class TilesetCreateDialog {
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
        private void InitializeComponent()
        {
            labelName = new System.Windows.Forms.Label();
            textBoxName = new System.Windows.Forms.TextBox();
            labelImagePath = new System.Windows.Forms.Label();
            textBoxImagePath = new System.Windows.Forms.TextBox();
            buttonBrowse = new System.Windows.Forms.Button();
            buttonCreate = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new System.Drawing.Point(12, 20);
            labelName.Name = "labelName";
            labelName.Size = new System.Drawing.Size(42, 15);
            labelName.TabIndex = 0;
            labelName.Text = "Name:";
            // 
            // textBoxName
            // 
            textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            textBoxName.Location = new System.Drawing.Point(95, 17);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new System.Drawing.Size(465, 23);
            textBoxName.TabIndex = 1;
            // 
            // labelImagePath
            // 
            labelImagePath.AutoSize = true;
            labelImagePath.Location = new System.Drawing.Point(12, 50);
            labelImagePath.Name = "labelImagePath";
            labelImagePath.Size = new System.Drawing.Size(70, 15);
            labelImagePath.TabIndex = 2;
            labelImagePath.Text = "Image Path:";
            // 
            // textBoxImagePath
            // 
            textBoxImagePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            textBoxImagePath.Location = new System.Drawing.Point(95, 47);
            textBoxImagePath.Name = "textBoxImagePath";
            textBoxImagePath.Size = new System.Drawing.Size(374, 23);
            textBoxImagePath.TabIndex = 3;
            // 
            // buttonBrowse
            // 
            buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            buttonBrowse.Location = new System.Drawing.Point(474, 46);
            buttonBrowse.Name = "buttonBrowse";
            buttonBrowse.Size = new System.Drawing.Size(86, 23);
            buttonBrowse.TabIndex = 4;
            buttonBrowse.Text = "Browse...";
            buttonBrowse.UseVisualStyleBackColor = true;
            buttonBrowse.Click += buttonBrowse_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            buttonCancel.Location = new System.Drawing.Point(389, 85);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(85, 27);
            buttonCancel.TabIndex = 5;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonCreate
            // 
            buttonCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            buttonCreate.Location = new System.Drawing.Point(479, 85);
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new System.Drawing.Size(81, 27);
            buttonCreate.TabIndex = 6;
            buttonCreate.Text = "Create";
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.Click += buttonCreate_Click;
            // 
            // TilesetCreateDialog
            // 
            AcceptButton = buttonCreate;
            CancelButton = buttonCancel;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(584, 125);
            Controls.Add(buttonCreate);
            Controls.Add(buttonCancel);
            Controls.Add(buttonBrowse);
            Controls.Add(textBoxImagePath);
            Controls.Add(labelImagePath);
            Controls.Add(textBoxName);
            Controls.Add(labelName);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "New Tileset";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelImagePath;
        private System.Windows.Forms.TextBox textBoxImagePath;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonCancel;
    }
}
