using System.ComponentModel;

namespace csharp_editor.Models {
    public class EntityInstanceDisplay {
        [Category("Entity"), Description("Unique instance ID"), ReadOnly(true)]
        public string Uid { get; init; } = "";

        [Category("Entity"), Description("Entity definition name"), ReadOnly(true)]
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
