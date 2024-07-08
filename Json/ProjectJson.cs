using System.Text.Json.Serialization;

namespace csharp_editor.Json {
    public class ProjectJson {

        [JsonPropertyName("path")]
        public string? Path { get; set; }


        [JsonPropertyName("tasks")]
        public ArraySegment<string>? Tasks { get; set; }
    }
}
