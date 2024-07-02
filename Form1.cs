using System;
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