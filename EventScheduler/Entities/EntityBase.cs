using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EventScheduler.Entities;

public class EntityBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(ref string? prop, string? value, [CallerMemberName] string? caller = null)
    {
        prop = value;
        if ((prop == default ^ value == default) || (prop?.Equals(value) ?? false))
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
    }

    protected void OnPropertyChanged2<T>(ref T prop, T value, [CallerMemberName] string? caller = null)
        where T : struct
    {
        prop = value;
        if (prop.Equals(value))
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
    }

    protected void OnPropertyChanged2<T>(ref T? prop, T? value, [CallerMemberName] string? caller = null)
        where T : struct
    {
        prop = value;
        if (prop.Equals(value))
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
    }

    protected void OnPropertyChanged<T>(ref T prop, T value, [CallerMemberName] string? caller = null)
        where T : class
    {
        prop = value;
        if ((prop == default ^ value == default) || (prop?.Equals(value) ?? false))
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
    }
}
