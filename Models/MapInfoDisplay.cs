using System.ComponentModel;
using System.Drawing;
using csharp_editor.UserControls;

namespace csharp_editor.Models {
    public class MapInfoDisplay : INotifyPropertyChanged {
        private string? _idd;
        private string? _name;
        private int _worldx;
        private int _worldy;
        private int _width;
        private int _height;
        private int _tileSize;

        private int _tileSizeX;
        private int _tileSizeY;
        private Color _backgroundColor = Color.Black;
        private Color _gridColor = Color.Gray;

        [Category("Map"), Description("Internal map identifier"), ReadOnly(true)]
        public string? ID {
            get => _idd;
            set {
                if (_idd != value) {
                    _idd = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
                }
            }
        }

        [Category("Map"), Description("Map name"), ReadOnly(true)]
        public string? Name {
            get => _name;
            set {
                if (_name != value) {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }

        [Category("Map"), Description("World X coordinate"), ReadOnly(true)]
        public int WorldX {
            get => _worldx;
            set {
                if (_worldx != value) {
                    _worldx = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WorldX)));
                }
            }
        }

        [Category("Map"), Description("World Y coordinate"), ReadOnly(true)]
        public int WorldY {
            get => _worldy;
            set {
                if (_worldy != value) {
                    _worldy = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WorldY)));
                }
            }
        }

        [Category("Map"), Description("Map width in tiles"), ReadOnly(true)]
        public int Width {
            get => _width;
            set {
                if (_width != value) {
                    _width = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Width)));
                }
            }
        }

        [Category("Map"), Description("Map height in tiles"), ReadOnly(true)]
        public int Height {
            get => _height;
            set {
                if (_height != value) {
                    _height = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Height)));
                }
            }
        }

        [Category("Map"), Description("TileX size in pixels"), ReadOnly(false)]
        public int TileSizeX {
            get => _tileSizeX;
            set {
                if (_tileSizeX != value) {
                    _tileSizeX = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TileSizeX)));
                }
            }
        }

        [Category("Map"), Description("TileY size in pixels"), ReadOnly(false)]
        public int TileSizeY {
            get => _tileSizeY;
            set {
                if (_tileSizeY != value) {
                    _tileSizeY = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TileSizeY)));
                }
            }
        }

        [Category("Map"), Description("Background color"), Editor(typeof(ModalOnlyColorEditor), typeof(System.Drawing.Design.UITypeEditor)), TypeConverter(typeof(ColorNoTextTypeConverter))]
        public Color BackgroundColor {
            get => _backgroundColor;
            set {
                if (_backgroundColor != value) {
                    _backgroundColor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BackgroundColor)));
                }
            }
        }

        [Category("Map"), Description("Grid color"), Editor(typeof(ModalOnlyColorEditor), typeof(System.Drawing.Design.UITypeEditor)), TypeConverter(typeof(ColorNoTextTypeConverter))]
        public Color GridColor {
            get => _gridColor;
            set {
                if (_gridColor != value) {
                    _gridColor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GridColor)));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
