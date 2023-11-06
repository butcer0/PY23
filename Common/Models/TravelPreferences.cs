using System.Collections.Generic;
using Newtonsoft.Json;

namespace PY23.Common.Models
{
    public class TravelPreferences
    {
        [JsonProperty(PropertyName = "primaryLanguage")]
        public string PrimaryLanguage { get; set; } // User's primary language

        [JsonProperty(PropertyName = "languagesSpoken")]
        public List<LanguageProficiency> LanguagesSpoken { get; set; } // List of languages and proficiency levels

        // Add more specific travel preferences as necessary
    }
}
