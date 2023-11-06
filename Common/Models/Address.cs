using Newtonsoft.Json;

namespace PY23.Common.Models
{
    public class Address
    {
        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }
    }
}
