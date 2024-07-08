using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csharp_editor
{
    public partial class TextureViewer : Form
    {
        public string path;

        public TextureViewer(string path) {

            InitializeComponent();

            if (path != null)
            {
                pictureBox1.Image = Image.FromFile(path);
                label_path.Text += path;
            }
        }
    }
}
