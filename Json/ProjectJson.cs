using System.Text.Json.Serialization;

namespace csharp_editor.Json {
    public class ProjectJson {

        [JsonPropertyName("path")]
        public string? Path { get; set; }
    }
}
