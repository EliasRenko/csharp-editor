using csharp_editor.Json;
using System.Diagnostics;
using System.Windows.Forms;

namespace csharp_editor;

public partial class Editor : Form {

    public bool active;

    public Editor(Renderer.CallbackDelegate callback) {

        InitializeComponent();

        active = true;

        toolStrip1.Renderer = new ToolStripRenderer();
        //toolStrip2.Renderer = new ToolStripRenderer();

        ImageList iconsList = new ImageList();
        //iconsList.ColorDepth = ColorDepth.Depth32Bit;
        iconsList.ImageSize = new Size(16, 16);
        iconsList.ColorDepth = ColorDepth.Depth32Bit;
        iconsList.Images.Add(Image.FromFile(Application.StartupPath + "\\Resources\\folder_brick.png"));
        iconsList.Images.Add(Image.FromFile(Application.StartupPath + "\\Resources\\folder_explorer.png"));

        tabPage_scene.ImageIndex = 0;
        tabPage_files.ImageIndex = 1;

        tabControl1.ImageList = iconsList;


        panel_main.Init(callback);

        Application.Idle += HandleApplicationIdle;
        FormClosing += Form1_FormClosing;

        PreviewKeyDown += Form1_PreviewKeyDown;
        KeyDown += Form1_KeyDown;

        this.toolStripButton_openFile.MouseUp += toolStripButton_openFile_MouseUp;
        this.toolStripButton_cmd.MouseUp += ToolStripButton_cmd_MouseUp;

        this.richTextBox_console.TextChanged += richTextBox_console_TextChanged;
    }

    private void toolStripButton_openFile_MouseUp(object? sender, MouseEventArgs e) {

        string path = Utils.OpenFile("");

        string ext = Path.GetExtension(path);

        switch (ext) {

            case ".json":

                ImportProject(Parser.GetProject(path));

                break;

            case ".png":

                ImportImage(path);

                break;

            default:

                throw new Exception("Invalid file name");
        }
    }

    private void ToolStripButton_cmd_MouseUp(object? sender, MouseEventArgs e) {

        Process p = new Process();
        p.StartInfo.FileName = "C:\\Windows\\system32\\cmd.exe";
        p.StartInfo.WorkingDirectory = @"C:\";
        p.StartInfo.Arguments = "node fileWithCommands.js";
        p.Start();
    }

    private void Form1_PreviewKeyDown(object? sender, PreviewKeyDownEventArgs e) {

        //e.Handled = true;

        if (e.KeyCode == Keys.Right) // do whatever you want with your user input
        {
            //MoveRight(); // for example
        }
    }

    private void Form1_KeyDown(object? sender, KeyEventArgs e) {

        e.Handled = true;

        //e.SuppressKeyPress = true; // cancel key press

        if (e.KeyCode == Keys.Right) // do whatever you want with your user input
        {
            //MoveRight(); // for example
        }
    }

    public void Render() {

        panel_main.Render();
    }

    public void Tick() {

        panel_main.Tick();
    }

    public void Log(String text) {

        richTextBox_console.AppendText("\n" + text);
    }

    private void ImportProject(ProjectJson json) {

        if (json.Path != null) {

            Log(json.Path);
        }
    }

    private void ImportImage(string path) {

        Log("Import image");

        TextureViewer textureViewer = new TextureViewer(path);

        textureViewer.ShowDialog();

        Renderer.LoadTexture(path, 0, "1");
    }

    private void HandleApplicationIdle(object? sender, EventArgs e) {

        //panel_main.Update();

        //panel_main.Draw();
    }

    private void LogCallback(string result) {

        Log(result);
    }

    private void Form1_FormClosing(object? sender, FormClosingEventArgs e) {

        panel_main.Release();

        active = false;
    }

    private void richTextBox_console_TextChanged(object sender, EventArgs e) {

        richTextBox_console.SelectionStart = richTextBox_console.Text.Length;

        richTextBox_console.ScrollToCaret();
    }
}