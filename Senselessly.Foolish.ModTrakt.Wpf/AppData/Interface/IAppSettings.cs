namespace Senselessly.Foolish.ModTrakt.Wpf.AppData.Interface
{
    using System.Collections.Generic;

    public interface IAppSettings
    {
        ISettings Settings { get; }

        IGameSettings Game { get; }

        IEnumerable<IGameSettings> Installed { get; set; }

        bool Missing => Settings?.ActiveGame == null;

        bool PromptGame => Settings?.ActiveGame == null || Settings.SelectGame;
    }
}