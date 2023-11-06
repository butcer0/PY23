using CM = PY23.Common.Models;
using PY23.UserProfile.Interfaces.Repositories;
using PY23.UserProfile.Interfaces.Services;

namespace PY23.UserProfile.Services;

public class UserProfileService : IUserProfileService
{
    private readonly IUserProfileRepository _userProfileRepository;

    public UserProfileService(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }

    public async Task<CM.UserProfile> GetUserProfileAsync(string id)
    {
        return await _userProfileRepository.GetUserProfileAsync(id);
    }

    public async Task<CM.UserProfile> AddUserProfileAsync(CM.UserProfile userProfile)
    {
        return await _userProfileRepository.AddUserProfileAsync(userProfile);
    }
}
