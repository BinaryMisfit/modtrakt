namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Shared
{
    using System.Windows;
    using Dialog.Exit;
    using Interfaces.Navigation;
    using MaterialDesignExtensions.Controls;
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Models.Messages;
    using Models.Messaging;

    internal abstract class ExtendedWindow : MaterialWindow
    {
        protected ExtendedWindow()
        {
            var canClose = false;
            Loaded += (s, e) => {
                WeakReferenceMessenger.Default.Register<ShowWindowMessage>(recipient: this,
                    handler: (r, m) => {
                        var options = m.Value;
                        if (options.Handled) { return; }

                        if (options.Caller != r.GetType()) { return; }

                        options.Handled = true;
                        var navigationService = Ioc.Default.GetService<INavigationService>();
                        var window = navigationService?.CreateWindow(options.Window);
                        window?.Show();

                        if (options.CloseCaller)
                        {
                            WeakReferenceMessenger.Default.Send(new WindowCloseMessage(new WindowCloseOptions(
                                source: r.GetType(),
                                close: true,
                                shutdown: false)));
                        }
                    });
                WeakReferenceMessenger.Default.Register<ConfirmExitMessage>(recipient: this,
                    handler: async (r, m) => {
                        var options = m.Value;
                        if (options.Handled) { return; }

                        options.Handled = true;
                        if (options.Shutdown && !string.IsNullOrEmpty(options.Host))
                        {
                            await ExitDialog.PromptAsync(type: r.GetType(),
                                host: options.Host,
                                cancel: options.CancelAction);
                        }
                        else
                        {
                            WeakReferenceMessenger.Default.Send(new WindowCloseMessage(new WindowCloseOptions(
                                source: r.GetType(),
                                close: options.Close,
                                shutdown: options.Shutdown)));
                        }
                    });
                WeakReferenceMessenger.Default.Register<WindowCloseMessage>(recipient: this,
                    handler: (r, m) => {
                        var options = m.Value;
                        if (options.Handled) { return; }

                        if (options.Source != r.GetType()) { return; }

                        options.Handled = true;
                        canClose = options.Close;
                        if (options.Close) { Close(); }

                        if (options.Shutdown) { Application.Current.Shutdown(); }

                        WeakReferenceMessenger.Default.Unregister<WindowCloseMessage>(this);
                    });
                WeakReferenceMessenger.Default.Register<ExceptionRaisedMessage>(recipient: this,
                    handler: (r, m) => {
                        var ex = m.Value;
                        if (ex.Handled) { return; }

                        ex.Handled = true;
                    });
            };
            Closing += (s, e) => { e.Cancel = !canClose; };
            Closed += (s, e) => {
                WeakReferenceMessenger.Default.Unregister<ShowWindowMessage>(this);
                WeakReferenceMessenger.Default.Unregister<WindowCloseMessage>(this);
                WeakReferenceMessenger.Default.Unregister<ConfirmExitMessage>(this);
                WeakReferenceMessenger.Default.Unregister<ExceptionRaisedMessage>(this);
            };
        }
    }
}