namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Models
{
    using Default;
    using Interface;
    using Modules;

    public class AppSettings : IAppSettings
    {
        public AppSettings(IGameSettings game, ISettings settings)
        {
            Game = game;
            Settings = settings;
        }

        public IGameSettings Game { get; }

        public ISettings Settings { get; }

        public static IAppSettings Load()
        {
            var settings = ConfigFile.LoadIni<Settings>(Config.SettingsPath);

            return new AppSettings(settings: settings, game: null);
        }
    }
}