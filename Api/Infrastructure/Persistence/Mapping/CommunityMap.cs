namespace ThreadsBackend.Api.Infrastructure.Persistence.Mapping;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadsBackend.Api.Domain.Entities;

public class CommunityMap : IEntityTypeConfiguration<Community>
{
    public void Configure(EntityTypeBuilder<Community> modelBuilder)
    {
        modelBuilder
            .HasIndex(c => c.Id).IsUnique();
        modelBuilder
            .HasIndex(c => c.Username).IsUnique();

        // modelBuilder
        //     .HasOne(c => c.CreatedBy)
        //     .WithMany(u => u.Communities)
        //     .HasForeignKey(c => c.CreatedById)
        //     .OnDelete(DeleteBehavior.Restrict);
        //
        // modelBuilder
        //     .HasMany(c => c.Members)
        //     .WithMany(m => m.Communities);

        modelBuilder
            .HasMany(c => c.Threads)
            .WithOne(t => t.Community)
            .HasForeignKey(t => t.CommunityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
