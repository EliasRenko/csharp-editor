using System;
using System.Runtime.InteropServices;
using csharp_editor.Models;
using NativeHaxeRuntime;

    public static class CExternsEditor {
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void EntitySelectionChangedCallback();

        // Structs
        
        [StructLayout(LayoutKind.Sequential)]
        public struct TextureDataStruct {
            public IntPtr Data; // unsigned char*
            public int Width;
            public int Height;
            public int BytesPerPixel;
            public int DataLength;
            public int Transparent;
        }
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct TilesetInfoStruct {
            public IntPtr name;              // Tileset name (use Marshal.PtrToStringAnsi to read)
            public IntPtr texturePath;       // Resource path to texture (use Marshal.PtrToStringAnsi to read)
        }
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct LayerInfoStruct {
            public IntPtr name;              // Layer name (use Marshal.PtrToStringAnsi to read)
            public int type;                 // Layer type (0 = TileLayer, 1 = EntityLayer)
            public IntPtr tilesetName;       // Tileset name for TileLayers (use Marshal.PtrToStringAnsi to read)
            public int tileSize;             // Tile size in pixels (TilemapLayer only, 0 for others)
            public int visible;              // Visibility flag (0 = hidden, 1 = visible)
            public bool silhouette;
            public int silhouetteColor;      // RGBA hex color for silhouette
        }
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct EntityDataStruct {
            public IntPtr name;              // Entity name (use Marshal.PtrToStringAnsi to read)
            public int width;                // Entity width in pixels
            public int height;               // Entity height in pixels
            public IntPtr tilesetName;       // Tileset name (use Marshal.PtrToStringAnsi to read)
            public int regionX;              // Region X in tiles
            public int regionY;              // Region Y in tiles
            public int regionWidth;          // Region width in tiles
            public int regionHeight;         // Region height in tiles
            public float pivotX;
            public float pivotY;
        }
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct EntityStruct {
            public IntPtr uid;               // Unique instance ID (use Marshal.PtrToStringAnsi to read)
            public IntPtr name;              // Entity definition name (use Marshal.PtrToStringAnsi to read)
            public int width;                // Width in pixels
            public int height;               // Height in pixels
            public int x;                    // World X position
            public int y;                    // World Y position
        }
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct MapProps {
            public IntPtr idd;               // id string
            public IntPtr name;              // name string
            public int worldx;
            public int worldy;
            public int width;
            public int height;
            public int tileSizeX;
            public int tileSizeY;
            public int bgColor;              // rgba hex
            public int gridColor;            // rgba hex
            public IntPtr projectFilePath;   // null / empty if map is not linked to a project
            public IntPtr projectName;       // null / empty if map is not linked to a project
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct ProjectProps {
            public IntPtr filePath;          // Project file path
            public IntPtr projectName;       // Project name
            public int defaultTileSizeX;
            public int defaultTileSizeY;
        }

        #region Project management

        [DllImport(CExterns.DLL, EntryPoint = "exportProject", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool ExportProject(string filePath, string projectName);

        [DllImport(CExterns.DLL, EntryPoint = "importProject", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool ImportProject(string filePath);

        [DllImport(CExterns.DLL, EntryPoint = "getProjectProps", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool _GetProjectProps(out ProjectProps outProps);
        
        public static bool GetProjectProps(out ProjectInfoStruct outInfo) {
            bool result = _GetProjectProps(out ProjectProps temp);
            if (result) {
                outInfo = new ProjectInfoStruct {
                    FilePath = Marshal.PtrToStringAnsi(temp.filePath),
                    ProjectName = Marshal.PtrToStringAnsi(temp.projectName),
                    DefaultTileSizeX = temp.defaultTileSizeX,
                    DefaultTileSizeY = temp.defaultTileSizeY
                };
            } else {
                outInfo = default;
            }
            return result;
        }

        [DllImport(CExterns.DLL, EntryPoint = "editProject", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool _EditProject(ref ProjectProps inProps);

        public static bool EditProject(ProjectInfoStruct info) {
            IntPtr filePathPtr = Marshal.StringToHGlobalAnsi(info.FilePath ?? "");
            IntPtr projectNamePtr = Marshal.StringToHGlobalAnsi(info.ProjectName ?? "");
            try {
                var native = new ProjectProps {
                    filePath = filePathPtr,
                    projectName = projectNamePtr,
                    defaultTileSizeX = info.DefaultTileSizeX,
                    defaultTileSizeY = info.DefaultTileSizeY
                };

                return CExternsEditor._EditProject(ref native);
            } finally {
                Marshal.FreeHGlobal(filePathPtr);
                Marshal.FreeHGlobal(projectNamePtr);
            }
        }
        
        #endregion

        #region Map management

        [DllImport(CExterns.DLL, EntryPoint = "exportMap", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool ExportMap(string path);
        
        [DllImport(CExterns.DLL, EntryPoint = "importMap", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int ImportMap(string path);
        
        [DllImport(CExterns.DLL, EntryPoint = "getMapProps", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool _GetMapProps(out MapProps outInfo);
        
        public static bool GetMapProps(out MapInfoStruct outInfo) {
            MapProps temp;
            bool success = _GetMapProps(out temp);

            if (!success) {
                outInfo = default;
                return false;
            }

            outInfo = new MapInfoStruct {
                idd = Marshal.PtrToStringAnsi(temp.idd),
                name = Marshal.PtrToStringAnsi(temp.name),
                worldx = temp.worldx,
                worldy = temp.worldy,
                width = temp.width,
                height = temp.height,
                tileSizeX = temp.tileSizeX,
                tileSizeY = temp.tileSizeY,
                bgColor = temp.bgColor,
                gridColor = temp.gridColor,
                projectFilePath = Marshal.PtrToStringAnsi(temp.projectFilePath),
                projectName = Marshal.PtrToStringAnsi(temp.projectName)
            };

            return true;
        }
        
        [DllImport(CExterns.DLL, EntryPoint = "setMapProps", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool _SetMapProps(ref MapProps info);
        
        public static bool SetMapProps(MapInfoStruct info) {
            IntPtr projectFilePathPtr = Marshal.StringToHGlobalAnsi(info.projectFilePath ?? "");
            IntPtr projectNamePtr = Marshal.StringToHGlobalAnsi(info.projectName ?? "");

            MapProps temp = new CExternsEditor.MapProps {
                idd = Marshal.StringToHGlobalAnsi(info.idd ?? ""),
                name = Marshal.StringToHGlobalAnsi(info.name ?? ""),
                worldx = info.worldx,
                worldy = info.worldy,
                width = info.width,
                height = info.height,
                tileSizeX = info.tileSizeX,
                tileSizeY = info.tileSizeY,
                bgColor = info.bgColor,
                gridColor = info.gridColor,
                projectFilePath = projectFilePathPtr,
                projectName = projectNamePtr
            };

            try {
                return _SetMapProps(ref temp);
            } finally {
                Marshal.FreeHGlobal(temp.idd);
                Marshal.FreeHGlobal(temp.name);
                Marshal.FreeHGlobal(temp.projectFilePath);
                Marshal.FreeHGlobal(temp.projectName);
            }
        }

        #endregion

        #region Textures

        [DllImport(CExterns.DLL, EntryPoint = "getTextureData", CharSet = CharSet.Ansi)]
        public static extern void GetTextureData(string path, out TextureDataStruct outData);
        
        #endregion

        #region Tileset managment
        
        [DllImport(CExterns.DLL, EntryPoint = "createTileset", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool CreateTileset(string texturePath, string name);
        
        [DllImport(CExterns.DLL, EntryPoint = "deleteTileset", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool DeleteTileset(string name);
        
        [DllImport(CExterns.DLL, EntryPoint = "getTileset", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetTileset(string tilesetName, out TilesetInfoStruct outInfo);
        
        [DllImport(CExterns.DLL, EntryPoint = "getTilesetAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetTilesetAt(int index, out TilesetInfoStruct outInfo);
        
        [DllImport(CExterns.DLL, EntryPoint = "getTilesetCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTilesetCount();
        
        [DllImport(CExterns.DLL, EntryPoint = "setActiveTileset", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool SetActiveTileset(string tilesetName);

        [DllImport(CExterns.DLL, EntryPoint = "getActiveTile")]
        public static extern int GetActiveTile();
        
        [DllImport(CExterns.DLL, EntryPoint = "setActiveTile")]
        public static extern void SetActiveTile(int tileRegionId);

        #endregion

        #region Entity definitions managment

        [DllImport(CExterns.DLL, EntryPoint = "createEntityDef", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool CreateEntity(string entityName, ref EntityDataStruct data);
        
        [DllImport(CExterns.DLL, EntryPoint = "editEntityDef", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool EditEntity(string entityName, ref EntityDataStruct data);
        
        [DllImport(CExterns.DLL, EntryPoint = "deleteEntityDef", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool DeleteEntityDef(string entityName);
        
        [DllImport(CExterns.DLL, EntryPoint = "getEntityDef", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool GetEntity(string entityName, out EntityDataStruct outData);
        
        [DllImport(CExterns.DLL, EntryPoint = "getEntityDefAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetEntityAt(int index, out EntityDataStruct outData);
        
        [DllImport(CExterns.DLL, EntryPoint = "getEntityDefCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEntityCount();
        
        [DllImport(CExterns.DLL, EntryPoint = "setActiveEntityDef", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool SetActiveEntity(string entityName);

        #endregion

        #region Entity instances management

        [DllImport(CExterns.DLL, EntryPoint = "setEntitySelectionChangedCallback", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetEntitySelectionChangedCallback(EntitySelectionChangedCallback callback);

        [DllImport(CExterns.DLL, EntryPoint = "getEntitySelectionCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEntitySelectionCount();

        [DllImport(CExterns.DLL, EntryPoint = "getEntitySelectionInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetEntitySelectionInfo(int index, out EntityStruct outData);
        
        [DllImport(CExterns.DLL, EntryPoint = "selectEntityByUID", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool SelectEntityByUID(string uid);

        [DllImport(CExterns.DLL, EntryPoint = "selectEntityInLayerByUID", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool SelectEntityInLayerByUID(string layerName, string uid);

        [DllImport(CExterns.DLL, EntryPoint = "deselectEntity", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeselectEntity();

        #endregion

        #region  Layer managment
        
        [DllImport(CExterns.DLL, EntryPoint = "createTilemapLayer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CreateTilemapLayer(string layerName, string tilesetName, int tileSize, int index);
        
        [DllImport(CExterns.DLL, EntryPoint = "createEntityLayer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CreateEntityLayer(string layerName);
        
        [DllImport(CExterns.DLL, EntryPoint = "createFolderLayer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CreateFolderLayer(string layerName);
        
        [DllImport(CExterns.DLL, EntryPoint = "getLayerCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetLayerCount();
        
        [DllImport(CExterns.DLL, EntryPoint = "getLayerInfo", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool GetLayerInfo(string layerName, out LayerInfoStruct outInfo);
        
        [DllImport(CExterns.DLL, EntryPoint = "getLayerInfoAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetLayerInfoAt(int index, out LayerInfoStruct outInfo);
        
        [DllImport(CExterns.DLL, EntryPoint = "replaceLayerTileset", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool ReplaceLayerTileset(string layerName, string tilesetName);

        [DllImport(CExterns.DLL, EntryPoint = "setActiveLayer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool SetActiveLayer(string layerName);
        
        [DllImport(CExterns.DLL, EntryPoint = "setActiveLayerAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetActiveLayerAt(int index);
        
        [DllImport(CExterns.DLL, EntryPoint = "setLayerProperties", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern bool _SetLayerProperties(string layerName, ref LayerInfoStruct properties);
        
        public static bool SetLayerProperties(string originalName, string newName, bool visible, string? tilesetName = null, int type = 0, bool silhouette = false, System.Drawing.Color silhouetteColor = default) {
            IntPtr namePtr = Marshal.StringToHGlobalAnsi(newName);
            IntPtr tilesetNamePtr = tilesetName != null ? Marshal.StringToHGlobalAnsi(tilesetName) : IntPtr.Zero;
            try {
                // Convert Color to RGBA (0xRRGGBBAA)
                int rgba = (silhouetteColor.R << 24) | (silhouetteColor.G << 16) | (silhouetteColor.B << 8) | silhouetteColor.A;
                var info = new CExternsEditor.LayerInfoStruct {
                    name = namePtr,
                    tilesetName = tilesetNamePtr,
                    type = type,
                    visible = visible ? 1 : 0,
                    silhouette = silhouette,
                    silhouetteColor = rgba
                };
                return _SetLayerProperties(originalName, ref info);
            } finally {
                Marshal.FreeHGlobal(namePtr);
                if (tilesetNamePtr != IntPtr.Zero) {
                    Marshal.FreeHGlobal(tilesetNamePtr);
                }
            }
        }

        [DllImport(CExterns.DLL, EntryPoint = "setLayerPropertiesAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetLayerPropertiesAt(int index, ref LayerInfoStruct properties);

        [DllImport(CExterns.DLL, EntryPoint = "removeLayer", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool RemoveLayer(string layerName);
        
        [DllImport(CExterns.DLL, EntryPoint = "removeLayerByIndex", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RemoveLayerByIndex(int index);

        [DllImport(CExterns.DLL, EntryPoint = "moveLayerUp", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool MoveLayerUp(string layerName);
        
        [DllImport(CExterns.DLL, EntryPoint = "moveLayerDown", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool MoveLayerDown(string layerName);
        
        [DllImport(CExterns.DLL, EntryPoint = "moveLayerTo", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool MoveLayerTo(string layerName, int newIndex);
        
        [DllImport(CExterns.DLL, EntryPoint = "moveLayerUpByIndex", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MoveLayerUpByIndex(int index);
        
        [DllImport(CExterns.DLL, EntryPoint = "moveLayerDownByIndex", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MoveLayerDownByIndex(int index);
        
        [DllImport(CExterns.DLL, EntryPoint = "setToolType")]
        public static extern void SetToolType(int toolType);
        
        [DllImport(CExterns.DLL, EntryPoint = "getToolType")]
        public static extern int GetToolType();

        #endregion
        
        #region Layer batch managment
        
        [DllImport(CExterns.DLL, EntryPoint = "getEntityLayerBatchCount", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int GetEntityLayerBatchCount(string layerName);

        [DllImport(CExterns.DLL, EntryPoint = "getEntityLayerBatchCountAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEntityLayerBatchCountAt(int index);

        [DllImport(CExterns.DLL, EntryPoint = "getEntityLayerBatchTilesetName", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern IntPtr _GetEntityLayerBatchTilesetName(string layerName, int batchIndex);
        
        public static string? GetEntityLayerBatchTilesetName(string layerName, int batchIndex) {
            IntPtr ptr = _GetEntityLayerBatchTilesetName(layerName, batchIndex);
            return ptr == IntPtr.Zero ? null : Marshal.PtrToStringAnsi(ptr);
        }

        [DllImport(CExterns.DLL, EntryPoint = "getEntityLayerInstanceCount", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int GetEntityLayerInstanceCount(string layerName, int batchIndex);

        [DllImport(CExterns.DLL, EntryPoint = "getEntityLayerInstanceAt", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int GetEntityLayerInstanceAt(string layerName, int batchIndex, int instanceIndex, out EntityStruct outData);

        [DllImport(CExterns.DLL, EntryPoint = "moveEntityLayerBatchUp", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool MoveEntityLayerBatchUp(string layerName, int batchIndex);

        [DllImport(CExterns.DLL, EntryPoint = "moveEntityLayerBatchDown", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool MoveEntityLayerBatchDown(string layerName, int batchIndex);

        [DllImport(CExterns.DLL, EntryPoint = "moveEntityLayerBatchTo", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool MoveEntityLayerBatchTo(string layerName, int batchIndex, int newIndex);

        [DllImport(CExterns.DLL, EntryPoint = "moveEntityLayerBatchUpByIndex", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MoveEntityLayerBatchUpByIndex(int layerIndex, int batchIndex);
        
        [DllImport(CExterns.DLL, EntryPoint = "moveEntityLayerBatchDownByIndex", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MoveEntityLayerBatchDownByIndex(int layerIndex, int batchIndex);
        
        [DllImport(CExterns.DLL, EntryPoint = "moveEntityLayerBatchToByIndex", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MoveEntityLayerBatchToByIndex(int layerIndex, int batchIndex, int newIndex);

        #endregion
        
    }
