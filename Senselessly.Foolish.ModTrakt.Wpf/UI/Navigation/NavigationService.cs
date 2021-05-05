namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Navigation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using Enum;
    using Interface;
    using MaterialDesignExtensions.Model;
    using MaterialDesignThemes.Wpf;
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
    using Module.ModList;
    using Properties;

    public class NavigationService : INavigationService
    {
        private readonly IEnumerable<INavigationServiceItem> _modules;

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
            _modules = modules;
        }

        public IEnumerable<INavigationItem> Items { get; }

        public UserControl HiglightTarget { get; private set; }

        public UserControl NavigateTo { get; private set; }

        public NavigationCommandType ExecuteCommand { get; private set; }

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
                    if (selected != null && type == selected.GetType()) { return NavigationServiceType.NotSet; }

                    if (type == typeof(ModListModule)) { NavigateTo = Ioc.Default.GetService<ModListModule>(); }

                    return NavigationServiceType.Module;
                }
                case NavigationServiceType.Command: {
                    i.IsSelected = false;
                    HiglightTarget = selected;
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
    }
}