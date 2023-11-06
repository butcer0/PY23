using System.Net;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PY23.Common.Exceptions;
using PY23.UserProfile.Interfaces.Services;

namespace PY23.UserProfile.Functions;

public class GetUserProfileFunction
{
    private readonly IUserProfileService _userProfileService;
    private readonly ILogger _logger;
    private readonly Guid _correlationId;

    public GetUserProfileFunction(IUserProfileService userProfileService, ILoggerFactory loggerFactory)
    {
        _userProfileService = userProfileService;
        _logger = loggerFactory.CreateLogger<GetUserProfileFunction>();
        _correlationId = Guid.NewGuid();
    }

    [Function("GetUserProfile")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation($"Received a request to get a user profile with CorrelationId: {_correlationId}");

        // Read the userProfileId from the request header
        if (!req.Headers.TryGetValues("x-id", out var userProfileIdValues))
        {
            var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            await badRequestResponse.WriteStringAsync("The user profile ID header is missing.");
            return badRequestResponse;
        }

        var userProfileId = userProfileIdValues.FirstOrDefault();
        if (string.IsNullOrWhiteSpace(userProfileId))
        {
            var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            await badRequestResponse.WriteStringAsync("The user profile ID header cannot be empty.");
            return badRequestResponse;
        }

        try
        {
            var userProfile = await _userProfileService.GetUserProfileAsync(userProfileId);
            if (userProfile == null)
            {
                var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
                await notFoundResponse.WriteStringAsync($"User profile with ID {userProfileId} not found.");
                return notFoundResponse;
            }

            var okResponse = req.CreateResponse(HttpStatusCode.OK);
            okResponse.Headers.Add("Content-Type", "application/json");
            await okResponse.WriteStringAsync(JsonConvert.SerializeObject(userProfile));
            return okResponse;
        }
        catch (CosmosException ex)
        {
            _logger.LogError(ex, $"Cosmos DB error occurred while retrieving the user profile: {ex.Message}, CorrelationId: {_correlationId}");
            var errorResponse = req.CreateResponse(ex.StatusCode);
            await errorResponse.WriteStringAsync(JsonConvert.SerializeObject(new { Message = ex.Message, CorrelationId = _correlationId }));
            return errorResponse;
        }
        catch (ApiException ex)
        {
            _logger.LogError($"An API error occurred while retrieving the user profile: {ex.Message}, CorrelationId: {_correlationId}");
            var errorResponse = req.CreateResponse(ex.StatusCode);
            await errorResponse.WriteStringAsync(JsonConvert.SerializeObject(new { Message = ex.Message, CorrelationId = _correlationId }));
            return errorResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while retrieving the user profile: {ex.Message}, CorrelationId: {_correlationId}");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync(JsonConvert.SerializeObject(new { Message = "An internal error occurred.", CorrelationId = _correlationId }));
            return errorResponse;
        }
    }
}
