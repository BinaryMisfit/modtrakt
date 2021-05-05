namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    using System.Collections.Generic;
    using System.Linq;

    internal interface IAppSettings
    {
        ISettings Settings { get; }

        IGameSettings Game { get; set; }

        IEnumerable<IGameSettings> Installed { get; set; }

        bool Missing
        {
            get => Settings?.ActiveGame == null;
        }

        bool GamePrompt
        {
            get => Settings?.ActiveGame == null || Settings.SelectGame;
        }

        bool GamesLoaded
        {
            get => Installed.Any();
        }
    }
}