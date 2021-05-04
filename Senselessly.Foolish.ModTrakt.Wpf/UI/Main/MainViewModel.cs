namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Main
{
    using System.Collections.Generic;
    using AppData.Interface;
    using Context.Messages;
    using Context.Options;
    using MaterialDesignExtensions.Model;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;

    public sealed class MainViewModel : ObservableObject
    {
        private readonly IAppSettings _appSettings;
        private readonly INavigation _navigation;

        public MainViewModel() { }

        public MainViewModel(IAppSettings appSettings, INavigation navigation)
        {
            _appSettings = appSettings;
            _navigation = navigation;
            ExitCommand = new RelayCommand(execute: Exit);
        }

        public IEnumerable<INavigationItem> Navigation
        {
            get => _navigation.Items;
        }

        public IGameSettings Game
        {
            get => _appSettings.Game;
        }

        public IRelayCommand ExitCommand { get; }

        private static void Exit()
        {
            WeakReferenceMessenger.Default.Send(new ConfirmExitMessage(new ConfirmExitOptions(host: "MainDialog",
                close: true,
                shutdown: true)));
        }
    }
}