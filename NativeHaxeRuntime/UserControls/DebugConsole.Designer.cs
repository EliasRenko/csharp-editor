using csharp_editor.UserControls;
namespace csharp_editor.UserControls {
    partial class DebugConsole {
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
            label_log = new System.Windows.Forms.Label();
            logView = new csharp_editor.UserControls.LogView();
            inputView1 = new csharp_editor.UserControls.InputView();
            panel1 = new System.Windows.Forms.Panel();
            button_clear = new System.Windows.Forms.Button();
            button_copy = new System.Windows.Forms.Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label_log
            // 
            label_log.AutoSize = true;
            label_log.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            label_log.Location = new System.Drawing.Point(9, 9);
            label_log.Margin = new System.Windows.Forms.Padding(0);
            label_log.Name = "label_log";
            label_log.Size = new System.Drawing.Size(50, 15);
            label_log.TabIndex = 0;
            label_log.Text = "Console";
            // 
            // logView
            // 
            logView.BackColor = System.Drawing.SystemColors.Control;
            logView.Dock = System.Windows.Forms.DockStyle.Fill;
            logView.Location = new System.Drawing.Point(4, 37);
            logView.Margin = new System.Windows.Forms.Padding(0);
            logView.Name = "logView";
            logView.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            logView.Size = new System.Drawing.Size(272, 215);
            logView.TabIndex = 1;
            // 
            // inputView1
            // 
            inputView1.Location = new System.Drawing.Point(0, 0);
            inputView1.Name = "inputView1";
            inputView1.Size = new System.Drawing.Size(258, 18);
            inputView1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(button_clear);
            panel1.Controls.Add(button_copy);
            panel1.Controls.Add(label_log);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(4, 4);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(272, 33);
            panel1.TabIndex = 3;
            // 
            // button_clear
            // 
            button_clear.Location = new System.Drawing.Point(125, 5);
            button_clear.Name = "button_clear";
            button_clear.Size = new System.Drawing.Size(48, 23);
            button_clear.TabIndex = 2;
            button_clear.Text = "Clear";
            button_clear.UseVisualStyleBackColor = true;
            // 
            // button_copy
            // 
            button_copy.Location = new System.Drawing.Point(71, 5);
            button_copy.Name = "button_copy";
            button_copy.Size = new System.Drawing.Size(48, 23);
            button_copy.TabIndex = 1;
            button_copy.Text = "Copy";
            button_copy.UseVisualStyleBackColor = true;
            // 
            // DebugConsole
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            Controls.Add(logView);
            Controls.Add(panel1);
            Margin = new System.Windows.Forms.Padding(4);
            Padding = new System.Windows.Forms.Padding(4);
            Size = new System.Drawing.Size(280, 256);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Button button_clear;

        private System.Windows.Forms.Button button_copy;

        private System.Windows.Forms.Panel panel1;

        private InputView inputView1;

        private csharp_editor.UserControls.LogView logView;

        #endregion

        private System.Windows.Forms.Label label_log;
    }
}
