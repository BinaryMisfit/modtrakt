namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using Enums;
    using MaterialDesignExtensions.Model;
    using UI.Shared;

    internal interface INavigationService
    {
        IEnumerable<INavigationItem> Items { get; }

        INavigationItem HighlightTarget { get; }

        UserControl NavigateTo { get; }

        NavigationCommandType ExecuteCommand { get; }

        ExtendedWindow CreateWindow(Type type);

        void Reset();

        Task<NavigationServiceType> SelectProcess(INavigationItem item, UserControl selected);
    }
}