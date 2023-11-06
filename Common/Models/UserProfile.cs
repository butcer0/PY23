using System;
using Newtonsoft.Json;

namespace PY23.Common.Models
{
    public class UserProfile
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; } // Unique document ID in Cosmos DB

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; } // Unique user ID, could be email or a username

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; } // Should be unique

        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "dateOfBirth")]
        public DateTime? DateOfBirth { get; set; } // Nullable for optional

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; } // Could be an enum: Male, Female, Non-Binary, Prefer Not to Say

        [JsonProperty(PropertyName = "profilePictureUrl")]
        public string ProfilePictureUrl { get; set; } // URL to the profile picture

        [JsonProperty(PropertyName = "homeAddress")]
        public Address HomeAddress { get; set; } // Use the Address class defined below

        [JsonProperty(PropertyName = "travelPreferences")]
        public TravelPreferences TravelPreferences { get; set; } // Contains user's travel preferences

        [JsonProperty(PropertyName = "socialProfiles")]
        public SocialProfiles SocialProfiles { get; set; } // Contains links to social media profiles

        [JsonProperty(PropertyName = "passportNumber")]
        public string PassportNumber { get; set; } // Sensitive data, consider encryption

        [JsonProperty(PropertyName = "emergencyContact")]
        public EmergencyContact EmergencyContact { get; set; } // Contains emergency contact information

        [JsonProperty(PropertyName = "paymentMethodId")]
        public string PaymentMethodId { get; set; } // Reference to secure payment method ID

        [JsonProperty(PropertyName = "createdAt")]
        public DateTime CreatedAt { get; set; } // Timestamp of profile creation

        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime UpdatedAt { get; set; } // Timestamp of last profile update
        [JsonProperty(PropertyName = "correlationId")]
        public Guid CorrelationId { get; set; } // Unique document ID in Cosmos DB
    }
}
