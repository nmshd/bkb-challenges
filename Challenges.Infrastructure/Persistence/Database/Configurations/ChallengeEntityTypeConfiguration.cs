using Challenges.Domain.Entities;
using Challenges.Domain.Ids;
using Enmeshed.DevelopmentKit.Identity.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenges.Infrastructure.Persistence.Database.Configurations
{
    public class ChallengeEventEntityTypeConfiguration : IEntityTypeConfiguration<Challenge>
    {
        public void Configure(EntityTypeBuilder<Challenge> builder)
        {
            builder.Property(x => x.Id).HasColumnType($"char({ChallengeId.MAX_LENGTH})");
            builder.Property(x => x.CreatedBy).HasColumnType($"char({IdentityAddress.MAX_LENGTH})");
            builder.Property(x => x.CreatedByDevice).HasColumnType($"char({DeviceId.MAX_LENGTH})");
        }
    }
}
