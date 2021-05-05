namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Main
{
    using System.Collections.Generic;
    using System.Windows.Controls;
    using AppData.Interface;
    using Context.Messages;
    using Context.Options;
    using Enum;
    using Interface;
    using MaterialDesignExtensions.Model;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;

    public sealed class MainViewModel : ObservableObject
    {
        private readonly IAppSettings _appSettings;
        private readonly INavigationService _navigationService;
        private UserControl _module;

        public MainViewModel() { }

        public MainViewModel(IAppSettings appSettings, INavigationService navigationService)
        {
            _appSettings = appSettings;
            _navigationService = navigationService;
            ExitCommand = new RelayCommand(() => {
                WeakReferenceMessenger.Default.Send(new ConfirmExitMessage(new ConfirmExitOptions(host: "MainDialog",
                    close: true,
                    shutdown: true)));
            });
            NavigateTo = new RelayCommand<INavigationItem>(async i => {
                var commandType = await _navigationService.SelectProcess(item: i, selected: _module);
                switch (commandType)
                {
                    case NavigationServiceType.Module:
                        Module = _navigationService.NavigateTo;
                        break;
                    case NavigationServiceType.Command: {
                        switch (_navigationService.ExecuteCommand)
                        {
                            case NavigationCommandType.Exit:
                                ExitCommand.Execute(null);
                                Module = _navigationService.HiglightTarget;
                                break;
                            default: return;
                        }

                        break;
                    }
                    case NavigationServiceType.NotSet: return;
                    default:                           return;
                }
            });
        }

        public IEnumerable<INavigationItem> Navigation
        {
            get => _navigationService.Items;
        }

        public IGameSettings Game
        {
            get => _appSettings.Game;
        }

        public UserControl Module
        {
            get => _module;
            private set => SetProperty(field: ref _module, newValue: value);
        }

        public IRelayCommand ExitCommand { get; }

        public IRelayCommand<INavigationItem> NavigateTo { get; }
    }
}