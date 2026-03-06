namespace csharp_editor {
    public readonly struct ToolType {
        private readonly int _value;
        private ToolType(int v) => _value = v;

        public static readonly ToolType TileDraw     = new(0);
        public static readonly ToolType TileErase    = new(1);
        public static readonly ToolType EntityAdd    = new(2);
        public static readonly ToolType EntitySelect = new(3);

        public static implicit operator int(ToolType t) => t._value;
        public static implicit operator ToolType(int v) => new(v);
    }
}
