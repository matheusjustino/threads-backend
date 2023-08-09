namespace ThreadsBackend.Api.Infrastructure.Persistence.Mapping;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadsBackend.Api.Domain.Entities;

public class ThreadMap : IEntityTypeConfiguration<Thread>
{
    public void Configure(EntityTypeBuilder<Thread> modelBuilder)
    {
        modelBuilder
            .HasOne(t => t.Author)
            .WithMany(u => u.Threads)
            .HasForeignKey(t => t.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .HasMany(t => t.Comments)
            .WithOne()
            .HasForeignKey(t => t.ParentThreadId);
    }
}