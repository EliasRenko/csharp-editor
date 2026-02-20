using System.ComponentModel;

namespace csharp_editor.UserControls {
    public class LayerInfoDisplay : INotifyPropertyChanged {

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

        [Category("Layer"), Description("Layer type (0 = TileLayer, 1 = EntityLayer)"), ReadOnly(true)]
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
