namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using Interfaces.Settings;

    public sealed class Settings : ISettings
    {
        public string ActiveGame { get; set; }

        public bool SelectGame { get; set; }

        public string LastGame { get; set; }
    }
}