namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Navigation
{
    using Enums;

    internal interface INavigationServiceCommand : INavigationServiceItem
    {
        new NavigationServiceType Type
        {
            get => NavigationServiceType.Command;
        }

        NavigationCommandType CommandType { get; set; }
    }
}