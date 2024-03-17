using System.ComponentModel.DataAnnotations;

namespace TwoEventSchedule.Entity.Entities;

public class Job
{
    [Key]
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public Schedule Start { get; set; }
    public Schedule End { get; set; }
}
