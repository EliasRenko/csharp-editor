using csharp_editor.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace csharp_editor
{
    public partial class Editor : Form
    {

        public bool active;
        public int id = 0;

        public ProjectJson project;
        public TreeNode rootNode;

        public Editor()
        {
            InitializeComponent();
            this.KeyPreview = true;

            active = true;

            toolStrip1.Renderer = new ToolStripRenderer();
            //toolStrip2.Renderer = new ToolStripRenderer();

            ImageList iconsList = new ImageList();
            iconsList.ImageSize = new Size(16, 16);
            iconsList.ColorDepth = ColorDepth.Depth32Bit;
            iconsList.Images.Add(Image.FromFile(Application.StartupPath + "\\Resources\\folder_brick.png"));
            iconsList.Images.Add(Image.FromFile(Application.StartupPath + "\\Resources\\displays.png"));
            iconsList.Images.Add(Image.FromFile(Application.StartupPath + "\\Resources\\folder_explorer.png"));

            tabPage_scene.ImageIndex = 0;
            tabPage_display.ImageIndex = 1;
            tabPage_files.ImageIndex = 2;

            tabControl1.ImageList = iconsList;

            //

            Renderer.CallbackDelegate callback = (value) =>
            {
                Log(value);
            };

            // Init main panel (renderer as well)
            panel_main.Init(callback);

            // Events
            this.FormClosing += Form1_FormClosing;
            this.PreviewKeyDown += Form1_PreviewKeyDown;
            this.KeyDown += Form1_KeyDown;
            this.toolStripButton_openFile.MouseUp += toolStripButton_openFile_MouseUp;
            this.toolStripButton_cmd.MouseUp += ToolStripButton_cmd_MouseUp;
            this.toolStripButton_addEntity.MouseUp += ToolStripButton_addEntity;

            this.toolStripButton_addDisplayObject.MouseUp += ToolStripButton_addDisplayObject;

            this.richTextBox_console.TextChanged += richTextBox_console_TextChanged;

            this.tmi_exportBlueprint.MouseDown += Tmi_exportBlueprint_MouseDown;

            rootNode = new TreeNode("root");
            rootNode.Tag = new MapObject();
            this.treeView1.Nodes.Add(rootNode);
            this.treeView1.AfterSelect += TreeView1_AfterSelect;
        }

        private void ToolStripButton_addDisplayObject(object sender, MouseEventArgs e)
        {
            
        }

        private void Tmi_exportBlueprint_MouseDown(object sender, MouseEventArgs e)
        {
            ProjectJson project = new ProjectJson();
            project.Path = "C:/Users/Renko/Documents";

            ExportProject("", project);
        }   

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            TreeNode node = this.treeView1.SelectedNode;

            if (node == null)
            {

                return;
            }
            else
            {

                if (node.Text == "root")
                {

                    MapObject mapObject = node.Tag as MapObject;

                    if (mapObject == null) throw new Exception("Invalid map object");

                    propertyGrid_main.SelectedObject = mapObject;

                    mapObject.callback = changeMapProperties;

                    panel_main.DeselectEntity();

                    return;
                }

                if (node.Tag == null)
                {

                    panel_main.DeselectEntity();

                    return;
                }
            }

            Entity entity = node.Tag as Entity;

            if (entity == null) throw new Exception("Invalid or null entity object");

            entity.callback = changeEntityProperties;

            propertyGrid_main.SelectedObject = entity;

            panel_main.SelectEntity(entity.id);
        }

        private void changeEntityProperties(Entity entity)
        {
            panel_main.UpdateEntity(entity.id, entity.X, entity.Y);
        }

        private void changeMapProperties(MapObject map)
        {
            string hexValue = Utils.ToHex(map.color);

            //int intColor = (map.color.R << 16) | (map.color.G << 8) | (map.color.B);

            //int value = int.Parse(map.color., System.Globalization.NumberStyles.HexNumber);

            panel_main.UpdateMap(hexValue);
        }

        private void toolStripButton_openFile_MouseUp(object sender, MouseEventArgs e)
        {
            string path = Utils.OpenFile("");
            string ext = Path.GetExtension(path);

            switch (ext)
            {

                case ".json":

                    ImportProject(path);

                    break;

                case ".png":

                    ImportImage(path);

                    break;

                case "":

                    return;

                default:

                    throw new Exception("Invalid file name");
            }
        }

        private void ToolStripButton_cmd_MouseUp(object sender, MouseEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "C:\\Windows\\system32\\cmd.exe";
            p.StartInfo.WorkingDirectory = @"C:\";
            p.StartInfo.Arguments = "node fileWithCommands.js";
            p.Start();
        }
        private void ToolStripButton_addEntity(object sender, MouseEventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;

            if (selectedNode == null) selectedNode = rootNode;

            Entity entity = new Entity(id);

            TreeNode nodeEntity = new TreeNode("New Entity");

            nodeEntity.Tag = entity;

            selectedNode.Nodes.Add(nodeEntity);

            panel_main.AddEntity(id);

            id++;
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //e.Handled = true;

            if (e.KeyCode == Keys.Right) // do whatever you want with your user input
            {
                //MoveRight(); // for example
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;

            //e.SuppressKeyPress = true; // cancel key press

            if (e.KeyCode == Keys.Right) // do whatever you want with your user input
            {
                //MoveRight(); // for example
            }
        }

        #region rendering

        public void PreRender()
        {
            //panel_main.PreRender();
        }

        public void Render()
        {
            panel_main.Render();
        }

        public void PostRender()
        {
            //panel_main.PostRender();
        }

        #endregion

        #region Update

        public void Tick()
        {
            panel_main.Tick();
        }

        #endregion

        #region Log
        public void Log(String text)
        {
            richTextBox_console.AppendText("\n" + text);
        }

        private void LogCallback(string result)
        {
            Log(result);
        }

        #endregion

        #region Project

        private void ImportProject(string path)
        {
            string jsonString = File.ReadAllText(path);
            project = JsonSerializer.Deserialize<ProjectJson>(jsonString)!;


            //if (json.Path != null)
            //{
            //    Log(json.Path);
            //}
        }

        private void ExportProject(string path, ProjectJson project)
        {
            string data = JsonSerializer.Serialize<ProjectJson>(project);

            Utils.SaveFile("", data, "blueprint", "json");
        }

        private void ImportImage(string path)
        {

            Log("Import image");

            TextureViewer textureViewer = new TextureViewer(path);

            textureViewer.ShowDialog();

            Renderer.LoadTexture(path, 0, "1");
        }

        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            panel_main.Release();
            active = false;
        }

        private void richTextBox_console_TextChanged(object sender, EventArgs e)
        {
            richTextBox_console.SelectionStart = richTextBox_console.Text.Length;
            richTextBox_console.ScrollToCaret();
        }

        private void Editor_KeyDown(object sender, KeyEventArgs e)
        {
            Log(e.KeyCode.ToString());
        }
    }
}