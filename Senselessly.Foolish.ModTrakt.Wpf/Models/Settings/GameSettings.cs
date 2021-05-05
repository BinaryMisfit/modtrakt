namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using Interfaces.Settings;

    public sealed class GameSettings : IGameSettings
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string GamePath { get; set; }
    }
}