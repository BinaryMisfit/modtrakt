namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Splash
{
    using System;
    using System.Threading.Tasks;
    using Extensions.Storage;
    using GameList;
    using Interfaces.Services;
    using Interfaces.Settings;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Models.App;
    using Models.Messaging.Messages.Settings;
    using Models.Messaging.Messages.UI;
    using Models.Messaging.Options.UI;
    using Properties;

    internal sealed class SplashViewModel : ObservableObject
    {
        private readonly IConfiguratorService _configurator;
        private IAppSettings _settings;
        private string _status;

        public SplashViewModel() { }

        public SplashViewModel(IAppSettings settings, IConfiguratorService configurator)
        {
            _configurator = configurator;
            _settings = settings;
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
            await CheckConfiguration(_settings);
        }

        private async Task CheckConfiguration(IAppSettings settings)
        {
            if (settings.FirstRun)
            {
                WeakReferenceMessenger.Default.Register<AppSettingsMessage>(recipient: this,
                    handler: (s, e) => {
                        var m = e.Value;
                        if (m.Handled) return;

                        m.Handled = true;
                        var config = m.Settings;
                        if (config == null) return;

                        config.SaveConfigFile(ConfigKeys.FileAppConfig);
                        settings = settings.LoadConfigFile(ConfigKeys.FileAppConfig);
                        if (settings.IsConfigured) _settings = settings;
                    });
                await _configurator.LoadAsync(key: ConfigKeys.JsonConfig);
            }

            await _configurator.CheckFolders(settings.Folders);
            if (settings.IsConfigured) ShowGameSelector();
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