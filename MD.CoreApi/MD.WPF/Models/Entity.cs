using Newtonsoft.Json;

namespace MD.WPF.Models
{
    public class Entity
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}