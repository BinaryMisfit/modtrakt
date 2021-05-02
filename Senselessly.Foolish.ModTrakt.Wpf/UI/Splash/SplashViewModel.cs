namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Splash
{
    using System.Threading;
    using System.Threading.Tasks;
    using AppData.Default;
    using AppData.Interface;
    using Context.Interface;
    using Context.Models;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Properties;

    public class SplashViewModel : ObservableObject
    {
        private readonly IAppSettings _appSettings;
        private readonly CancellationTokenSource _cancelTask;
        private readonly IGameLocatorService _gameLocatorService;
        private string _status;

        public SplashViewModel() { }

        public SplashViewModel(IAppSettings appSettings, IGameLocatorService gameLocatorService)
        {
            _appSettings = appSettings;
            _gameLocatorService = gameLocatorService;
            _cancelTask = new CancellationTokenSource();
            ContentRenderedAsync = new AsyncRelayCommand(StartAsync);
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
            set => SetProperty(field: ref _status, newValue: value);
        }

        public IAsyncRelayCommand ContentRenderedAsync { get; }

        private async Task StartAsync()
        {
            Status = Resources.Splash_Status_Starting_Up;
            await CheckStartupOptions(_appSettings);
        }

        private async Task CheckStartupOptions(IAppSettings settings)
        {
            if (settings.Missing) { await LocateSupportedGames(); }
        }

        private async Task LocateSupportedGames()
        {
            Status = Resources.Splash_Status_Games_Locating;
            var dictionaryLoaded = await _gameLocatorService.LoadAsync(Config.GameDictionaryKey);
            if (dictionaryLoaded == 0)
            {
                Status = Resources.Game_Dictionary_Empty;
                return;
            }

            _gameLocatorService.Progress = (s, e) => {
                Status = string.Format(format: Resources.Splash_Status_Game_Progress,
                                       arg0: e.Current,
                                       arg1: e.Remaining,
                                       arg2: e.Game);
            };
            var gamesFound = await _gameLocatorService.Locate(_cancelTask.Token);
            if (!_cancelTask.IsCancellationRequested)
            {
                Status = string.Format(format: Resources.Splash_Status_Games_Found, arg0: gamesFound);
            }
        }
    }
}