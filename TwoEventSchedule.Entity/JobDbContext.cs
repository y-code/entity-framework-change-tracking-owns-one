using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TwoEventSchedule.Entity.Entities;

namespace TwoEventSchedule.Entity;

public class JobDbContext : DbContext
{
    DbSet<Job>? Jobs;

	public JobDbContext(DbContextOptions<JobDbContext> options) : base(options)
	{
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(e =>
        {
            e.ComplexProperty(p => p.Start);
            e.ComplexProperty(p => p.End);
        });
    }
}

public class JobDbContextFactory : IDesignTimeDbContextFactory<JobDbContext>
{
    public JobDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<JobDbContext>();
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=job-db;User Id=postgres;Password=job-db;");
        return new JobDbContext(optionsBuilder.Options);
    }
}
