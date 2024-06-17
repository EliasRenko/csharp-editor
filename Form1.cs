using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace csharp_editor;

public partial class Form1 : Form
{
    //private string result;

    public Form1()
    {
        InitializeComponent();

        this.richTextBox_console.AppendText("Components initialized!");

        //Renderer.Init();
        Renderer.InitWithCallback(MyCallback);

        IntPtr sdlHandle = Renderer.GetHandle();

        // ** Get the parent window handle.
        IntPtr windowHandle = this.panel_main.Handle;

        Renderer.SetWindowPos(
            sdlHandle,
            Handle,
            0,
            0,
            0,
            0,
            0x0401 // NOSIZE | SHOWWINDOW
        );

        // Attach the SDL2 window to the panel
        Renderer.SetParent(sdlHandle, windowHandle);
        Renderer.ShowWindow(sdlHandle, 1); // SHOWNORMAL

        //int res = Add(9, 8, "res", MyCallback);
        //Console.WriteLine("the result of Add function: " + res);

        //SetLogDispacher(MyCallback);

        Application.Idle += HandleApplicationIdle;
    }

    private void HandleApplicationIdle(object? sender, EventArgs e)
    {
        Renderer.Draw();
    }

    public void MyCallback(string result)
    {
        //Console.WriteLine("Callback invoked from C++ with result: " + result);
        this.richTextBox_console.AppendText("\n" + result);
    }
    //public void RunAdd()
    //{
    //    int res = Add(9, 8, "res", MyCallback);
    //    Console.WriteLine("the result of Add function: " + res);
    //}
}