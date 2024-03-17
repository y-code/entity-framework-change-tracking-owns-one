namespace TwoEventSchedule.Entity.Entities;

public class Schedule
{
    public DateTimeOffset Scheduled { get; set; }
    public DateTimeOffset? Executed { get; set; }
}
