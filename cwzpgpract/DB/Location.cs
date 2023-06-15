using System.Text.Json.Serialization;

namespace cwzpgpract.DB
{
    public class Location
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public List<Item> Items_ { get; set; } = new List<Item>();
        [JsonIgnore]
        public List<NPC> NPC_ { get; set; } = new List<NPC>();
    }
}
