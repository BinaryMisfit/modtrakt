namespace Senselessly.Foolish.ModTrakt.Wpf.Interface.Navigation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using Enum;
    using MaterialDesignExtensions.Model;

    public interface INavigationService
    {
        IEnumerable<INavigationItem> Items { get; }

        INavigationItem HighlightTarget { get; }

        UserControl NavigateTo { get; }

        NavigationCommandType ExecuteCommand { get; }

        void Reset();

        Task<NavigationServiceType> SelectProcess(INavigationItem item, UserControl selected);
    }
}