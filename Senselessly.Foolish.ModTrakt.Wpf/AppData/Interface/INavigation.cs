namespace Senselessly.Foolish.ModTrakt.Wpf.AppData.Interface
{
    using System.Collections.Generic;
    using MaterialDesignExtensions.Model;

    public interface INavigation
    {
        IEnumerable<INavigationItem> Items { get; }
    }
}