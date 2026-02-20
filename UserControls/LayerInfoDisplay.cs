using System.ComponentModel;

namespace csharp_editor.UserControls {
    public class LayerInfoDisplay : INotifyPropertyChanged {
    private bool _silhouette;
    private System.Drawing.Color _silhouetteColor = System.Drawing.Color.Black;
        [Category("Layer"), Description("Show silhouette")]
        public bool Silhouette {
            get => _silhouette;
            set {
                if (_silhouette != value) {
                    _silhouette = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Silhouette)));
                }
            }
        }

        [Category("Layer"), Description("Silhouette color"), Editor(typeof(ModalOnlyColorEditor), typeof(System.Drawing.Design.UITypeEditor)), TypeConverter(typeof(ColorNoTextTypeConverter))]
        public System.Drawing.Color SilhouetteColor {
            get => _silhouetteColor;
            set {
                if (_silhouetteColor != value) {
                    _silhouetteColor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SilhouetteColor)));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        // Stores the original name used as the backend ID before any rename
        [Browsable(false)]
        public string OriginalName { get; private set; } = "";

        private string _name = "";
        private int _type;
        private string _tilesetName = "";
        private bool _visible;

        [Category("Layer"), Description("Layer name")]
        public string Name {
            get => _name;
            set {
                if (_name != value) {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }

        [Category("Layer"), Description("Silhouette color"), Editor(typeof(System.Drawing.Design.ColorEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public int Type {
            get => _type;
            set {
                if (_type != value) {
                    _type = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Type)));
                }
            }
        }

        [Category("Layer"), Description("Tileset name for TileLayers"), ReadOnly(true)]
        public string TilesetName {
            get => _tilesetName;
            set {
                if (_tilesetName != value) {
                    _tilesetName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TilesetName)));
                }
            }
        }

        [Category("Layer"), Description("Visibility flag")]
        public bool Visible {
            get => _visible;
            set {
                if (_visible != value) {
                    _visible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Visible)));
                }
            }
        }

        public void SetOriginalName(string name) {
            OriginalName = name;
        }
    }
}
