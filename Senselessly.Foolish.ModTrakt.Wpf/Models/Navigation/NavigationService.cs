namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using Enums;
    using Interfaces.Navigation;
    using MaterialDesignExtensions.Model;
    using MaterialDesignThemes.Wpf;
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
    using Properties;
    using UI.GameList;
    using UI.Main;
    using UI.Modules.ModList;
    using UI.Modules.PluginList;
    using UI.Shared;

    internal sealed class NavigationService : INavigationService
    {
        private readonly IEnumerable<INavigationServiceItem> _modules;

        public NavigationService()
        {
            var items = new[] {
                new FirstLevelNavigationItem {
                    Label = Resources.Navigation_Mods, Icon = PackIconKind.ViewModule, IsSelected = true
                },
                new FirstLevelNavigationItem {
                    Label = Resources.Navigation_Plugins, Icon = PackIconKind.ToyBrick, IsSelected = true
                },
                new FirstLevelNavigationItem {Label = Resources.Navigation_Exit, Icon = PackIconKind.ExitToApp}
            };
            var modules = new INavigationServiceItem[] {
                new NavigationServiceModule {Label = Resources.Navigation_Mods, Module = typeof(ModListModule)},
                new NavigationServiceModule {Label = Resources.Navigation_Plugins, Module = typeof(PluginListModule)},
                new NavigationServiceCommand {
                    Label = Resources.Navigation_Exit, CommandType = NavigationCommandType.Exit
                }
            };
            Items = items;
            _modules = modules;
        }

        public ExtendedWindow CreateWindow(Type type)
        {
            if (type == typeof(GameListWindow)) { return Ioc.Default.GetService<GameListWindow>(); }

            if (type == typeof(MainWindow)) { return Ioc.Default.GetService<MainWindow>(); }

            return null;
        }

        public IEnumerable<INavigationItem> Items { get; }

        public INavigationItem HighlightTarget { get; private set; }

        public UserControl NavigateTo { get; private set; }

        public NavigationCommandType ExecuteCommand { get; private set; }

        public void Reset()
        {
            NavigateTo = null;
            HighlightTarget = null;
            ExecuteCommand = NavigationCommandType.NotSet;
        }

        public async Task<NavigationServiceType> SelectProcess(INavigationItem i, UserControl selected)
        {
            INavigationServiceItem current = null;
            if (i is FirstLevelNavigationItem item) { current = await FindItem(item.Label); }

            if (current == null) { return NavigationServiceType.NotSet; }

            switch (current.Type)
            {
                case NavigationServiceType.NotSet: { return NavigationServiceType.NotSet; }
                case NavigationServiceType.Module: {
                    var type = ((NavigationServiceModule)current).Module;
                    if (type == typeof(ModListModule)) { NavigateTo = Ioc.Default.GetService<ModListModule>(); }

                    if (type == typeof(PluginListModule)) { NavigateTo = Ioc.Default.GetService<PluginListModule>(); }

                    return NavigationServiceType.Module;
                }
                case NavigationServiceType.Command: {
                    i.IsSelected = false;
                    HighlightTarget = await FindNavigation(selected);
                    var command = ((INavigationServiceCommand)current).CommandType;
                    ExecuteCommand = command;
                    return NavigationServiceType.Command;
                }
                default: return NavigationServiceType.NotSet;
            }
        }

        private ValueTask<INavigationServiceItem> FindItem(string label)
        {
            return _modules.ToAsyncEnumerable().FirstOrDefaultAsync(module => module.Label == label);
        }

        private async ValueTask<INavigationItem> FindNavigation(UserControl control)
        {
            if (control == null) { return null; }

            await foreach (var item in Items.ToAsyncEnumerable())
            {
                if (!(item is FirstLevelNavigationItem navItem)) { return null; }

                var navModule = await _modules.ToAsyncEnumerable()
                                              .FirstOrDefaultAsync(module => module.Label == navItem.Label);
                if (!(navModule is INavigationServiceModule)) { return null; }

                var found = await _modules.ToAsyncEnumerable()
                                          .FirstOrDefaultAsync(module => module.Type == navModule.Type);
                return found == null ? null : item;
            }

            return null;
        }
    }
}