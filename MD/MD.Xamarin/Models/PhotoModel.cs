using Newtonsoft.Json;

namespace MD.Xamarin.Models
{
    public class PhotoModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("noteid")]
        public int NoteId { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }
}