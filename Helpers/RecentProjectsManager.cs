using System.Text.Json;

namespace csharp_editor.Helpers {
    /// <summary>
    /// Persists a list of recently opened project paths to
    /// %LOCALAPPDATA%\csharp-editor\recent.json.
    /// </summary>
    internal static class RecentProjectsManager {
        private static readonly string ConfigDir =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "csharp-editor");
        private static readonly string ConfigFile = Path.Combine(ConfigDir, "recent.json");
        private const int MaxEntries = 12;

        public static List<string> Load() {
            try {
                if (!File.Exists(ConfigFile)) return new();
                string json = File.ReadAllText(ConfigFile);
                return JsonSerializer.Deserialize<List<string>>(json) ?? new();
            }
            catch {
                return new();
            }
        }

        public static void Add(string path) {
            var list = Load();
            list.RemoveAll(p => string.Equals(p, path, StringComparison.OrdinalIgnoreCase));
            list.Insert(0, path);
            if (list.Count > MaxEntries)
                list = list.Take(MaxEntries).ToList();
            Save(list);
        }

        public static void Remove(string path) {
            var list = Load();
            list.RemoveAll(p => string.Equals(p, path, StringComparison.OrdinalIgnoreCase));
            Save(list);
        }

        private static void Save(List<string> paths) {
            try {
                Directory.CreateDirectory(ConfigDir);
                File.WriteAllText(ConfigFile,
                    JsonSerializer.Serialize(paths, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch { /* non-critical */ }
        }
    }
}
