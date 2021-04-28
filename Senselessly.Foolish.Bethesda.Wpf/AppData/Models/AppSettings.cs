namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Models
{
    using System.Collections.Generic;
    using Interface;

    public class AppSettings : IAppSettings
    {
        public AppSettings(IGameSettings game, ISettings settings)
        {
            Game = game;
            Settings = settings;
        }

        public IGameSettings Game { get; }

        public IEnumerable<IGameSettings> Installed { get; set; }

        public ISettings Settings { get; }
    }
}