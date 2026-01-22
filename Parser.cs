using csharp_editor.Json;
using System.IO;
using System.Text.Json;

namespace csharp_editor {
    internal class Parser {

        public static void GetConfiguration(string path) {

        }

        public static ProjectJson GetProject(string path) {

            string jsonString = File.ReadAllText(path);

            ProjectJson project = JsonSerializer.Deserialize<ProjectJson>(jsonString)!;

            return project;
        }
    }
}
