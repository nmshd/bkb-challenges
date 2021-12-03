using System;
using Challenges.API.Extensions;
using Challenges.API.JsonConverters;
using Challenges.Application.Extensions;
using Challenges.Infrastructure.EventBus;
using Challenges.Infrastructure.Persistence;
using Enmeshed.BuildingBlocks.API.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Challenges.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCustomAspNetCore(_configuration, _env, options =>
            {
                options.Authentication.Audience = "challenges";
                options.Authentication.Authority = _configuration.GetAuthorizationConfiguration().Authority;
                options.Authentication.ValidIssuer = _configuration.GetAuthorizationConfiguration().ValidIssuer;

                options.Cors.AllowedOrigins = _configuration.GetCorsConfiguration().AllowedOrigins;
                options.Cors.ExposedHeaders = _configuration.GetCorsConfiguration().ExposedHeaders;

                options.HealthChecks.SqlConnectionString = _configuration.GetSqlDatabaseConfiguration().ConnectionString;

                options.Json.Converters.Add(new ChallengeIdJsonConverter());
            });

            services.AddCustomApplicationInsights();

            services.AddCustomFluentValidation();

            services.AddDatabase(dbOptions => { dbOptions.DbConnectionString = _configuration.GetSqlDatabaseConfiguration().ConnectionString; });

            services.AddEventBus(_configuration.GetEventBusConfiguration());

            services.AddApplication();

            return services.ToAutofacServiceProvider();
        }

        public void Configure(IApplicationBuilder app, TelemetryConfiguration telemetryConfiguration)
        {
            telemetryConfiguration.DisableTelemetry = !_configuration.GetApplicationInsightsConfiguration().Enabled;

            app.ConfigureMiddleware(_env);
        }
    }
}
