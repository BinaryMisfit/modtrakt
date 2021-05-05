namespace Senselessly.Foolish.ModTrakt.Wpf.Interface.Navigation
{
    using System;

    public interface INavigationServiceModule : INavigationServiceItem
    {
        Type Module { get; set; }
    }
}