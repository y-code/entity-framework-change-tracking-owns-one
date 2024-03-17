using System.Reflection;
using EventScheduler;
using EventScheduler.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Xunit.Abstractions;

namespace EventSchedulerTests;

public class UnitTest1
{
    private readonly ITestOutputHelper _output;

    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task Test1()
    {
        var id = Guid.NewGuid();

        using (var context = new JobDbContextFactory().CreateDbContext(Array.Empty<string>()))
        {
            var events = context.Set<Event>();

            var newEvent = new Event
            {
                Id = id,
                Description = "TEST",
                Start =
            {
                Scheduled = DateTimeOffset.Parse("2024-03-24T13:00:00Z"),
                Executed = null,
            },
                End =
            {
                Scheduled = DateTimeOffset.Parse("2024-03-24T14:00:00Z"),
                Executed = null,
            },
            };

            await events.AddAsync(newEvent);

            context.SaveChanges();
        }

        using (var context = new JobDbContextFactory().CreateDbContext(Array.Empty<string>()))
        {
            context.ChangeTracker.DetectedAllChanges += (object? sender, Microsoft.EntityFrameworkCore.ChangeTracking.DetectedChangesEventArgs e) =>
            {
                foreach (var entry in context.ChangeTracker.Entries())
                {
                    if (entry.State != Microsoft.EntityFrameworkCore.EntityState.Modified)
                        continue;

                    _output.WriteLine($"ENTRY: {entry.DebugView.ShortView}");

                    if (TryGetOwner(entry, out var owner))
                    {
                        foreach (var prop in entry.Properties)
                        {
                            if (!prop.IsModified)
                                continue;

                            var ownerProp = prop.Metadata.DeclaringEntityType.Name.Split('#', 2).First().Split('.').Last();
                            _output.WriteLine($"  PROPERTY: {owner.type.Name}[{owner.key}] {ownerProp} {prop.Metadata.Name} ({prop.OriginalValue} => {prop.CurrentValue})");
                        }
                    }
                    else
                    {
                        foreach (var prop in entry.Properties)
                        {
                            if (!prop.IsModified)
                                continue;

                            _output.WriteLine($"{(prop.IsModified ? "[M] " : "    ")}PROPERTY: {prop.Metadata.Name} ({prop.OriginalValue} => {prop.CurrentValue})");
                        }
                    }
                }
            };

            var existingEvent = context.Set<Event>().Single(x => x.Id == id);

            existingEvent.Start.Executed = DateTimeOffset.Parse("2024-03-24T11:00:00Z");
            existingEvent.End.Scheduled = DateTimeOffset.Parse("2024-03-24T15:00:00Z");

            await context.SaveChangesAsync();
        }
    }

    private bool TryGetOwner(EntityEntry entry, out (Type type, string keyPropName, object? key) owner)
    {
        foreach (var prop in entry.Properties)
        {
            if (!prop.Metadata.IsForeignKey())
                continue;

            var foreignKey = prop.Metadata.GetContainingForeignKeys().First();
            owner = (foreignKey.PrincipalEntityType.ClrType,
                foreignKey.PrincipalKey.Properties.First().Name,
                prop.CurrentValue);
            return true;
        }

        owner = default;
        return false;
    }
}