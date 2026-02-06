using csharp_editor.UserControls;
namespace csharp_editor.UserControls {
    partial class Console {
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
            logView = new csharp_editor.LogView();
            inputView1 = new csharp_editor.InputView();
            inputView2 = new csharp_editor.InputView();
            SuspendLayout();
            // 
            // label_log
            // 
            label_log.AutoSize = true;
            label_log.Dock = System.Windows.Forms.DockStyle.Top;
            label_log.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            label_log.Location = new System.Drawing.Point(4, 4);
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
            logView.Location = new System.Drawing.Point(4, 19);
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
            // inputView2
            // 
            inputView2.Dock = System.Windows.Forms.DockStyle.Bottom;
            inputView2.Location = new System.Drawing.Point(4, 234);
            inputView2.Margin = new System.Windows.Forms.Padding(0);
            inputView2.Name = "inputView2";
            inputView2.Size = new System.Drawing.Size(272, 18);
            inputView2.TabIndex = 2;
            // 
            // Console
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            Controls.Add(logView);
            Controls.Add(inputView2);
            Controls.Add(label_log);
            Margin = new System.Windows.Forms.Padding(4);
            Padding = new System.Windows.Forms.Padding(4);
            Size = new System.Drawing.Size(280, 256);
            ResumeLayout(false);
            PerformLayout();
        }

        private csharp_editor.InputView inputView2;

        private csharp_editor.InputView inputView1;

        private csharp_editor.LogView logView;

        #endregion

        private System.Windows.Forms.Label label_log;
    }
}
