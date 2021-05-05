namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Splash
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
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
        private readonly IAppSettings _appSettings;
        private readonly CancellationTokenSource _cancelTask;
        private readonly IConfigurator _configurator;
        private readonly IGameLocatorService _gameLocatorService;
        private string _status;

        public SplashViewModel() { }

        public SplashViewModel(
            IAppSettings appSettings,
            IConfigurator configurator,
            IGameLocatorService gameLocatorService)
        {
            _appSettings = appSettings;
            _configurator = configurator;
            _gameLocatorService = gameLocatorService;
            _cancelTask = new CancellationTokenSource();
            ContentRenderedAsync = new AsyncRelayCommand<EventArgs>(StartAsync);
            Status = Resources.Splash_Status_Starting_Up;
            WeakReferenceMessenger.Default.Register<StatusUpdateMessage>(recipient: this,
                handler: (r, m) => {
                    var status = m.Value;
                    if (status.Clear) { Status = null; }

                    Status = status.Message;
                });
            WeakReferenceMessenger.Default.Register<ExceptionRaisedMessage>(recipient: this,
                handler: (r, m) => {
                    var e = m.Value;
                    Status = e.Exception.Message;
                    _cancelTask.Cancel();
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
            await CheckStartupOptions(_appSettings);
        }

        private async Task CheckStartupOptions(IAppSettings settings)
        {
            settings.Installed = await LocateSupportedGames();
            _configurator.Check(ConfigKeys.JSON_CONFIG);
        }

        private async Task<IEnumerable<IGameSettings>> LocateSupportedGames()
        {
            var dictionaryLoaded = await _gameLocatorService.LoadAsync(ConfigKeys.JSON_GAMES);
            if (dictionaryLoaded == 0) { return null; }

            var gamesFound = await _gameLocatorService.Locate(_cancelTask.Token);
            if (!_cancelTask.IsCancellationRequested && gamesFound > 0) { return _gameLocatorService.Found; }

            return null;
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