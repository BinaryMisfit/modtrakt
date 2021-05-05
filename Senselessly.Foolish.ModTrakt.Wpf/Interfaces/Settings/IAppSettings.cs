namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    using System.Collections.Generic;
    using System.IO.Abstractions;

    internal interface IAppSettings
    {
        ISettings Settings { get; }

        IGameSettings Game { get; set; }

        IEnumerable<IGameSettings> Installed { get; set; }

        IDirectoryInfo ProductFolder { get; }

        IDirectoryInfo UserFolder { get; }
    }
}