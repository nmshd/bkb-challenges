﻿using Challenges.Domain.Entities;
using Challenges.Domain.Ids;
using Challenges.Infrastructure.Persistence.Database.ValueConverters;
using Enmeshed.BuildingBlocks.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Challenges.Infrastructure.Persistence.Database;

public class ApplicationDbContext : AbstractDbContextBase
{
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public virtual DbSet<Challenge> Challenges { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<ChallengeId>().AreUnicode(false).AreFixedLength().HaveMaxLength(ChallengeId.MAX_LENGTH).HaveConversion<ChallengeIdEntityFrameworkValueConverter>();
    }
}
