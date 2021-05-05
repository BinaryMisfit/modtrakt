namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Navigation
{
    using Enums;
    using Interfaces.Navigation;

    internal sealed class NavigationServiceCommand : INavigationServiceCommand
    {
        public string Label { get; set; }

        public NavigationServiceType Type
        {
            get => NavigationServiceType.Command;
        }

        public NavigationCommandType CommandType { get; set; }
    }
}