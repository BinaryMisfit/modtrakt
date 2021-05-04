namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Navigation
{
    using System;
    using Enum;
    using Interface;

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