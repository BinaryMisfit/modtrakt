namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Interface
{
    using Enum;

    public interface INavigationServiceCommand : INavigationServiceItem
    {
        new NavigationServiceType Type
        {
            get => NavigationServiceType.Command;
        }

        NavigationCommandType CommandType { get; set; }
    }
}