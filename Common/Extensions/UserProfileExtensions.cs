using PY23.Common.Models;
using System;

namespace PY23.Common.Extensions
{
    public static class UserProfileExtensions
    {
        public static void SetCorrelationId(this UserProfile userProfile, Guid correlationId)
        {
            userProfile.CorrelationId = correlationId;
        }
    }
}
