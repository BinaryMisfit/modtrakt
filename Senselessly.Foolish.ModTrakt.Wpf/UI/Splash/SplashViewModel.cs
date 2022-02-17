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
            WeakReferenceMessenger.Default.Register<StatusUpdateMessage>(this,
                (r, m) => {
                    var status = m.Value;
                    if (status.Clear) Status = null;

                    Status = status.Message;
                });
        }

        public string Status
        {
            get => _status;
            private set => SetProperty(ref _status, value);
        }

        public IAsyncRelayCommand<EventArgs> ContentRenderedAsync { get; }

        private async Task StartAsync(EventArgs e)
        {
            await CheckConfigurationAsync(_settings);
        }

        private async Task CheckConfigurationAsync(IAppSettings settings)
        {
            switch (settings.FirstRun)
            {
                case false:
                    await CheckFoldersAsync(settings);
                    break;
                case true:
                    WeakReferenceMessenger.Default.Register<AppSettingsMessage>(this,
                        async (s, e) => {
                            var m = e.Value;
                            if (m.Handled) return;

                            m.Handled = true;
                            var config = m.Settings;
                            if (config == null) return;

                            config.SaveConfigFile(ConfigKeys.FileAppConfig);
                            settings = settings.LoadConfigFile(ConfigKeys.FileAppConfig);
                            if (!settings.IsConfigured) return;

                            _settings = settings;
                            WeakReferenceMessenger.Default.Unregister<AppSettingsMessage>(this);
                            await CheckFoldersAsync(settings);
                        });
                    await _configurator.LoadAsync(ConfigKeys.JsonConfig);
                    break;
            }
        }

        private async Task CheckFoldersAsync(IAppSettings settings)
        {
            var identifier = Guid.NewGuid();
            WeakReferenceMessenger.Default.Register<TaskCompletionMessage>(this,
                (s, e) => {
                    var m = e.Value;
                    if (identifier == Guid.Empty) return;

                    if (m.Identifier != identifier) return;

                    if (!m.IsSuccessful) return;

                    if (!settings.IsConfigured) return;

                    WeakReferenceMessenger.Default.Unregister<TaskCompletionMessage>(this);
                    ShowGameSelector();
                });
            await _configurator.CheckFoldersAsync(identifier, settings.Folders);
        }

        private static void ShowGameSelector()
        {
            WeakReferenceMessenger.Default.Send(new ShowWindowMessage(
                new ShowWindowOptions(typeof(SplashWindow),
                    typeof(GameListWindow),
                    true)));
        }
    }
}