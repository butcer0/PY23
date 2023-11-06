using System.Net;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PY23.Common.Exceptions;
using PY23.UserProfile.Helpers;
using PY23.UserProfile.Interfaces.Services;

namespace PY23.UserProfile.Functions;
public class AddUserProfileFunction
{
    private readonly IUserProfileService _userProfileService;
    private readonly ILogger _logger;
    private readonly Guid _correlationId;

    public AddUserProfileFunction(IUserProfileService userProfileService, ILoggerFactory loggerFactory)
    {
        _userProfileService = userProfileService;
        _logger = loggerFactory.CreateLogger<AddUserProfileFunction>();
        _correlationId = Guid.NewGuid();
    }

    [Function("AddUserProfile")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        
        var logger = executionContext.GetLogger("AddUserProfile");
        logger.LogInformation($"Received a request to add a user profile with CorrelationId: {_correlationId}");

        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var userProfile = UserProfileHelper.CreateNewUserProfile(requestBody, _correlationId);

        try
        {
            var createdUserProfile = await _userProfileService.AddUserProfileAsync(userProfile);
            var response = req.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Content-Type", "application/json");
            await response.WriteStringAsync(JsonConvert.SerializeObject(createdUserProfile));
            return response;
        }
        catch (CosmosException ex)
        {
            logger.LogError(ex, $"Cosmos DB error occurred while adding the user profile: {ex.Message}, CorrelationId: {_correlationId}");
            var errorResponse = req.CreateResponse(ex.StatusCode);
            await errorResponse.WriteAsJsonAsync(new { Message = ex.Message, CorrelationId = _correlationId });
            return errorResponse;
        }
        catch (ApiException ex)
        {
            logger.LogError($"An API error occurred while adding the user profile: {ex.Message}, CorrelationId: {_correlationId}");
            var errorResponse = req.CreateResponse(ex.StatusCode);
            await errorResponse.WriteAsJsonAsync(new { Message = ex.Message, CorrelationId = _correlationId });
            return errorResponse;
        }
        catch (Exception ex)
        {
            logger.LogError($"An error occurred while adding the user profile: {ex.Message}, CorrelationId: {_correlationId}");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteAsJsonAsync(new { Message = "An internal error occurred.", CorrelationId = _correlationId });
            return errorResponse;
        }
    }
}
