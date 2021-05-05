namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Navigation
{
    using System;
    using Enums;
    using Interfaces.Navigation;

    internal sealed class NavigationServiceModule : INavigationServiceModule
    {
        public string Label { get; set; }

        public NavigationServiceType Type
        {
            get => NavigationServiceType.Module;
        }

        public Type Module { get; set; }
    }
}