namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Models
{
    using Interface;

    public class AppSettings : IAppSettings
    {
        private AppSettings(IGameSettings game, ISettings settings)
        {
            Game = game;
            Settings = settings;
        }

        public IGameSettings Game { get; }

        public ISettings Settings { get; }
    }
}