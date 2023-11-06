using CM = PY23.Common.Models;

namespace PY23.UserProfile.Interfaces.Repositories;

public interface IUserProfileRepository
{
    Task<CM.UserProfile> GetUserProfileAsync(string id);
    Task<CM.UserProfile> AddUserProfileAsync(CM.UserProfile userProfile);
    Task<CM.UserProfile> UpdateUserProfileAsync(CM.UserProfile userProfile);
}
