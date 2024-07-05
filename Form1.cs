using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace csharp_editor;

public partial class Form1 : Form {
    //private string result;

    public Form1(Renderer.CallbackDelegate callback) {

        InitializeComponent();

        panel_main.Init(callback);

        Application.Idle += HandleApplicationIdle;
        FormClosing += Form1_FormClosing;

        PreviewKeyDown += Form1_PreviewKeyDown;
        KeyDown += Form1_KeyDown;

        this.toolStripButton_cmd.MouseUp += ToolStripButton_cmd_MouseUp;
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

    private void HandleApplicationIdle(object? sender, EventArgs e) {

        //panel_main.Update();

        //panel_main.Draw();
    }

    private void LogCallback(string result) {

        Log(result);
    }

    private void Form1_FormClosing(object? sender, FormClosingEventArgs e) {

        panel_main.Release();
    }
}