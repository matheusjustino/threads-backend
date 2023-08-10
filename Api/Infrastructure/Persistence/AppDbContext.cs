namespace ThreadsBackend.Api.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using ThreadsBackend.Api.Domain.Entities;
using ThreadsBackend.Api.Infrastructure.Persistence.Mapping;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Thread> Threads { get; set; }

    public DbSet<Community> Communities { get; set; }

    public DbSet<CommunityMember> CommunityMembers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(new UserMap().Configure);
        modelBuilder.Entity<Thread>(new ThreadMap().Configure);
        modelBuilder.Entity<Community>(new CommunityMap().Configure);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<Entity>().ToList();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }

            if (entry.State is EntityState.Added or EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries<Entity>().ToList();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.Now;
                entry.Entity.UpdatedAt = DateTime.Now;
            }

            if (entry.State is EntityState.Added or EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.Now;
            }
        }

        return base.SaveChanges();
    }
}
