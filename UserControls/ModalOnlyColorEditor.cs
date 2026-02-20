using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace csharp_editor.UserControls
{
    public class ModalOnlyColorEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                dlg.Color = (Color)value;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return dlg.Color;
                }
            }
            return value;
        }
    }
}
