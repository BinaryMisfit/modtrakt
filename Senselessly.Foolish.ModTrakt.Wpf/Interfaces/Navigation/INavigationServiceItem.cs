namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Navigation
{
    using Enums;

    internal interface INavigationServiceItem
    {
        string Label { get; set; }

        NavigationServiceType Type { get; }
    }
}