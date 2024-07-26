namespace csharp_editor {
    internal class MapObject {

        public Action<MapObject>? callback;

        private Color _color;

        public Color color {

            get { return _color; }

            set {

                _color = value;

                onChange();
            }
        }

        private void onChange() {

            if (callback != null) {

                callback.Invoke(this);
            }
        }
    }
}
