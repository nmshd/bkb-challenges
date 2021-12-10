using Azure.Identity;
using Challenges.API;
using Challenges.API.Extensions;
using Challenges.Infrastructure.Persistence.Database;
using Enmeshed.BuildingBlocks.API.Extensions;
using Microsoft.AspNetCore;

var host = CreateWebHostBuilder(args)
    .Build()
    .MigrateDbContext<ApplicationDbContext>((_, _) => { });

host.Run();

static IWebHostBuilder CreateWebHostBuilder(string[] args)
{
    return WebHost.CreateDefaultBuilder(args)
        .UseKestrel(options =>
        {
            options.AddServerHeader = false;
            options.Limits.MaxRequestBodySize = 0;
        })
        .ConfigureAppConfiguration(AddAzureAppConfiguration)
        .UseStartup<Startup>();
}

static void AddAzureAppConfiguration(WebHostBuilderContext hostingContext, IConfigurationBuilder builder)
{
    var configuration = builder.Build();

    var azureAppConfigurationConfiguration = configuration.GetAzureAppConfigurationConfiguration();

    if (azureAppConfigurationConfiguration.Enabled)
        builder.AddAzureAppConfiguration(appConfigurationOptions =>
        {
            var credentials = new ManagedIdentityCredential();

            appConfigurationOptions
                .Connect(new Uri(azureAppConfigurationConfiguration.Endpoint), credentials)
                .ConfigureKeyVault(vaultOptions => { vaultOptions.SetCredential(credentials); })
                .Select("*", "")
                .Select("*", "Challenges");
        });
}