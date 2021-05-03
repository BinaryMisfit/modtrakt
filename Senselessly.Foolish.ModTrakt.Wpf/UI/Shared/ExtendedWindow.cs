namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Shared
{
    using System.Windows;
    using Context.Messages;
    using Context.Options;
    using Dialog.Exit;
    using MaterialDesignExtensions.Controls;
    using Microsoft.Toolkit.Mvvm.Messaging;

    public class ExtendedWindow : MaterialWindow
    {
        protected ExtendedWindow()
        {
            Loaded += (s, e) => {
                WeakReferenceMessenger.Default.Register<ConfirmExitMessage>(recipient: this,
                    handler: async (r, m) => {
                        var options = m.Value;
                        if (options.Handled) { return; }

                        options.Handled = true;
                        if (options.Shutdown && !string.IsNullOrEmpty(options.Host))
                        {
                            await ExitDialog.Prompt(type: r.GetType(), host: options.Host);
                        }
                        else
                        {
                            WeakReferenceMessenger.Default.Send(new WindowCloseMessage(new WindowClose(
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
                        if (options.Close) { Close(); }

                        if (options.Shutdown) { Application.Current.Shutdown(); }

                        WeakReferenceMessenger.Default.Unregister<WindowCloseMessage>(this);
                    });
            };
            Closed += (s, e) => {
                WeakReferenceMessenger.Default.Unregister<WindowCloseMessage>(this);
                WeakReferenceMessenger.Default.Unregister<ConfirmExitMessage>(this);
            };
        }
    }
}