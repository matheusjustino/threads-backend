namespace ThreadsBackend.Api.Infrastructure.Persistence.Mapping;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadsBackend.Api.Domain.Entities;

public class CommunityMemberMap : IEntityTypeConfiguration<CommunityMember>
{
    public void Configure(EntityTypeBuilder<CommunityMember> modelBuilder)
    {
        modelBuilder
            .HasIndex(cm => cm.Id).IsUnique();

        modelBuilder
            .HasOne(cm => cm.Community)
            .WithMany(c => c.Members)
            .HasForeignKey(cm => cm.CommunityId);

        modelBuilder
            .HasOne(cm => cm.Member)
            .WithMany(u => u.Communities)
            .HasForeignKey(cm => cm.CommunityId);
    }
}