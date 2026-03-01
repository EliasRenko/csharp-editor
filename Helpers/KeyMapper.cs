namespace csharp_editor {
    public static class KeyMapper {
        public static int ToSDLScancode(Keys key) {
            switch (key) {
                // Letters A-Z (SDL: 97-122, lowercase ASCII)
                case Keys.A: return 97;
                case Keys.B: return 98;
                case Keys.C: return 99;
                case Keys.D: return 100;
                case Keys.E: return 101;
                case Keys.F: return 102;
                case Keys.G: return 103;
                case Keys.H: return 104;
                case Keys.I: return 105;
                case Keys.J: return 106;
                case Keys.K: return 107;
                case Keys.L: return 108;
                case Keys.M: return 109;
                case Keys.N: return 110;
                case Keys.O: return 111;
                case Keys.P: return 112;
                case Keys.Q: return 113;
                case Keys.R: return 114;
                case Keys.S: return 115;
                case Keys.T: return 116;
                case Keys.U: return 117;
                case Keys.V: return 118;
                case Keys.W: return 119;
                case Keys.X: return 120;
                case Keys.Y: return 121;
                case Keys.Z: return 122;

                // Numbers 0-9 (SDL: 48-57, ASCII)
                case Keys.D0: return 48;
                case Keys.D1: return 49;
                case Keys.D2: return 50;
                case Keys.D3: return 51;
                case Keys.D4: return 52;
                case Keys.D5: return 53;
                case Keys.D6: return 54;
                case Keys.D7: return 55;
                case Keys.D8: return 56;
                case Keys.D9: return 57;

                // Special keys
                case Keys.Return: return 13;
                case Keys.Escape: return 27;
                case Keys.Back: return 8;
                case Keys.Tab: return 9;
                case Keys.Space: return 32;
                case Keys.OemMinus: return 45;    // -
                case Keys.Oemplus: return 61;     // =
                case Keys.OemOpenBrackets: return 91;  // [
                case Keys.OemCloseBrackets: return 93; // ]
                case Keys.OemPipe: return 92;     // \ (backslash)
                case Keys.OemSemicolon: return 59; // ;
                case Keys.OemQuotes: return 39;    // '
                case Keys.Oemtilde: return 96;     // `
                case Keys.Oemcomma: return 44;     // ,
                case Keys.OemPeriod: return 46;    // .
                case Keys.OemQuestion: return 47;  // /
                case Keys.Delete: return 127;

                // Function keys F1-F12
                case Keys.F1: return 1073741882;
                case Keys.F2: return 1073741883;
                case Keys.F3: return 1073741884;
                case Keys.F4: return 1073741885;
                case Keys.F5: return 1073741886;
                case Keys.F6: return 1073741887;
                case Keys.F7: return 1073741888;
                case Keys.F8: return 1073741889;
                case Keys.F9: return 1073741890;
                case Keys.F10: return 1073741891;
                case Keys.F11: return 1073741892;
                case Keys.F12: return 1073741893;

                // System keys
                case Keys.PrintScreen: return 1073741894;
                case Keys.Scroll: return 1073741895;
                case Keys.Pause: return 1073741896;
                case Keys.Insert: return 1073741897;
                case Keys.Home: return 1073741898;
                case Keys.PageUp: return 1073741899;
                case Keys.End: return 1073741901;
                case Keys.PageDown: return 1073741902;
                
                // Arrow keys
                case Keys.Right: return 1073741903;
                case Keys.Left: return 1073741904;
                case Keys.Down: return 1073741905;
                case Keys.Up: return 1073741906;

                // Numpad
                case Keys.NumLock: return 1073741907;
                case Keys.Divide: return 1073741908;
                case Keys.Multiply: return 1073741909;
                case Keys.Subtract: return 1073741910;
                case Keys.Add: return 1073741911;
                case Keys.NumPad0: return 1073741922;
                case Keys.NumPad1: return 1073741913;
                case Keys.NumPad2: return 1073741914;
                case Keys.NumPad3: return 1073741915;
                case Keys.NumPad4: return 1073741916;
                case Keys.NumPad5: return 1073741917;
                case Keys.NumPad6: return 1073741918;
                case Keys.NumPad7: return 1073741919;
                case Keys.NumPad8: return 1073741920;
                case Keys.NumPad9: return 1073741921;
                case Keys.Decimal: return 1073741923;

                // Modifiers
                case Keys.LControlKey: return 1073742048;
                case Keys.LShiftKey: return 1073742049;
                case Keys.LMenu: return 1073742050; // Left Alt
                case Keys.RControlKey: return 1073742052;
                case Keys.RShiftKey: return 1073742053;
                case Keys.RMenu: return 1073742054; // Right Alt
                case Keys.CapsLock: return 1073741881;

                // Default fallback
                default: return 0; // UNKNOWN
            }
        }
    }
}
