using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace csharp_editor.Json
{
    class MapJson
    {
        public MapJson(string name)
        {
            Name = name;
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("entites")]
        public List<string> Entites { get; set; }
    }
}
