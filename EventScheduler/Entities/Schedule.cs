namespace EventScheduler.Entities;

public class Schedule : EntityBase
{
    private DateTimeOffset _scheduled;
    public DateTimeOffset Scheduled
    {
        get => _scheduled;
        set => OnPropertyChanged2(ref _scheduled, value);
    }
    private DateTimeOffset? _executed; 
    public DateTimeOffset? Executed
    {
        get => _executed;
        set => OnPropertyChanged2(ref _executed, value);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Schedule e) return false;
        return _scheduled == e._scheduled
            && _executed == e._executed;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
