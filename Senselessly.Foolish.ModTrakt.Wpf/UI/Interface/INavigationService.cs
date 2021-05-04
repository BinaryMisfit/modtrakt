namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Interface
{
    using System.Collections.Generic;
    using System.Linq;
    using MaterialDesignExtensions.Model;

    public interface INavigationService
    {
        IEnumerable<INavigationItem> Items { get; }

        IEnumerable<INavigationServiceItem> Modules { get; }

        INavigationServiceItem FindItem(string label) => Modules.FirstOrDefault(module => module.Label == label);
    }
}