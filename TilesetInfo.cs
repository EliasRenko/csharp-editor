using System.Runtime.InteropServices;

namespace csharp_editor;

public class TilesetInfo
{
    public string Name { get; set; }
    public string TexturePath { get; set; }
    public int TileSize { get; set; }
    public int TilesPerRow { get; set; }
    public int TilesPerCol { get; set; }
    public int RegionCount { get; set; }

    public static TilesetInfo FromStruct(Externs.TilesetInfoStruct structData)
    {
        return new TilesetInfo
        {
            Name = Marshal.PtrToStringAnsi(structData.name),
            TexturePath = Marshal.PtrToStringAnsi(structData.texturePath),
            TileSize = structData.tileSize,
            TilesPerRow = structData.tilesPerRow,
            TilesPerCol = structData.tilesPerCol,
            RegionCount = structData.regionCount
        };
    }
}