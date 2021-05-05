namespace Senselessly.Foolish.ModTrakt.Wpf.Interface.Navigation
{
    using Enum;

    public interface INavigationServiceItem
    {
        string Label { get; set; }

        NavigationServiceType Type { get; }
    }
}