namespace csharp_editor {
    public static class MouseButtonMapper {
        public static int ToSDLMouseButton(MouseButtons button) {
            switch (button) {
                case MouseButtons.Left:
                    return 1;
                case MouseButtons.Middle:
                    return 2;
                case MouseButtons.Right:
                    return 3;
                default:
                    return 0;
            }
        }
    }
}
