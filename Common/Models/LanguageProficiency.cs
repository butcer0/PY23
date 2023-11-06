using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PY23.CommonContracts.Enums;

namespace PY23.Common.Models
{
    public class LanguageProficiency
    {
        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; } // The name of the language

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "proficiency")]
        public ProfileEnums.ProficiencyLevel Proficiency { get; set; } // Proficiency level in the language
    }
}
