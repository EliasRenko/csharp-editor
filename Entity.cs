namespace csharp_editor {
    internal class Entity {

        private int _id;

        private int _x;

        private int _y;

        public Entity(int id) {

            _id = id;
        }

        public int X {

            get { return _x; }

            set { _x = value; }
        }

        public int Y {

            get { return _y; }

            set { _y = value; }
        }
    }
}
