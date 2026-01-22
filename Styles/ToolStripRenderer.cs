using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class ToolStripRenderer : ToolStripProfessionalRenderer {
    public ToolStripRenderer() {

        this.RoundedEdges = false;
    }

    protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e) {
        
        LinearGradientMode mode = LinearGradientMode.Horizontal;

        using (LinearGradientBrush b = new LinearGradientBrush(e.AffectedBounds, ColorTable.MenuStripGradientBegin, ColorTable.MenuStripGradientEnd, mode)) {

            e.Graphics.FillRectangle(b, e.AffectedBounds);
        }
    }
}