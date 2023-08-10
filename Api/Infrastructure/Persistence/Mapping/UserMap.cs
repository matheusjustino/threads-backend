namespace ThreadsBackend.Api.Infrastructure.Persistence.Mapping;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadsBackend.Api.Domain.Entities;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> modelBuilder)
    {
        modelBuilder
            .HasIndex(u => u.Id).IsUnique();

        modelBuilder
            .HasIndex(p => p.Username).IsUnique();

        modelBuilder
            .HasMany(u => u.Threads)
            .WithOne(t => t.Author)
            .HasForeignKey(t => t.AuthorId);

        // modelBuilder
        //     .HasMany(u => u.Communities)
        //     .WithOne(c => c.CreatedBy)
        //     .HasForeignKey(c => c.CreatedById);
    }
}
