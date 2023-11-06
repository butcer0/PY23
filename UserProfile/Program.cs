using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;
using PY23.UserProfile.Interfaces.Repositories;
using PY23.UserProfile.Repositories;
using PY23.UserProfile.Interfaces.Services;
using PY23.UserProfile.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((context, config) =>
    {
        // Add other configuration providers if necessary
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        // Register the CosmosClient instance with the connection string from configuration
        services.AddSingleton(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var connectionString = configuration["UserProfileDb_ConnectionString"];
            return new CosmosClient(connectionString);
        });

        // Register the UserProfile repository with the specific container and database names
        services.AddSingleton<IUserProfileRepository>(sp =>
        {
            var cosmosClient = sp.GetRequiredService<CosmosClient>();
            var configuration = sp.GetRequiredService<IConfiguration>();
            var databaseName = configuration["UserProfileDb_DatabaseName"];
            var containerName = configuration["UserProfileDb_ContainerName"];
            var logger = sp.GetRequiredService<ILogger<UserProfileRepository>>();
            return new UserProfileRepository(cosmosClient, databaseName, containerName, logger);
        });

        // Register the UserProfile service
        services.AddTransient<IUserProfileService, UserProfileService>();

        // Register logging services if needed
        services.AddLogging();
    })
    .Build();

await host.RunAsync();
