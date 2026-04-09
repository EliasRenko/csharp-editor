namespace csharp_editor.Dialogs {
    partial class ColorPickerDialog {

        private System.ComponentModel.IContainer components = null!;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pickerCanvas = new csharp_editor.Dialogs.ColorPickerDialog.DoubleBufferedPanel();
            hueStrip = new csharp_editor.Dialogs.ColorPickerDialog.DoubleBufferedPanel();
            satStrip = new csharp_editor.Dialogs.ColorPickerDialog.DoubleBufferedPanel();
            valStrip = new csharp_editor.Dialogs.ColorPickerDialog.DoubleBufferedPanel();
            previewOld = new System.Windows.Forms.Panel();
            previewNew = new System.Windows.Forms.Panel();
            lblHex = new System.Windows.Forms.Label();
            hexBox = new System.Windows.Forms.TextBox();
            lblCh1 = new System.Windows.Forms.Label();
            lblCh2 = new System.Windows.Forms.Label();
            lblCh3 = new System.Windows.Forms.Label();
            ch1Box = new System.Windows.Forms.TextBox();
            ch2Box = new System.Windows.Forms.TextBox();
            ch3Box = new System.Windows.Forms.TextBox();
            btnMode = new System.Windows.Forms.Button();
            btnOk = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            panelBottom = new System.Windows.Forms.Panel();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // pickerCanvas
            // 
            pickerCanvas.BackColor = System.Drawing.Color.Black;
            pickerCanvas.Cursor = System.Windows.Forms.Cursors.Cross;
            pickerCanvas.Location = new System.Drawing.Point(12, 12);
            pickerCanvas.Name = "pickerCanvas";
            pickerCanvas.Size = new System.Drawing.Size(360, 280);
            pickerCanvas.TabIndex = 0;
            pickerCanvas.Paint += pickerCanvas_Paint;
            pickerCanvas.MouseDown += pickerCanvas_MouseDown;
            pickerCanvas.MouseMove += pickerCanvas_MouseMove;
            pickerCanvas.MouseUp += pickerCanvas_MouseUp;
            pickerCanvas.Resize += pickerCanvas_Resize;
            // 
            // hueStrip
            // 
            hueStrip.BackColor = System.Drawing.Color.Black;
            hueStrip.Cursor = System.Windows.Forms.Cursors.Hand;
            hueStrip.Location = new System.Drawing.Point(12, 300);
            hueStrip.Name = "hueStrip";
            hueStrip.Size = new System.Drawing.Size(360, 22);
            hueStrip.TabIndex = 1;
            hueStrip.Paint += hueStrip_Paint;
            hueStrip.MouseDown += hueStrip_MouseDown;
            hueStrip.MouseMove += hueStrip_MouseMove;
            hueStrip.MouseUp += hueStrip_MouseUp;
            hueStrip.Resize += hueStrip_Resize;
            // 
            // satStrip
            // 
            satStrip.BackColor = System.Drawing.Color.Black;
            satStrip.Cursor = System.Windows.Forms.Cursors.Hand;
            satStrip.Location = new System.Drawing.Point(12, 330);
            satStrip.Name = "satStrip";
            satStrip.Size = new System.Drawing.Size(360, 22);
            satStrip.TabIndex = 14;
            satStrip.Paint += satStrip_Paint;
            satStrip.MouseDown += satStrip_MouseDown;
            satStrip.MouseMove += satStrip_MouseMove;
            satStrip.MouseUp += satStrip_MouseUp;
            satStrip.Resize += satStrip_Resize;
            // 
            // valStrip
            // 
            valStrip.BackColor = System.Drawing.Color.Black;
            valStrip.Cursor = System.Windows.Forms.Cursors.Hand;
            valStrip.Location = new System.Drawing.Point(12, 360);
            valStrip.Name = "valStrip";
            valStrip.Size = new System.Drawing.Size(360, 22);
            valStrip.TabIndex = 15;
            valStrip.Paint += valStrip_Paint;
            valStrip.MouseDown += valStrip_MouseDown;
            valStrip.MouseMove += valStrip_MouseMove;
            valStrip.MouseUp += valStrip_MouseUp;
            valStrip.Resize += valStrip_Resize;
            // 
            // previewOld
            // 
            previewOld.BackColor = System.Drawing.Color.Gray;
            previewOld.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            previewOld.Location = new System.Drawing.Point(12, 390);
            previewOld.Name = "previewOld";
            previewOld.Size = new System.Drawing.Size(44, 30);
            previewOld.TabIndex = 2;
            // 
            // previewNew
            // 
            previewNew.BackColor = System.Drawing.Color.White;
            previewNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            previewNew.Location = new System.Drawing.Point(58, 390);
            previewNew.Name = "previewNew";
            previewNew.Size = new System.Drawing.Size(44, 30);
            previewNew.TabIndex = 3;
            // 
            // lblHex
            // 
            lblHex.AutoSize = true;
            lblHex.Font = new System.Drawing.Font("Segoe UI", 8F);
            lblHex.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)153)), ((int)((byte)153)), ((int)((byte)153)));
            lblHex.Location = new System.Drawing.Point(116, 390);
            lblHex.Name = "lblHex";
            lblHex.Size = new System.Drawing.Size(26, 13);
            lblHex.TabIndex = 4;
            lblHex.Text = "Hex";
            // 
            // hexBox
            // 
            hexBox.BackColor = System.Drawing.Color.FromArgb(((int)((byte)60)), ((int)((byte)60)), ((int)((byte)60)));
            hexBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            hexBox.Font = new System.Drawing.Font("Consolas", 9F);
            hexBox.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)212)), ((int)((byte)212)), ((int)((byte)212)));
            hexBox.Location = new System.Drawing.Point(116, 408);
            hexBox.Name = "hexBox";
            hexBox.Size = new System.Drawing.Size(80, 22);
            hexBox.TabIndex = 5;
            hexBox.KeyDown += hexBox_KeyDown;
            hexBox.Leave += hexBox_Leave;
            // 
            // lblCh1
            // 
            lblCh1.AutoSize = true;
            lblCh1.Font = new System.Drawing.Font("Segoe UI", 8F);
            lblCh1.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)153)), ((int)((byte)153)), ((int)((byte)153)));
            lblCh1.Location = new System.Drawing.Point(210, 390);
            lblCh1.Name = "lblCh1";
            lblCh1.Size = new System.Drawing.Size(27, 13);
            lblCh1.TabIndex = 6;
            lblCh1.Text = "Red";
            // 
            // lblCh2
            // 
            lblCh2.AutoSize = true;
            lblCh2.Font = new System.Drawing.Font("Segoe UI", 8F);
            lblCh2.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)153)), ((int)((byte)153)), ((int)((byte)153)));
            lblCh2.Location = new System.Drawing.Point(262, 390);
            lblCh2.Name = "lblCh2";
            lblCh2.Size = new System.Drawing.Size(38, 13);
            lblCh2.TabIndex = 8;
            lblCh2.Text = "Green";
            // 
            // lblCh3
            // 
            lblCh3.AutoSize = true;
            lblCh3.Font = new System.Drawing.Font("Segoe UI", 8F);
            lblCh3.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)153)), ((int)((byte)153)), ((int)((byte)153)));
            lblCh3.Location = new System.Drawing.Point(314, 390);
            lblCh3.Name = "lblCh3";
            lblCh3.Size = new System.Drawing.Size(29, 13);
            lblCh3.TabIndex = 10;
            lblCh3.Text = "Blue";
            // 
            // ch1Box
            // 
            ch1Box.BackColor = System.Drawing.Color.FromArgb(((int)((byte)60)), ((int)((byte)60)), ((int)((byte)60)));
            ch1Box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            ch1Box.Font = new System.Drawing.Font("Consolas", 9F);
            ch1Box.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)212)), ((int)((byte)212)), ((int)((byte)212)));
            ch1Box.Location = new System.Drawing.Point(210, 408);
            ch1Box.Name = "ch1Box";
            ch1Box.Size = new System.Drawing.Size(44, 22);
            ch1Box.TabIndex = 7;
            ch1Box.KeyDown += ch1Box_KeyDown;
            ch1Box.Leave += ch1Box_Leave;
            // 
            // ch2Box
            // 
            ch2Box.BackColor = System.Drawing.Color.FromArgb(((int)((byte)60)), ((int)((byte)60)), ((int)((byte)60)));
            ch2Box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            ch2Box.Font = new System.Drawing.Font("Consolas", 9F);
            ch2Box.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)212)), ((int)((byte)212)), ((int)((byte)212)));
            ch2Box.Location = new System.Drawing.Point(262, 408);
            ch2Box.Name = "ch2Box";
            ch2Box.Size = new System.Drawing.Size(44, 22);
            ch2Box.TabIndex = 9;
            ch2Box.KeyDown += ch2Box_KeyDown;
            ch2Box.Leave += ch2Box_Leave;
            // 
            // ch3Box
            // 
            ch3Box.BackColor = System.Drawing.Color.FromArgb(((int)((byte)60)), ((int)((byte)60)), ((int)((byte)60)));
            ch3Box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            ch3Box.Font = new System.Drawing.Font("Consolas", 9F);
            ch3Box.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)212)), ((int)((byte)212)), ((int)((byte)212)));
            ch3Box.Location = new System.Drawing.Point(314, 408);
            ch3Box.Name = "ch3Box";
            ch3Box.Size = new System.Drawing.Size(44, 22);
            ch3Box.TabIndex = 11;
            ch3Box.KeyDown += ch3Box_KeyDown;
            ch3Box.Leave += ch3Box_Leave;
            // 
            // btnMode
            // 
            btnMode.BackColor = System.Drawing.Color.FromArgb(((int)((byte)62)), ((int)((byte)62)), ((int)((byte)66)));
            btnMode.Cursor = System.Windows.Forms.Cursors.Hand;
            btnMode.FlatAppearance.BorderSize = 0;
            btnMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnMode.Font = new System.Drawing.Font("Segoe UI", 9F);
            btnMode.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)212)), ((int)((byte)212)), ((int)((byte)212)));
            btnMode.Location = new System.Drawing.Point(12, 434);
            btnMode.Name = "btnMode";
            btnMode.Size = new System.Drawing.Size(56, 24);
            btnMode.TabIndex = 12;
            btnMode.Text = "RGB";
            btnMode.UseVisualStyleBackColor = false;
            btnMode.Click += btnMode_Click;
            // 
            // btnOk
            // 
            btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            btnOk.BackColor = System.Drawing.Color.FromArgb(((int)((byte)14)), ((int)((byte)99)), ((int)((byte)156)));
            btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnOk.Font = new System.Drawing.Font("Segoe UI", 9F);
            btnOk.ForeColor = System.Drawing.Color.White;
            btnOk.Location = new System.Drawing.Point(10, 11);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(80, 28);
            btnOk.TabIndex = 0;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = false;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)((byte)62)), ((int)((byte)62)), ((int)((byte)66)));
            btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)212)), ((int)((byte)212)), ((int)((byte)212)));
            btnCancel.Location = new System.Drawing.Point(466, 10);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(80, 28);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // panelBottom
            // 
            panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)((byte)37)), ((int)((byte)37)), ((int)((byte)38)));
            panelBottom.Controls.Add(btnOk);
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelBottom.Location = new System.Drawing.Point(0, 470);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new System.Drawing.Size(384, 48);
            panelBottom.TabIndex = 13;
            // 
            // ColorPickerDialog
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(((int)((byte)45)), ((int)((byte)45)), ((int)((byte)48)));
            CancelButton = btnCancel;
            ClientSize = new System.Drawing.Size(384, 518);
            Controls.Add(pickerCanvas);
            Controls.Add(hueStrip);
            Controls.Add(satStrip);
            Controls.Add(valStrip);
            Controls.Add(previewOld);
            Controls.Add(previewNew);
            Controls.Add(lblHex);
            Controls.Add(hexBox);
            Controls.Add(lblCh1);
            Controls.Add(ch1Box);
            Controls.Add(lblCh2);
            Controls.Add(ch2Box);
            Controls.Add(lblCh3);
            Controls.Add(ch3Box);
            Controls.Add(btnMode);
            Controls.Add(panelBottom);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Color Picker";
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private DoubleBufferedPanel pickerCanvas;
        private DoubleBufferedPanel hueStrip;
        private DoubleBufferedPanel satStrip;
        private DoubleBufferedPanel valStrip;
        private System.Windows.Forms.Panel  previewOld;
        private System.Windows.Forms.Panel  previewNew;
        private System.Windows.Forms.Label  lblHex;
        private System.Windows.Forms.TextBox hexBox;
        private System.Windows.Forms.Label  lblCh1;
        private System.Windows.Forms.Label  lblCh2;
        private System.Windows.Forms.Label  lblCh3;
        private System.Windows.Forms.TextBox ch1Box;
        private System.Windows.Forms.TextBox ch2Box;
        private System.Windows.Forms.TextBox ch3Box;
        private System.Windows.Forms.Button btnMode;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel  panelBottom;

        /// <summary>Panel with double-buffering enabled to prevent flicker on custom-paint.</summary>
        private sealed class DoubleBufferedPanel : System.Windows.Forms.Panel {
            public DoubleBufferedPanel() {
                DoubleBuffered = true;
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            }
        }
    }
}
