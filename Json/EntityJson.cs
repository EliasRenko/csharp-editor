using System.Text.Json.Serialization;

namespace csharp_editor.Json
{
    class EntityJson
    {
        public EntityJson(string name)
        {
            Name = name;
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
