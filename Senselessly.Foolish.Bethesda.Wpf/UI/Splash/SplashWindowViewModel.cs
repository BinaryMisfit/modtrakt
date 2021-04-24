namespace Senselessly.Foolish.Bethesda.Wpf.UI.Splash
{
    using AppData.Default;
    using AppData.Interface;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Properties;
    using Services.Interface;

    public class SplashWindowViewModel : ObservableObject
    {
        private readonly IAppSettings _appSettings;
        private readonly IGameLocator _gameLocator;
        private string _status;

        public SplashWindowViewModel(IAppSettings appSettings, IGameLocator gameLocator)
        {
            _appSettings = appSettings;
            _gameLocator = gameLocator;
            Start();
        }

        public string Status
        {
            get =>
                _status;
            set =>
                SetProperty(field: ref _status, newValue: value);
        }

        private void Start()
        {
            _status = Resources.Splash_Status_Starting_Up;
            CheckStartupOptions(_appSettings);
        }

        private void CheckStartupOptions(IAppSettings settings)
        {
            if (settings.Missing)
            {
                LocateSupportedGames();
            }
        }

        private void LocateSupportedGames()
        {
            _status = Resources.Splash_Status_Games_Locating;
            var dictionaryLoaded = _gameLocator.Load(Config.GameDictionaryKey);
            if (dictionaryLoaded == 0)
            {
                _status = Resources.Game_Dictionary_Empty;
                return;
            }

            _gameLocator.Progress = (s, e) =>
            {
                _status = string.Format(format: Resources.Splash_Status_Game_Progress,
                    arg0: e.Current,
                    arg1: e.Remaining,
                    arg2: e.Game);
            };
            var gamesFound = _gameLocator.Locate();
            _status = string.Format(format: Resources.Splash_Status_Games_Found, arg0: gamesFound);
        }
    }
}