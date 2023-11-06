using CM = PY23.Common.Models;
using PY23.UserProfile.Interfaces.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using System.Net;

namespace PY23.UserProfile.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly Container _container;
    private readonly ILogger _logger;

    public UserProfileRepository(CosmosClient cosmosClient, string databaseName, string containerName, ILogger logger)
    {
        _container = cosmosClient.GetContainer(databaseName, containerName);
        _logger = logger;
    }

    public async Task<CM.UserProfile> GetUserProfileAsync(string id)
    {
        try
        {
            var response = await _container.ReadItemAsync<CM.UserProfile>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogInformation($"User profile not found for ID: {id}. StatusCode: {ex.StatusCode}");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while retrieving user profile for ID: {id}");
            throw;
        }
    }


    public async Task<CM.UserProfile> AddUserProfileAsync(CM.UserProfile userProfile)
    {
        try
        {
            var response = await _container.CreateItemAsync(userProfile, new PartitionKey(userProfile.Id.ToString()));
            _logger.LogInformation($"User profile created for ID: {userProfile.UserId}. CorrelationId: {userProfile.CorrelationId}");
            return response.Resource;
        }
        catch (CosmosException ex)
        {
            _logger.LogError(ex, $"Cosmos DB error occurred while creating user profile for ID: {userProfile.UserId}. CorrelationId: {userProfile.CorrelationId}");
            throw; 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while creating a user profile for ID: {userProfile.UserId}");
            throw;
        }
    }

    public async Task<CM.UserProfile> UpdateUserProfileAsync(CM.UserProfile userProfile)
    {
        try
        {
            var response = await _container.UpsertItemAsync(userProfile, new PartitionKey(userProfile.UserId));
            _logger.LogInformation($"User profile updated for ID: {userProfile.UserId}. CorrelationId: {userProfile.CorrelationId}");
            return response.Resource;
        }
        catch (CosmosException ex)
        {
            _logger.LogError(ex, $"Cosmos DB error occurred while updating user profile for ID: {userProfile.UserId}. CorrelationId: {userProfile.CorrelationId}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while updating user profile for ID: {userProfile.UserId}");
            throw;
        }
    }
}
