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
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Module.ModList;
    using Navigation;

    public sealed class MainViewModel : ObservableObject
    {
        private readonly IAppSettings _appSettings;
        private readonly INavigationService _navigationService;

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
            NavigateTo = new RelayCommand<INavigationItem>(i => {
                INavigationServiceItem current = null;
                if (i is FirstLevelNavigationItem item) { current = _navigationService.FindItem(item.Label); }

                if (current == null) { return; }

                switch (current.Type)
                {
                    case NavigationServiceType.Module: {
                        var type = ((NavigationServiceModule)current).Module;
                        if (type == typeof(ModListModule)) { Module = Ioc.Default.GetService<ModListModule>(); }

                        break;
                    }
                    case NavigationServiceType.Command: {
                        var command = ((INavigationServiceCommand)current).CommandType;
                        switch (command)
                        {
                            case NavigationCommandType.Exit:
                                ExitCommand.Execute(null);
                                break;
                            default: return;
                        }

                        break;
                    }
                    default: return;
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

        public UserControl Module { get; set; }

        public IRelayCommand ExitCommand { get; }

        public IRelayCommand<INavigationItem> NavigateTo { get; }
    }
}