namespace Senselessly.Foolish.ModTrakt.Wpf.UI.GameList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Interfaces.Services;
    using Interfaces.Settings;
    using Main;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Models.App;
    using Models.Messaging;

    internal sealed class GameListViewModel : ObservableObject
    {
        private readonly CancellationTokenSource _cancelTask;
        private readonly IGameLocatorService _gameLocatorService;
        private readonly IAppSettings _settings;
        private IEnumerable<IGameSettings> _installed;
        private bool _isBusy;

        public GameListViewModel() { }

        public GameListViewModel(IAppSettings settings, IGameLocatorService gameLocatorService)
        {
            _cancelTask = new CancellationTokenSource();
            _settings = settings;
            _gameLocatorService = gameLocatorService;
            CancelGameSelect = new RelayCommand(CancelCommand);
            ContentRenderedAsync = new AsyncRelayCommand<EventArgs>(StartAsync);
            GameSelected = new RelayCommand(GameSelectedCommand);
        }

        public string Active
        {
            get => _settings.General.ActiveGame;
            set => _settings.General.ActiveGame = value;
        }

        public RelayCommand CancelGameSelect { get; }

        public IAsyncRelayCommand<EventArgs> ContentRenderedAsync { get; }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(field: ref _isBusy, newValue: value);
        }

        public IEnumerable<IGameSettings> Installed
        {
            get => _installed.OrderBy(g => g.Code);
            private set => SetProperty(field: ref _installed, newValue: value);
        }

        public RelayCommand GameSelected { get; }

        private void CancelCommand()
        {
            WeakReferenceMessenger.Default.Send(new ConfirmExitMessage(new ConfirmExitOptions(host: "GameListDialog",
                close: true,
                shutdown: _settings.General.ActiveGame == null)));
        }

        private async Task<IEnumerable<IGameSettings>> LocateSupportedGames()
        {
            var dictionaryLoaded = await _gameLocatorService.LoadAsync(ConfigKeys.JsonGames);
            if (dictionaryLoaded == 0) return null;

            var gamesFound = await _gameLocatorService.Locate(_cancelTask.Token);
            if (!_cancelTask.IsCancellationRequested && gamesFound > 0) return _gameLocatorService.Found;

            return null;
        }

        private void GameSelectedCommand()
        {
            if (_settings.General.ActiveGame == null) return;

            WeakReferenceMessenger.Default.Send(new ShowWindowMessage(
                new ShowWindowOptions(caller: typeof(GameListWindow), window: typeof(MainWindow), closeCaller: true)));
        }

        private async Task StartAsync(EventArgs e)
        {
            IsBusy = true;
            Installed = await LocateSupportedGames();
            IsBusy = false;
        }
    }
}