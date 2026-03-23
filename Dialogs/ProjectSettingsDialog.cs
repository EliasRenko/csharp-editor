using System;
using System.Drawing;
using System.Windows.Forms;
using csharp_editor.UserControls;

namespace csharp_editor.Dialogs {
    public class ProjectSettingsDialog : Form {
        private TextBox textBoxProjectFilePath;
        private TextBox textBoxProjectName;
        private NumericUpDown numericUpDownTileSizeX;
        private NumericUpDown numericUpDownTileSizeY;
        private Button buttonOK;
        private Button buttonCancel;

        public ProjectInfoStruct UpdatedProjectInfo { get; private set; }

        public ProjectSettingsDialog(ProjectInfoStruct current) {
            this.Text = "Project Settings";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(360, 200);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.AutoScaleMode = AutoScaleMode.Font;

            var labelFilePath = new Label { Text = "Project file:", Location = new Point(12, 10), AutoSize = true };
            textBoxProjectFilePath = new TextBox { Location = new Point(12, 30), Size = new Size(330, 22), ReadOnly = true, TabStop = false };

            var labelProjectName = new Label { Text = "Project name:", Location = new Point(12, 60), AutoSize = true };
            textBoxProjectName = new TextBox { Location = new Point(12, 80), Size = new Size(330, 22) };

            var labelDefaultTile = new Label { Text = "Default tile size (X x Y):", Location = new Point(12, 110), AutoSize = true };
            numericUpDownTileSizeX = new NumericUpDown { Location = new Point(12, 130), Size = new Size(80, 22), Minimum = 1, Maximum = 1024 };
            numericUpDownTileSizeY = new NumericUpDown { Location = new Point(100, 130), Size = new Size(80, 22), Minimum = 1, Maximum = 1024 };

            buttonOK = new Button { Text = "OK", Location = new Point(186, 165), Size = new Size(75, 25), DialogResult = DialogResult.OK };
            buttonCancel = new Button { Text = "Cancel", Location = new Point(267, 165), Size = new Size(75, 25), DialogResult = DialogResult.Cancel };

            buttonOK.Click += ButtonOK_Click;
            buttonCancel.Click += (s, e) => this.Close();

            this.Controls.Add(labelFilePath);
            this.Controls.Add(textBoxProjectFilePath);
            this.Controls.Add(labelProjectName);
            this.Controls.Add(textBoxProjectName);
            this.Controls.Add(labelDefaultTile);
            this.Controls.Add(numericUpDownTileSizeX);
            this.Controls.Add(numericUpDownTileSizeY);
            this.Controls.Add(buttonOK);
            this.Controls.Add(buttonCancel);

            textBoxProjectFilePath.Text = current.FilePath ?? "";
            textBoxProjectName.Text = current.ProjectName ?? "";
            numericUpDownTileSizeX.Value = current.DefaultTileSizeX > 0 ? current.DefaultTileSizeX : 32;
            numericUpDownTileSizeY.Value = current.DefaultTileSizeY > 0 ? current.DefaultTileSizeY : 32;

            UpdatedProjectInfo = current;
        }

        private void ButtonOK_Click(object? sender, EventArgs e) {
            string projectName = textBoxProjectName.Text.Trim();
            if (string.IsNullOrEmpty(projectName)) {
                MessageBox.Show(this, "Project name cannot be empty.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UpdatedProjectInfo = new ProjectInfoStruct {
                FilePath = textBoxProjectFilePath.Text,
                ProjectName = projectName,
                DefaultTileSizeX = (int)numericUpDownTileSizeX.Value,
                DefaultTileSizeY = (int)numericUpDownTileSizeY.Value
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}