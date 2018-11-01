using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MD.WPF.Models
{
    public class Note : Entity
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("photos")]
        public List<Photo> Photos { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }

        [JsonIgnore]
        public AppUser User { get; set; }
    }
}