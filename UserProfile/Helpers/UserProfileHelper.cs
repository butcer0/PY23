using CM = PY23.Common.Models;
using Newtonsoft.Json;
using PY23.Common.Exceptions;
using System.Net;

namespace PY23.UserProfile.Helpers
{
    public static class UserProfileHelper
    {
        public static CM.UserProfile CreateNewUserProfile(string serializedUserProfile, Guid correlationId)
        {
            var userProfile = JsonConvert.DeserializeObject<CM.UserProfile>(serializedUserProfile);

            if (userProfile == null)
            {
                throw new ApiException(HttpStatusCode.BadRequest, "Please provide a user profile in the request body.");
            }

            userProfile.Id = Guid.NewGuid();
            userProfile.CorrelationId = correlationId;
            return userProfile;
        }
    }
}
