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

    //const Int64 WS_THICKFRAME = 0x00040000L; //The window has a sizing border. Same as the WS_SIZEBOX style.
    //const Int64 WS_CHILD = 0x40000000L; // The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style.
    //const Int64 WS_EX_NOACTIVATE = 0x08000000L;
    //const Int64 WS_EX_TOOLWINDOW = 0x00000080L;

    //protected override CreateParams CreateParams {
    //    get {
    //        CreateParams ret = base.CreateParams;
    //        //ret.Style = (int)WS_THICKFRAME |
    //        //   (int)WS_CHILD;
    //        ret.ExStyle |= (int)WS_EX_NOACTIVATE |
    //           (int)WS_EX_TOOLWINDOW;
    //        return ret;
    //    }
    //}
}