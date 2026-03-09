namespace csharp_editor.UserControls {
    partial class EntitySelector {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent() {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageDefs = new System.Windows.Forms.TabPage();
            this.tabPageInstances = new System.Windows.Forms.TabPage();
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelCount = new System.Windows.Forms.Label();
            this.listBoxEntities = new System.Windows.Forms.ListBox();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.labelSelected = new System.Windows.Forms.Label();
            this.panelInstancesTop = new System.Windows.Forms.Panel();
            this.labelInstanceCount = new System.Windows.Forms.Label();
            this.listBoxInstances = new System.Windows.Forms.ListBox();
            this.tabControl.SuspendLayout();
            this.tabPageDefs.SuspendLayout();
            this.tabPageInstances.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelInstancesTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageDefs);
            this.tabControl.Controls.Add(this.tabPageInstances);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(300, 450);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageDefs
            // 
            this.tabPageDefs.Controls.Add(this.listBoxEntities);
            this.tabPageDefs.Controls.Add(this.panelBottom);
            this.tabPageDefs.Controls.Add(this.panelTop);
            this.tabPageDefs.Location = new System.Drawing.Point(4, 24);
            this.tabPageDefs.Name = "tabPageDefs";
            this.tabPageDefs.Size = new System.Drawing.Size(292, 422);
            this.tabPageDefs.TabIndex = 0;
            this.tabPageDefs.Text = "Definitions";
            this.tabPageDefs.UseVisualStyleBackColor = true;
            // 
            // tabPageInstances
            // 
            this.tabPageInstances.Controls.Add(this.listBoxInstances);
            this.tabPageInstances.Controls.Add(this.panelInstancesTop);
            this.tabPageInstances.Location = new System.Drawing.Point(4, 24);
            this.tabPageInstances.Name = "tabPageInstances";
            this.tabPageInstances.Size = new System.Drawing.Size(292, 422);
            this.tabPageInstances.TabIndex = 1;
            this.tabPageInstances.Text = "Instances";
            this.tabPageInstances.UseVisualStyleBackColor = true;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelCount);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(292, 35);
            this.panelTop.TabIndex = 0;
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(10, 10);
            this.labelCount.Name = "labelCount";
            this.labelCount.TabIndex = 0;
            this.labelCount.Text = "Entities: 0";
            // 
            // listBoxEntities
            // 
            this.listBoxEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxEntities.FormattingEnabled = true;
            this.listBoxEntities.ItemHeight = 15;
            this.listBoxEntities.Location = new System.Drawing.Point(0, 35);
            this.listBoxEntities.Name = "listBoxEntities";
            this.listBoxEntities.Size = new System.Drawing.Size(292, 357);
            this.listBoxEntities.TabIndex = 1;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.labelSelected);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 392);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(292, 30);
            this.panelBottom.TabIndex = 2;
            // 
            // labelSelected
            // 
            this.labelSelected.AutoSize = true;
            this.labelSelected.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelSelected.Location = new System.Drawing.Point(10, 8);
            this.labelSelected.Name = "labelSelected";
            this.labelSelected.TabIndex = 0;
            this.labelSelected.Text = "Selected: None";
            // 
            // panelInstancesTop
            // 
            this.panelInstancesTop.Controls.Add(this.labelInstanceCount);
            this.panelInstancesTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInstancesTop.Location = new System.Drawing.Point(0, 0);
            this.panelInstancesTop.Name = "panelInstancesTop";
            this.panelInstancesTop.Size = new System.Drawing.Size(292, 35);
            this.panelInstancesTop.TabIndex = 0;
            // 
            // labelInstanceCount
            // 
            this.labelInstanceCount.AutoSize = true;
            this.labelInstanceCount.Location = new System.Drawing.Point(10, 10);
            this.labelInstanceCount.Name = "labelInstanceCount";
            this.labelInstanceCount.TabIndex = 0;
            this.labelInstanceCount.Text = "Instances: 0";
            // 
            // listBoxInstances
            // 
            this.listBoxInstances.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxInstances.FormattingEnabled = true;
            this.listBoxInstances.ItemHeight = 15;
            this.listBoxInstances.Location = new System.Drawing.Point(0, 35);
            this.listBoxInstances.Name = "listBoxInstances";
            this.listBoxInstances.Size = new System.Drawing.Size(292, 387);
            this.listBoxInstances.TabIndex = 1;
            // 
            // EntitySelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "EntitySelector";
            this.Size = new System.Drawing.Size(300, 450);
            this.tabControl.ResumeLayout(false);
            this.tabPageDefs.ResumeLayout(false);
            this.tabPageInstances.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.panelInstancesTop.ResumeLayout(false);
            this.panelInstancesTop.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageDefs;
        private System.Windows.Forms.TabPage tabPageInstances;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.ListBox listBoxEntities;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label labelSelected;
        private System.Windows.Forms.Panel panelInstancesTop;
        private System.Windows.Forms.Label labelInstanceCount;
        private System.Windows.Forms.ListBox listBoxInstances;
    }
}
