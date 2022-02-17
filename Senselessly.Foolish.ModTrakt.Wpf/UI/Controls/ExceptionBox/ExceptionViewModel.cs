namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Controls.ExceptionBox
{
    using System;
    using System.Windows;
    using Interfaces.App;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Models.Messaging.Messages.Exceptions;

    internal sealed class ExceptionViewModel : ObservableObject
    {
        private Action _action;
        private IExceptionInfo _exception;
        private Visibility _visible = Visibility.Collapsed;

        public ExceptionViewModel()
        {
            WeakReferenceMessenger.Default.Register<ExceptionRaisedMessage>(this,
                (r, m) => {
                    var e = m.Value;
                    Show = e != null ? Visibility.Visible : Visibility.Collapsed;
                    ExceptionDetail = e;
                    _action = e?.Close;
                });
            CloseCommand = new RelayCommand<EventArgs>(args => {
                Show = Visibility.Collapsed;
                _action?.Invoke();
            });
        }

        public Visibility Show
        {
            get => _visible;
            private set => SetProperty(ref _visible, value);
        }

        public IExceptionInfo ExceptionDetail
        {
            get => _exception;
            private set => SetProperty(ref _exception, value);
        }

        public IRelayCommand<EventArgs> CloseCommand { get; }
    }
}