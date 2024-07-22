namespace csharp_editor {
    internal class Entity {

        public Action<Entity>? callback;

        private int _id;

        private int _x;

        private int _y;

        public Entity(int id) {

            _id = id;
        }

        public int X {

            get { return _x; }

            set { 

                _x = value;

                onChange();
            }
        }

        public int Y {

            get { return _y; }

            set { 

                _y = value;

                onChange();
            }
        }

        public int id {

            get { return _id; }
        }

        private void onChange() {

            if (callback != null) {

                callback.Invoke(this);
            }
        }
    }
}
