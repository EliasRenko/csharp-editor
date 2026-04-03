using System.Text.Json;
using csharp_editor.Models;

namespace csharp_editor.Helpers {

    /// <summary>
    /// Loads and saves the application <see cref="AppTheme"/> to
    /// %LOCALAPPDATA%\csharp-editor\theme.json.
    /// </summary>
    public static class AppThemeManager {

        private static readonly string ConfigDir =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                         "csharp-editor");

        private static readonly string ConfigFile = Path.Combine(ConfigDir, "theme.json");

        private static AppTheme? _current;

        private static readonly JsonSerializerOptions _jsonOptions =
            new() { WriteIndented = true };

        /// <summary>
        /// Fired whenever the active theme changes (live preview or save).
        /// Subscribers should re-apply colors to their controls.
        /// </summary>
        public static event Action<AppTheme>? ThemeUpdated;

        /// <summary>
        /// Updates <see cref="Current"/> in memory and fires <see cref="ThemeUpdated"/>
        /// without writing to disk. Use <see cref="Save"/> to persist.
        /// </summary>
        public static void Apply(AppTheme theme) {
            _current = theme;
            ThemeUpdated?.Invoke(theme);
        }

        /// <summary>
        /// The active theme. Loaded lazily on first access; never null.
        /// </summary>
        public static AppTheme Current {
            get {
                if (_current == null) _current = Load();
                return _current;
            }
        }

        /// <summary>Loads the theme from disk, or returns the default if the file is missing/corrupt.</summary>
        public static AppTheme Load() {
            try {
                if (!File.Exists(ConfigFile)) return AppTheme.Default;
                string json = File.ReadAllText(ConfigFile);
                _current = JsonSerializer.Deserialize<AppTheme>(json) ?? AppTheme.Default;
                return _current;
            } catch {
                return AppTheme.Default;
            }
        }

        /// <summary>Persists <paramref name="theme"/> to disk, updates <see cref="Current"/>, and fires <see cref="ThemeUpdated"/>.</summary>
        public static void Save(AppTheme theme) {
            Apply(theme);
            try {
                Directory.CreateDirectory(ConfigDir);
                File.WriteAllText(ConfigFile, JsonSerializer.Serialize(theme, _jsonOptions));
            } catch { /* non-critical */ }
        }

        /// <summary>Resets to defaults in memory and removes the persisted file.</summary>
        public static void Reset() {
            _current = AppTheme.Default;
            try {
                if (File.Exists(ConfigFile)) File.Delete(ConfigFile);
            } catch { /* non-critical */ }
        }

        /// <summary>Returns a deep copy of <paramref name="theme"/> via JSON round-trip.</summary>
        public static AppTheme Clone(AppTheme theme) {
            var json = JsonSerializer.Serialize(theme, _jsonOptions);
            return JsonSerializer.Deserialize<AppTheme>(json)!;
        }
    }
}
