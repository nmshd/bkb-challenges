using System;
using System.Collections.Generic;
using System.Reflection;
using Enmeshed.BuildingBlocks.Application.Abstractions.Infrastructure.Persistence.Database;
using Enmeshed.BuildingBlocks.Application.Abstractions.Infrastructure.UserContext;
using Challenges.Application.Extensions;
using Challenges.Infrastructure.Persistence.Database;
using Enmeshed.DevelopmentKit.Identity.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Challenges.Jobs.Cleanup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;

                    services.AddHostedService<Worker>();

                    services.AddApplication();

                    services.AddSingleton<IUserContext, FakeUserContext>();

                    services.AddDbContext<ApplicationDbContext>(dbContextOptions =>
                        dbContextOptions.UseSqlServer(configuration.GetSection("SqlDatabase")["ConnectionString"], sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name);
                            sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                        }), ServiceLifetime.Singleton);

                    services.AddSingleton<IDbContext, ApplicationDbContext>();
                });
        }
    }

    public class FakeUserContext : IUserContext
    {
        public IdentityAddress GetAddress()
        {
            throw new NotImplementedException();
        }

        public IdentityAddress GetAddressOrNull()
        {
            throw new NotImplementedException();
        }

        public DeviceId GetDeviceId()
        {
            throw new NotImplementedException();
        }

        public DeviceId GetDeviceIdOrNull()
        {
            throw new NotImplementedException();
        }

        public string GetUserId()
        {
            throw new NotImplementedException();
        }

        public string GetUserIdOrNull()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetRoles()
        {
            throw new NotImplementedException();
        }

        public SubscriptionPlan GetSubscriptionPlan()
        {
            throw new NotImplementedException();
        }
    }
}
