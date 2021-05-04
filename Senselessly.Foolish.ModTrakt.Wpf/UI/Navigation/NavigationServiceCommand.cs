namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Navigation
{
    using Enum;
    using Interface;

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