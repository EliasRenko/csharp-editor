using System.Runtime.Versioning;
using System.Windows.Forms;

namespace NativeHaxeRuntime.UserControls;

[SupportedOSPlatform("windows")]
public partial class LogView : UserControl {
    private Panel _panelBorder = null!;
    private RichTextBox _richTextBoxLog = null!;

    public LogView() {
        InitializeComponent();
        _richTextBoxLog.AppendText("Startup...");
    }

    public string? CopyText() {
        return _richTextBoxLog.SelectedText;
    }

    public void Log(string text) => Log(text, System.Drawing.Color.Empty);

    public void Log(string text, System.Drawing.Color color) {
        _richTextBoxLog.SelectionStart  = _richTextBoxLog.TextLength;
        _richTextBoxLog.SelectionLength = 0;
        _richTextBoxLog.SelectionColor  = color == System.Drawing.Color.Empty ? _richTextBoxLog.ForeColor: color;
        _richTextBoxLog.AppendText(Environment.NewLine + text);
        _richTextBoxLog.SelectionColor = _richTextBoxLog.ForeColor;
        _richTextBoxLog.ScrollToCaret();
    }

    public void Clear() {
        _richTextBoxLog.Clear();
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        _panelBorder = new System.Windows.Forms.Panel();
        _richTextBoxLog = new System.Windows.Forms.RichTextBox();
        _panelBorder.SuspendLayout();
        SuspendLayout();
        // 
        // panel_border
        // 
        _panelBorder.BackColor = System.Drawing.Color.Gray;
        _panelBorder.Controls.Add(_richTextBoxLog);
        _panelBorder.Dock = System.Windows.Forms.DockStyle.Fill;
        _panelBorder.Location = new System.Drawing.Point(0, 0);
        _panelBorder.Name = "_panelBorder";
        _panelBorder.Padding = new System.Windows.Forms.Padding(1);
        _panelBorder.Size = new System.Drawing.Size(300, 200);
        _panelBorder.TabIndex = 0;
        // 
        // richTextBox_log
        // 
        _richTextBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
        _richTextBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
        _richTextBoxLog.Location = new System.Drawing.Point(1, 1);
        _richTextBoxLog.Name = "_richTextBoxLog";
        _richTextBoxLog.ReadOnly = true;
        _richTextBoxLog.Size = new System.Drawing.Size(298, 198);
        _richTextBoxLog.TabIndex = 0;
        _richTextBoxLog.Text = "";
        // 
        // LogView
        // 
        Controls.Add(_panelBorder);
        Size = new System.Drawing.Size(300, 200);
        _panelBorder.ResumeLayout(false);
        ResumeLayout(false);
    }
}

