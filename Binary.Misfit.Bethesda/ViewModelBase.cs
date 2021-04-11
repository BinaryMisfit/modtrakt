namespace Binary.Misfit.Bethesda
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
 
    protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName]string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(x: field, y: newValue))
        {
            return false;
        }

        field = newValue;
        PropertyChanged?.Invoke(sender: this, e: new PropertyChangedEventArgs(propertyName));
        return true;
    }
}
}