using csharp_editor.Json;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace csharp_editor;

public partial class Editor : Form {

    public bool active;

    public int id = 0;

    public TreeNode rootNode;

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

        FormClosing += Form1_FormClosing;

        PreviewKeyDown += Form1_PreviewKeyDown;
        KeyDown += Form1_KeyDown;

        this.toolStripButton_openFile.MouseUp += toolStripButton_openFile_MouseUp;
        this.toolStripButton_cmd.MouseUp += ToolStripButton_cmd_MouseUp;
        this.toolStripButton_addEntity.MouseUp += ToolStripButton_addEntity;

        this.richTextBox_console.TextChanged += richTextBox_console_TextChanged;

        rootNode = new TreeNode("root");

        this.treeView1.Nodes.Add(rootNode);
        this.treeView1.AfterSelect += TreeView1_AfterSelect;
    }

    private void TreeView1_AfterSelect(object? sender, TreeViewEventArgs e) {

        TreeNode node = this.treeView1.SelectedNode;
        
        if (node == null) {

            return;
        }
        else {

            if (node.Tag == null) {

                panel_main.DeselectEntity();

                return;
            }
        }

        int id = (int) node.Tag;

        panel_main.SelectEntity(id);
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
    private void ToolStripButton_addEntity(object? sender, MouseEventArgs e) {

        TreeNode selectedNode = treeView1.SelectedNode;

        if (selectedNode == null) selectedNode = rootNode;

        TreeNode entity = new TreeNode("New Entity");
        entity.Tag = id;

        selectedNode.Nodes.Add(entity);

        panel_main.AddEntity(id);

        id++;
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

    public void PreRender() {

        panel_main.PreRender();
    }

    public void Render() {

        panel_main.Render();
    }

    public void PostRender() {

        panel_main.PostRender();
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