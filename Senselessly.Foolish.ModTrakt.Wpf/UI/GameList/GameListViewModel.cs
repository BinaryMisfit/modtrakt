namespace Senselessly.Foolish.ModTrakt.Wpf.UI.GameList
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Settings;
    using Main;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Models.Messages;
    using Models.Messaging;

    internal sealed class GameListViewModel : ObservableObject
    {
        private readonly IAppSettings _settings;

        public GameListViewModel() { }

        public GameListViewModel(IAppSettings settings)
        {
            _settings = settings;
            CancelGameSelect = new RelayCommand(CancelCommand);
            GameSelected = new RelayCommand(GameSelectedCommand);
        }

        public IEnumerable<IGameSettings> Installed
        {
            get => _settings.Installed.OrderBy(g => g.Code);
        }

        public IGameSettings Active
        {
            get => _settings.Game;
            set => _settings.Game = value;
        }

        public RelayCommand CancelGameSelect { get; }

        public RelayCommand GameSelected { get; }

        private void CancelCommand()
        {
            WeakReferenceMessenger.Default.Send(new ConfirmExitMessage(new ConfirmExitOptions(host: "GameListDialog",
                close: true,
                shutdown: _settings.Missing)));
        }

        private void GameSelectedCommand()
        {
            if (_settings.Game == null) { return; }

            WeakReferenceMessenger.Default.Send(new ShowWindowMessage(
                new ShowWindowOptions(caller: typeof(GameListWindow), window: typeof(MainWindow), closeCaller: true)));
        }
    }
}