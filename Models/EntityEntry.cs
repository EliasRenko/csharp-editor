namespace csharp_editor.Models {
    /// <summary>
    /// Data model for an entity definition loaded from the C++ engine.
    /// </summary>
    public class EntityEntry {
        public string Name        { get; set; } = "";
        public int    Width       { get; set; } = 32;
        public int    Height      { get; set; } = 32;
        public string TilemapName { get; set; } = "";
        public int    TileX       { get; set; } = 0;
        public int    TileY       { get; set; } = 0;
        public int    TileWidth   { get; set; } = 1;
        public int    TileHeight  { get; set; } = 1;
        public float  PivotX      { get; set; } = 0.5f;
        public float  PivotY      { get; set; } = 1.0f;
        public string PivotName   { get; set; } = "BottomCenter";

        public override string ToString() {
            string tilemapPart = string.IsNullOrEmpty(TilemapName) ? "" : $" [{TilemapName}]";
            return $"{Name} ({Width}x{Height}px){tilemapPart}";
        }
    }
}
