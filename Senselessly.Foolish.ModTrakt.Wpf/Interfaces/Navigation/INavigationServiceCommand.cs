namespace Senselessly.Foolish.ModTrakt.Wpf.Interface.Navigation
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