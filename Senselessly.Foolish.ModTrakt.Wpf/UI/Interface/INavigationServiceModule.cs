namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Interface
{
    using System;

    public interface INavigationServiceModule : INavigationServiceItem
    {
        Type Module { get; set; }
    }
}