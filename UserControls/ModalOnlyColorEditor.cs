using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using csharp_editor.Dialogs;

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
            var ownerControl = (provider?.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService) != null
                ? Application.OpenForms.Count > 0 ? Application.OpenForms[0] : null
                : null;

            using var dlg = new ColorPickerDialog(value is Color c ? c : Color.White);
            if (dlg.ShowDialog(ownerControl) == DialogResult.OK)
            {
                return dlg.SelectedColor;
            }
            return value;
        }
    }
}
