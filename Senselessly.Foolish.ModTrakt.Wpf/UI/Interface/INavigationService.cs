namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using Enum;
    using MaterialDesignExtensions.Model;

    public interface INavigationService
    {
        IEnumerable<INavigationItem> Items { get; }

        UserControl HiglightTarget { get; }

        UserControl NavigateTo { get; }

        NavigationCommandType ExecuteCommand { get; }

        Task<NavigationServiceType> SelectProcess(INavigationItem item, UserControl selected);
    }
}