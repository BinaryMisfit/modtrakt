namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Splash
{
    using System;
    using System.IO.Abstractions;
    using System.Threading.Tasks;
    using GameList;
    using Interfaces.Services;
    using Interfaces.Settings;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Models.App;
    using Models.Messaging;
    using Properties;

    internal sealed class SplashViewModel : ObservableObject
    {
        private readonly IConfiguratorService _configurator;
        private readonly IFileSystem _storage;
        private IAppSettings _settings;
        private string _status;

        public SplashViewModel() { }

        public SplashViewModel(IAppSettings settings, IConfiguratorService configurator, IFileSystem storage)
        {
            _configurator = configurator;
            _settings = settings;
            _storage = storage;
            ContentRenderedAsync = new AsyncRelayCommand<EventArgs>(StartAsync);
            Status = Resources.Splash_Status_Starting_Up;
            WeakReferenceMessenger.Default.Register<StatusUpdateMessage>(recipient: this,
                handler: (r, m) => {
                    var status = m.Value;
                    if (status.Clear) Status = null;

                    Status = status.Message;
                });
        }

        public string Status
        {
            get => _status;
            private set => SetProperty(field: ref _status, newValue: value);
        }

        public IAsyncRelayCommand<EventArgs> ContentRenderedAsync { get; }

        private async Task StartAsync(EventArgs e)
        {
            _settings = await CheckStartupOptions(_settings);
        }

        private async Task<IAppSettings> CheckStartupOptions(IAppSettings settings)
        {
            var configured = await _configurator.Load(key: ConfigKeys.JsonConfig);
            if (settings.CompareTo(configured) != 0) { }

            return settings;
        }

        private static void ShowGameSelector()
        {
            WeakReferenceMessenger.Default.Send(new ShowWindowMessage(
                new ShowWindowOptions(caller: typeof(SplashWindow),
                    window: typeof(GameListWindow),
                    closeCaller: true)));
        }
    }
}