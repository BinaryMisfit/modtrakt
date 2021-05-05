namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using Interface.Settings;

    public class GameSettings : IGameSettings
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string GamePath { get; set; }
    }
}