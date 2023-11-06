using CM = PY23.Common.Models;

namespace PY23.UserProfile.Interfaces.Services
{
    public interface IUserProfileService
    {
        Task<CM.UserProfile> GetUserProfileAsync(string id);
        Task<CM.UserProfile> AddUserProfileAsync(CM.UserProfile userProfile);
    }
}
