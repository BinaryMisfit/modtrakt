namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Dialog.Exit
{
    using System;
    using System.Threading.Tasks;
    using Context.Messages;
    using Context.Options;
    using MaterialDesignThemes.Wpf;
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.Messaging;

    public partial class ExitDialog
    {
        public ExitDialog()
        {
            InitializeComponent();
        }

        public static async Task PromptAsync(Type type, string host)
        {
            var exit = Ioc.Default.GetService<ExitDialog>();
            if (exit == null) { return; }

            var result = await DialogHost.Show(content: exit, dialogIdentifier: host);
            bool.TryParse(value: result?.ToString(), result: out var shutdown);
            if (!shutdown) { return; }

            WeakReferenceMessenger.Default.Send(
                new WindowCloseMessage(new WindowCloseOptions(source: type, close: true, shutdown: true)));
        }
    }
}