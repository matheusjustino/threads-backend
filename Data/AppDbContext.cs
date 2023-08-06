namespace ThreadsBackend.Data;

using Microsoft.EntityFrameworkCore;
using ThreadsBackend.Models;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Thread> Threads { get; set; }

    public DbSet<Community> Communities { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(p => p.Username).IsUnique();
        modelBuilder.Entity<User>()
            .HasIndex(p => p.Id).IsUnique();
        modelBuilder.Entity<Community>()
            .HasIndex(c => c.Id).IsUnique();
        modelBuilder.Entity<Community>()
            .HasIndex(c => c.Username).IsUnique();

        modelBuilder.Entity<Thread>()
            .HasOne(t => t.Author)
            .WithMany()
            .HasForeignKey(t => t.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Thread>()
            .HasMany(t => t.Comments)
            .WithOne()
            .HasForeignKey(t => t.ParentThreadId);

        modelBuilder.Entity<Community>()
            .HasOne(c => c.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
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
