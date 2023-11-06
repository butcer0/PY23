using Newtonsoft.Json;

namespace PY23.Common.Models
{
    public class SocialProfiles
    {
        [JsonProperty(PropertyName = "facebook")]
        public string Facebook { get; set; }

        [JsonProperty(PropertyName = "twitter")]
        public string Twitter { get; set; }

        [JsonProperty(PropertyName = "instagram")]
        public string Instagram { get; set; }

        // Other social networks can be added as needed
    }
}
