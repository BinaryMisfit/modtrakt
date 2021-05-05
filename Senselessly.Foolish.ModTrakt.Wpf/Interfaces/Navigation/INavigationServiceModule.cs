namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Navigation
{
    using System;

    internal interface INavigationServiceModule : INavigationServiceItem
    {
        Type Module { get; set; }
    }
}