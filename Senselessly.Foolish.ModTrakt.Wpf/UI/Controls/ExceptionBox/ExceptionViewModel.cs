namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Controls.ExceptionBox
{
    using System.Windows;
    using Interfaces.App;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Models.Messaging.Messages.Exceptions;

    internal sealed class ExceptionViewModel : ObservableObject
    {
        private IExceptionInfo _exception;
        private Visibility _visible = Visibility.Collapsed;

        public ExceptionViewModel()
        {
            WeakReferenceMessenger.Default.Register<ExceptionRaisedMessage>(recipient: this,
                handler: (r, m) => {
                    var e = m.Value;
                    Show = e != null ? Visibility.Visible : Visibility.Collapsed;
                    ExceptionDetail = e;
                });
        }

        public Visibility Show
        {
            get => _visible;
            private set => SetProperty(field: ref _visible, newValue: value);
        }

        public IExceptionInfo ExceptionDetail
        {
            get => _exception;
            private set => SetProperty(field: ref _exception, newValue: value);
        }
    }
}