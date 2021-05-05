namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Navigation
{
    using System;
    using Enum;
    using Interface.Navigation;

    public class NavigationServiceModule : INavigationServiceModule
    {
        public string Label { get; set; }

        public NavigationServiceType Type
        {
            get => NavigationServiceType.Module;
        }

        public Type Module { get; set; }
    }
}