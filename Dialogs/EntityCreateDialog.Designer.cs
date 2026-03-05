namespace csharp_editor.Dialogs {
    partial class EntityCreateDialog {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent() {
            groupBasic          = new System.Windows.Forms.GroupBox();
            labelName           = new System.Windows.Forms.Label();
            textBoxName         = new System.Windows.Forms.TextBox();
            labelWidth          = new System.Windows.Forms.Label();
            numericUpDownWidth  = new System.Windows.Forms.NumericUpDown();
            labelHeight         = new System.Windows.Forms.Label();
            numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            groupAppearance     = new System.Windows.Forms.GroupBox();
            labelTilemap        = new System.Windows.Forms.Label();
            comboBoxTilemap     = new System.Windows.Forms.ComboBox();
            buttonSelectRegion  = new System.Windows.Forms.Button();
            labelRegionInfo     = new System.Windows.Forms.Label();
            groupBehaviour      = new System.Windows.Forms.GroupBox();
            labelClass          = new System.Windows.Forms.Label();
            comboBoxClass       = new System.Windows.Forms.ComboBox();
            labelPivot          = new System.Windows.Forms.Label();
            panelPivot          = new System.Windows.Forms.Panel();
            btnPivotTL          = new System.Windows.Forms.Button();
            btnPivotTC          = new System.Windows.Forms.Button();
            btnPivotTR          = new System.Windows.Forms.Button();
            btnPivotML          = new System.Windows.Forms.Button();
            btnPivotMC          = new System.Windows.Forms.Button();
            btnPivotMR          = new System.Windows.Forms.Button();
            btnPivotBL          = new System.Windows.Forms.Button();
            btnPivotBC          = new System.Windows.Forms.Button();
            btnPivotBR          = new System.Windows.Forms.Button();
            checkBoxHitbox      = new System.Windows.Forms.CheckBox();
            panelHitbox         = new System.Windows.Forms.Panel();
            labelHitboxX        = new System.Windows.Forms.Label();
            numHitboxX          = new System.Windows.Forms.NumericUpDown();
            labelHitboxY        = new System.Windows.Forms.Label();
            numHitboxY          = new System.Windows.Forms.NumericUpDown();
            labelHitboxW        = new System.Windows.Forms.Label();
            numHitboxW          = new System.Windows.Forms.NumericUpDown();
            labelHitboxH        = new System.Windows.Forms.Label();
            numHitboxH          = new System.Windows.Forms.NumericUpDown();
            groupProperties     = new System.Windows.Forms.GroupBox();
            listViewProperties  = new System.Windows.Forms.ListView();
            colPropName         = new System.Windows.Forms.ColumnHeader();
            colPropType         = new System.Windows.Forms.ColumnHeader();
            colPropDefault      = new System.Windows.Forms.ColumnHeader();
            buttonAddProperty   = new System.Windows.Forms.Button();
            buttonRemoveProperty = new System.Windows.Forms.Button();
            buttonCreate        = new System.Windows.Forms.Button();
            buttonCancel        = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHitboxX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHitboxY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHitboxW).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHitboxH).BeginInit();
            groupBasic.SuspendLayout();
            groupAppearance.SuspendLayout();
            groupBehaviour.SuspendLayout();
            panelPivot.SuspendLayout();
            panelHitbox.SuspendLayout();
            groupProperties.SuspendLayout();
            SuspendLayout();

            // β”€β”€ groupBasic
            groupBasic.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBasic.Controls.AddRange(new System.Windows.Forms.Control[] { labelName, textBoxName, labelWidth, numericUpDownWidth, labelHeight, numericUpDownHeight });
            groupBasic.Location = new System.Drawing.Point(12, 10);
            groupBasic.Name = "groupBasic";
            groupBasic.Size = new System.Drawing.Size(756, 75);
            groupBasic.TabStop = false;
            groupBasic.Text = "Basic";

            labelName.AutoSize = true;
            labelName.Location = new System.Drawing.Point(12, 22);
            labelName.Text = "Name:";

            textBoxName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxName.Location = new System.Drawing.Point(65, 18);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new System.Drawing.Size(679, 23);
            textBoxName.TabIndex = 0;

            labelWidth.AutoSize = true;
            labelWidth.Location = new System.Drawing.Point(12, 52);
            labelWidth.Text = "Width:";

            numericUpDownWidth.Location = new System.Drawing.Point(65, 48);
            numericUpDownWidth.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numericUpDownWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownWidth.Name = "numericUpDownWidth";
            numericUpDownWidth.Size = new System.Drawing.Size(80, 23);
            numericUpDownWidth.TabIndex = 1;
            numericUpDownWidth.Value = new decimal(new int[] { 32, 0, 0, 0 });

            labelHeight.AutoSize = true;
            labelHeight.Location = new System.Drawing.Point(162, 52);
            labelHeight.Text = "Height:";

            numericUpDownHeight.Location = new System.Drawing.Point(218, 48);
            numericUpDownHeight.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numericUpDownHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownHeight.Name = "numericUpDownHeight";
            numericUpDownHeight.Size = new System.Drawing.Size(80, 23);
            numericUpDownHeight.TabIndex = 2;
            numericUpDownHeight.Value = new decimal(new int[] { 32, 0, 0, 0 });

            // β”€β”€ groupAppearance
            groupAppearance.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupAppearance.Controls.AddRange(new System.Windows.Forms.Control[] { labelTilemap, comboBoxTilemap, buttonSelectRegion, labelRegionInfo });
            groupAppearance.Location = new System.Drawing.Point(12, 93);
            groupAppearance.Name = "groupAppearance";
            groupAppearance.Size = new System.Drawing.Size(756, 78);
            groupAppearance.TabStop = false;
            groupAppearance.Text = "Appearance";

            labelTilemap.AutoSize = true;
            labelTilemap.Location = new System.Drawing.Point(12, 22);
            labelTilemap.Text = "Tilemap:";

            comboBoxTilemap.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBoxTilemap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxTilemap.FormattingEnabled = true;
            comboBoxTilemap.Location = new System.Drawing.Point(70, 18);
            comboBoxTilemap.Name = "comboBoxTilemap";
            comboBoxTilemap.Size = new System.Drawing.Size(674, 23);
            comboBoxTilemap.TabIndex = 3;

            buttonSelectRegion.Location = new System.Drawing.Point(12, 47);
            buttonSelectRegion.Name = "buttonSelectRegion";
            buttonSelectRegion.Size = new System.Drawing.Size(90, 23);
            buttonSelectRegion.TabIndex = 4;
            buttonSelectRegion.Text = "Region...";
            buttonSelectRegion.UseVisualStyleBackColor = true;

            labelRegionInfo.AutoSize = true;
            labelRegionInfo.Location = new System.Drawing.Point(110, 51);
            labelRegionInfo.Name = "labelRegionInfo";
            labelRegionInfo.Text = "";

            // β”€β”€ groupBehaviour
            groupBehaviour.Controls.AddRange(new System.Windows.Forms.Control[] { labelClass, comboBoxClass, labelPivot, panelPivot, checkBoxHitbox, panelHitbox });
            groupBehaviour.Location = new System.Drawing.Point(12, 179);
            groupBehaviour.Name = "groupBehaviour";
            groupBehaviour.Size = new System.Drawing.Size(360, 175);
            groupBehaviour.TabStop = false;
            groupBehaviour.Text = "Behaviour";

            labelClass.AutoSize = true;
            labelClass.Location = new System.Drawing.Point(10, 26);
            labelClass.Text = "Class:";

            comboBoxClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxClass.FormattingEnabled = true;
            comboBoxClass.Location = new System.Drawing.Point(75, 22);
            comboBoxClass.Name = "comboBoxClass";
            comboBoxClass.Size = new System.Drawing.Size(270, 23);
            comboBoxClass.TabIndex = 5;

            labelPivot.AutoSize = true;
            labelPivot.Location = new System.Drawing.Point(10, 58);
            labelPivot.Text = "Pivot:";

            // β”€β”€ panelPivot (3x3 grid of 25x25 buttons)
            panelPivot.Controls.AddRange(new System.Windows.Forms.Control[] { btnPivotTL, btnPivotTC, btnPivotTR, btnPivotML, btnPivotMC, btnPivotMR, btnPivotBL, btnPivotBC, btnPivotBR });
            panelPivot.Location = new System.Drawing.Point(75, 50);
            panelPivot.Name = "panelPivot";
            panelPivot.Size = new System.Drawing.Size(75, 75);

            SetupPivotButton(btnPivotTL, 0, 0, 25, "TopLeft");
            SetupPivotButton(btnPivotTC, 1, 0, 25, "TopCenter");
            SetupPivotButton(btnPivotTR, 2, 0, 25, "TopRight");
            SetupPivotButton(btnPivotML, 0, 1, 25, "MiddleLeft");
            SetupPivotButton(btnPivotMC, 1, 1, 25, "MiddleCenter");
            SetupPivotButton(btnPivotMR, 2, 1, 25, "MiddleRight");
            SetupPivotButton(btnPivotBL, 0, 2, 25, "BottomLeft");
            SetupPivotButton(btnPivotBC, 1, 2, 25, "BottomCenter");
            SetupPivotButton(btnPivotBR, 2, 2, 25, "BottomRight");

            checkBoxHitbox.AutoSize = true;
            checkBoxHitbox.Location = new System.Drawing.Point(10, 134);
            checkBoxHitbox.Name = "checkBoxHitbox";
            checkBoxHitbox.TabIndex = 6;
            checkBoxHitbox.Text = "Enable Hitbox";

            // β”€β”€ panelHitbox
            panelHitbox.Controls.AddRange(new System.Windows.Forms.Control[] { labelHitboxX, numHitboxX, labelHitboxY, numHitboxY, labelHitboxW, numHitboxW, labelHitboxH, numHitboxH });
            panelHitbox.Enabled = false;
            panelHitbox.Location = new System.Drawing.Point(0, 154);
            panelHitbox.Name = "panelHitbox";
            panelHitbox.Size = new System.Drawing.Size(354, 26);

            labelHitboxX.AutoSize = true;
            labelHitboxX.Location = new System.Drawing.Point(5, 5);
            labelHitboxX.Text = "X:";

            numHitboxX.Location = new System.Drawing.Point(20, 2);
            numHitboxX.Minimum = new decimal(new int[] { 2048, 0, 0, -2147483648 });
            numHitboxX.Maximum = new decimal(new int[] { 2048, 0, 0, 0 });
            numHitboxX.Name = "numHitboxX";
            numHitboxX.Size = new System.Drawing.Size(60, 23);
            numHitboxX.TabIndex = 7;

            labelHitboxY.AutoSize = true;
            labelHitboxY.Location = new System.Drawing.Point(88, 5);
            labelHitboxY.Text = "Y:";

            numHitboxY.Location = new System.Drawing.Point(103, 2);
            numHitboxY.Minimum = new decimal(new int[] { 2048, 0, 0, -2147483648 });
            numHitboxY.Maximum = new decimal(new int[] { 2048, 0, 0, 0 });
            numHitboxY.Name = "numHitboxY";
            numHitboxY.Size = new System.Drawing.Size(60, 23);
            numHitboxY.TabIndex = 8;

            labelHitboxW.AutoSize = true;
            labelHitboxW.Location = new System.Drawing.Point(171, 5);
            labelHitboxW.Text = "W:";

            numHitboxW.Location = new System.Drawing.Point(186, 2);
            numHitboxW.Maximum = new decimal(new int[] { 2048, 0, 0, 0 });
            numHitboxW.Name = "numHitboxW";
            numHitboxW.Size = new System.Drawing.Size(60, 23);
            numHitboxW.TabIndex = 9;
            numHitboxW.Value = new decimal(new int[] { 32, 0, 0, 0 });

            labelHitboxH.AutoSize = true;
            labelHitboxH.Location = new System.Drawing.Point(254, 5);
            labelHitboxH.Text = "H:";

            numHitboxH.Location = new System.Drawing.Point(269, 2);
            numHitboxH.Maximum = new decimal(new int[] { 2048, 0, 0, 0 });
            numHitboxH.Name = "numHitboxH";
            numHitboxH.Size = new System.Drawing.Size(60, 23);
            numHitboxH.TabIndex = 10;
            numHitboxH.Value = new decimal(new int[] { 32, 0, 0, 0 });

            // β”€β”€ groupProperties
            groupProperties.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupProperties.Controls.AddRange(new System.Windows.Forms.Control[] { listViewProperties, buttonAddProperty, buttonRemoveProperty });
            groupProperties.Location = new System.Drawing.Point(380, 179);
            groupProperties.Name = "groupProperties";
            groupProperties.Size = new System.Drawing.Size(388, 175);
            groupProperties.TabStop = false;
            groupProperties.Text = "Custom Properties";

            listViewProperties.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            listViewProperties.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { colPropName, colPropType, colPropDefault });
            listViewProperties.FullRowSelect = true;
            listViewProperties.GridLines = true;
            listViewProperties.Location = new System.Drawing.Point(10, 22);
            listViewProperties.Name = "listViewProperties";
            listViewProperties.Size = new System.Drawing.Size(368, 113);
            listViewProperties.TabIndex = 11;
            listViewProperties.UseCompatibleStateImageBehavior = false;
            listViewProperties.View = System.Windows.Forms.View.Details;

            colPropName.Text = "Name";
            colPropName.Width = 130;
            colPropType.Text = "Type";
            colPropType.Width = 80;
            colPropDefault.Text = "Default";
            colPropDefault.Width = 130;

            buttonAddProperty.Location = new System.Drawing.Point(10, 142);
            buttonAddProperty.Name = "buttonAddProperty";
            buttonAddProperty.Size = new System.Drawing.Size(70, 24);
            buttonAddProperty.TabIndex = 12;
            buttonAddProperty.Text = "+ Add";
            buttonAddProperty.UseVisualStyleBackColor = true;
            buttonAddProperty.Click += buttonAddProperty_Click;

            buttonRemoveProperty.Enabled = false;
            buttonRemoveProperty.Location = new System.Drawing.Point(88, 142);
            buttonRemoveProperty.Name = "buttonRemoveProperty";
            buttonRemoveProperty.Size = new System.Drawing.Size(70, 24);
            buttonRemoveProperty.TabIndex = 13;
            buttonRemoveProperty.Text = "Remove";
            buttonRemoveProperty.UseVisualStyleBackColor = true;
            buttonRemoveProperty.Click += buttonRemoveProperty_Click;

            // β”€β”€ Dialog buttons
            buttonCreate.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonCreate.Location = new System.Drawing.Point(688, 366);
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new System.Drawing.Size(80, 27);
            buttonCreate.TabIndex = 14;
            buttonCreate.Text = "Create";
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.Click += buttonCreate_Click;

            buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Location = new System.Drawing.Point(600, 366);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(80, 27);
            buttonCancel.TabIndex = 15;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;

            // β”€β”€ Form
            AcceptButton = buttonCreate;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new System.Drawing.Size(780, 406);
            Controls.AddRange(new System.Windows.Forms.Control[] { groupBasic, groupAppearance, groupBehaviour, groupProperties, buttonCreate, buttonCancel });
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            MaximizeBox = false;
            MinimumSize = new System.Drawing.Size(780, 440);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "New Entity";

            groupBasic.ResumeLayout(false);
            groupBasic.PerformLayout();
            groupAppearance.ResumeLayout(false);
            groupAppearance.PerformLayout();
            groupBehaviour.ResumeLayout(false);
            groupBehaviour.PerformLayout();
            panelPivot.ResumeLayout(false);
            panelHitbox.ResumeLayout(false);
            panelHitbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHitboxX).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHitboxY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHitboxW).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHitboxH).EndInit();
            groupProperties.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private void SetupPivotButton(System.Windows.Forms.Button btn, int col, int row, int size, string tag) {
            btn.Location  = new System.Drawing.Point(col * size, row * size);
            btn.Size      = new System.Drawing.Size(size, size);
            btn.Tag       = tag;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.TabStop   = false;
            btn.Text      = "";
            btn.Click    += PivotButton_Click;
        }
        #endregion

        // β”€β”€ Basic
        private System.Windows.Forms.GroupBox groupBasic;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;

        // β”€β”€ Appearance
        private System.Windows.Forms.GroupBox groupAppearance;
        private System.Windows.Forms.Label labelTilemap;
        private System.Windows.Forms.ComboBox comboBoxTilemap;
        private System.Windows.Forms.Button buttonSelectRegion;
        private System.Windows.Forms.Label labelRegionInfo;

        // β”€β”€ Behaviour
        private System.Windows.Forms.GroupBox groupBehaviour;
        private System.Windows.Forms.Label labelClass;
        private System.Windows.Forms.ComboBox comboBoxClass;
        private System.Windows.Forms.Label labelPivot;
        private System.Windows.Forms.Panel panelPivot;
        private System.Windows.Forms.Button btnPivotTL, btnPivotTC, btnPivotTR;
        private System.Windows.Forms.Button btnPivotML, btnPivotMC, btnPivotMR;
        private System.Windows.Forms.Button btnPivotBL, btnPivotBC, btnPivotBR;
        private System.Windows.Forms.CheckBox checkBoxHitbox;
        private System.Windows.Forms.Panel panelHitbox;
        private System.Windows.Forms.Label labelHitboxX, labelHitboxY, labelHitboxW, labelHitboxH;
        private System.Windows.Forms.NumericUpDown numHitboxX, numHitboxY, numHitboxW, numHitboxH;

        // β”€β”€ Custom Properties
        private System.Windows.Forms.GroupBox groupProperties;
        private System.Windows.Forms.ListView listViewProperties;
        private System.Windows.Forms.ColumnHeader colPropName, colPropType, colPropDefault;
        private System.Windows.Forms.Button buttonAddProperty;
        private System.Windows.Forms.Button buttonRemoveProperty;

        // β”€β”€ Dialog buttons
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonCancel;
    }
}
