using Challenges.Domain.Entities;
using Challenges.Domain.Ids;
using Challenges.Infrastructure.Persistence.Database.ValueConverters;
using Enmeshed.BuildingBlocks.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Challenges.Infrastructure.Persistence.Database;

public class ApplicationDbContext : AbstractDbContextBase
{
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public virtual DbSet<Challenge> Challenges { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.UseValueConverter(new ChallengeIdEntityFrameworkValueConverter(new ConverterMappingHints(ChallengeId.MAX_LENGTH)));

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
