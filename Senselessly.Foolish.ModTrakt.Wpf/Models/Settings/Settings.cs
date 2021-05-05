namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using Interface.Settings;

    public class Settings : ISettings
    {
        public string ActiveGame { get; set; }

        public bool SelectGame { get; set; }

        public string LastGame { get; set; }
    }
}