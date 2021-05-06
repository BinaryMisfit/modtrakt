namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Dialog.Exit
{
    using System;
    using System.Threading.Tasks;
    using MaterialDesignThemes.Wpf;
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Models.Messaging;

    public partial class ExitDialog
    {
        public ExitDialog()
        {
            InitializeComponent();
        }

        public static async Task PromptAsync(Type type, string host, Action cancel = null)
        {
            var exit = Ioc.Default.GetService<ExitDialog>();
            if (exit == null) return;

            var result = await DialogHost.Show(content: exit, dialogIdentifier: host);
            bool.TryParse(value: result?.ToString(), result: out var shutdown);
            if (!shutdown)
            {
                cancel?.Invoke();
                return;
            }

            WeakReferenceMessenger.Default.Send(
                new WindowCloseMessage(new WindowCloseOptions(source: type, close: true, shutdown: true)));
        }
    }
}