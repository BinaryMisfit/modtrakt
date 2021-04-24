namespace Senselessly.Foolish.Bethesda.Wpf.UI.Splash
{
    using System.Threading.Tasks;
    using AppData.Default;
    using AppData.Interface;
    using Context.Interface;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using Properties;

    public class SplashWindowViewModel : ObservableObject
    {
        private readonly IAppSettings _appSettings;
        private readonly IGameLocatorService _gameLocatorService;
        private string _status;

        public SplashWindowViewModel()
        {
        }

        public SplashWindowViewModel(IAppSettings appSettings, IGameLocatorService gameLocatorService)
        {
            _appSettings = appSettings;
            _gameLocatorService = gameLocatorService;
            ContentRenderedAsync = new AsyncRelayCommand(StartAsync);
        }

        public string Status
        {
            get =>
                _status;
            private set =>
                SetProperty(field: ref _status, newValue: value);
        }

        public IAsyncRelayCommand ContentRenderedAsync { get; }

        private async Task StartAsync()
        {
            Status = Resources.Splash_Status_Starting_Up;
            await CheckStartupOptions(_appSettings);
        }

        private async Task CheckStartupOptions(IAppSettings settings)
        {
            if (settings.Missing)
            {
                await LocateSupportedGames();
            }
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

            _gameLocatorService.Progress = (s, e) =>
            {
                Status = string.Format(format: Resources.Splash_Status_Game_Progress,
                    arg0: e.Current,
                    arg1: e.Remaining,
                    arg2: e.Game);
            };
            var gamesFound = _gameLocatorService.Locate();
            Status = string.Format(format: Resources.Splash_Status_Games_Found, arg0: gamesFound);
        }
    }
}