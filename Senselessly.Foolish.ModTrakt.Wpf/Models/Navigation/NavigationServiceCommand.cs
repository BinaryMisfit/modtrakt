namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Navigation
{
    using Enum;
    using Interface.Navigation;

    public class NavigationServiceCommand : INavigationServiceCommand
    {
        public string Label { get; set; }

        public NavigationServiceType Type
        {
            get => NavigationServiceType.Command;
        }

        public NavigationCommandType CommandType { get; set; }
    }
}