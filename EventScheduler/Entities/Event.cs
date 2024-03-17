namespace EventScheduler.Entities;

public class Event : EntityBase
{
    public Guid Id { get; set; }
    private string? _description;
    public string? Description
    {
        get => _description;
        set => OnPropertyChanged(ref _description, value);
    }
    private Schedule _start = new();
    public Schedule Start
    {
        get => _start;
        set => OnPropertyChanged(ref _start, value);
    }
    private Schedule _end = new();
    public Schedule End
    {
        get => _end;
        set => OnPropertyChanged(ref _end, value);
    }
}
