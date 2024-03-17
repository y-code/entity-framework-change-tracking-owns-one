using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using EventScheduler.Entities;

namespace EventScheduler;

public class EventDbContext : DbContext
{
	public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
	{
        ChangeTracker.DetectedAllChanges += ChangeTracker_DetectedAllChanges;
	}

    private void ChangeTracker_DetectedAllChanges(object? sender, Microsoft.EntityFrameworkCore.ChangeTracking.DetectedChangesEventArgs e)
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            Console.WriteLine($"ENTRY {entry.DebugView.ShortView}");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(e =>
        {
            e.OwnsOne(p => p.Start).HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangedNotifications);
            e.OwnsOne(p => p.End).HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangedNotifications);
        });
    }
}

public class JobDbContextFactory : IDesignTimeDbContextFactory<EventDbContext>
{
    public EventDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EventDbContext>();
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=job-db-2;User Id=postgres;Password=job-db;");
        return new EventDbContext(optionsBuilder.Options);
    }
}
