using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestApp;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected bool SetValue<TValue>(ref TValue oldValue, TValue newValue, [CallerMemberName] string propertyName = null)
    {
        var equalityComparer = EqualityComparer<TValue>.Default;

        if (equalityComparer.Equals(oldValue, newValue))
        {
            return false;
        }

        oldValue = newValue;

        this.OnPropertyChanged(propertyName);

        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
