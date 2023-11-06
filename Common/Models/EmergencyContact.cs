using Newtonsoft.Json;

namespace PY23.Common.Models
{
    public class EmergencyContact
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "relationship")]
        public string Relationship { get; set; } // E.g., "Parent", "Spouse", "Friend"
    }
}
