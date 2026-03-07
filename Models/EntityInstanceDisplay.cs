using System.ComponentModel;

namespace csharp_editor.Models {
    /// <summary>
    /// Read-only view model shown in the PropertyGrid when one or more placed
    /// entity instances are selected in the C++ engine.
    /// </summary>
    public class EntityInstanceDisplay {
        [Category("Entity"), Description("Entity definition name")]
        public string DefName { get; init; } = "";

        [Category("Entity"), Description("World X position")]
        public int X { get; init; }

        [Category("Entity"), Description("World Y position")]
        public int Y { get; init; }

        [Category("Entity"), Description("Width in pixels")]
        public int Width { get; init; }

        [Category("Entity"), Description("Height in pixels")]
        public int Height { get; init; }
    }
}
