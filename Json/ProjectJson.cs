using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace csharp_editor.Json {
    public class ProjectJson {

        public ProjectJson()
        {
            Path = "";
            Tasks = new List<string>();
            SourceFolder = "";
            ResourcesFolder = "";
        }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("tasks")]
        public List<string> Tasks { get; set; }

        [JsonPropertyName("sourceFolder")]
        public string SourceFolder { get; set; }

        [JsonPropertyName("resourcesFolder")]
        public string ResourcesFolder { get; set; }
    }
}
