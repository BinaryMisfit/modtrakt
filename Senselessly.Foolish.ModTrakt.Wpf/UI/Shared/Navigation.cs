namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Shared
{
    using System.Collections.Generic;
    using AppData.Interface;
    using MaterialDesignExtensions.Model;
    using MaterialDesignThemes.Wpf;
    using Properties;

    public class Navigation : INavigation
    {
        public Navigation()
        {
            var items = new[] {
                new FirstLevelNavigationItem {
                    Label = Resources.Navigation_Mods, Icon = PackIconKind.FileCabinet, IsSelected = true
                },
                new FirstLevelNavigationItem {Label = Resources.Navigation_Exit, Icon = PackIconKind.ExitToApp}
            };
            Items = items;
        }

        public IEnumerable<INavigationItem> Items { get; }
    }
}