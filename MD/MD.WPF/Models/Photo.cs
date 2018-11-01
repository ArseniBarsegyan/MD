using Newtonsoft.Json;

namespace MD.WPF.Models
{
    public class Photo : Entity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("noteId")]
        public int NoteId { get; set; }

        [JsonIgnore]
        public Note Note { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }
}