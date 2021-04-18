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
            _status = Resources.Status_StartingUp;
            CheckStartupOptions(_appSettings);
        }

        private void CheckStartupOptions(IAppSettings settings)
        {
            if (settings.Settings == null || string.IsNullOrEmpty(settings.Settings.ActiveGame))
            {
                LocateSupportedGames();
            }
        }

        private void LocateSupportedGames()
        {
            _status = Resources.Status_LocatingGames;
            _gameLocator.Load(Config.GameDictionaryKey);
        }
    }
}