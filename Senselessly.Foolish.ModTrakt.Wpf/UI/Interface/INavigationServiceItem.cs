namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Interface
{
    using Enum;

    public interface INavigationServiceItem
    {
        string Label { get; set; }

        NavigationServiceType Type { get; }
    }
}