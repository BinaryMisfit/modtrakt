namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Navigation
{
    using System.Collections.Generic;
    using Enum;
    using Interface;
    using MaterialDesignExtensions.Model;
    using MaterialDesignThemes.Wpf;
    using Module.ModList;
    using Properties;

    public class NavigationService : INavigationService
    {
        public NavigationService()
        {
            var items = new[] {
                new FirstLevelNavigationItem {
                    Label = Resources.Navigation_Mods, Icon = PackIconKind.FileCabinet, IsSelected = true
                },
                new FirstLevelNavigationItem {Label = Resources.Navigation_Exit, Icon = PackIconKind.ExitToApp}
            };
            var modules = new INavigationServiceItem[] {
                new NavigationServiceModule {Label = Resources.Navigation_Mods, Module = typeof(ModListModule)},
                new NavigationServiceCommand {
                    Label = Resources.Navigation_Exit, CommandType = NavigationCommandType.Exit
                }
            };
            Items = items;
            Modules = modules;
        }

        public IEnumerable<INavigationItem> Items { get; }

        public IEnumerable<INavigationServiceItem> Modules { get; }
    }
}