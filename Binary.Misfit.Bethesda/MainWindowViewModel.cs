namespace Binary.Misfit.Bethesda
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Annotations;

    public sealed class MainWindowViewModel : INotifyPropertyChanged
    {
        public string Status { get; set; }

        public string Summary { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(sender: this, e: new PropertyChangedEventArgs(propertyName));
        }
    }
}