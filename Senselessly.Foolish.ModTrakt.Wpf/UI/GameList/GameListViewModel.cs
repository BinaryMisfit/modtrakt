namespace Senselessly.Foolish.ModTrakt.Wpf.UI.GameList
{
    using System.Collections.Generic;
    using System.Linq;
    using AppData.Interface;
    using Context.Models;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;

    public class GameListViewModel : ObservableObject
    {
        private readonly IAppSettings _settings;

        public GameListViewModel() { }

        public GameListViewModel(IAppSettings settings)
        {
            _settings = settings;
            CancelGameSelect = new RelayCommand(CancelCommand);
        }

        public IEnumerable<IGameSettings> Installed
        {
            get => _settings.Installed.OrderBy(g => g.Code);
        }

        public RelayCommand CancelGameSelect { get; }

        private void CancelCommand()
        {
            WeakReferenceMessenger.Default.Send(new WindowCloseMessage(
                new WindowCloseOptions(source: typeof(GameListViewModel), close: true, shutdown: _settings.Missing)));
        }
    }
}