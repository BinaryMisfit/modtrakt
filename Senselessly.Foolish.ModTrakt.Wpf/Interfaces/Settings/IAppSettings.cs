namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    using System.Collections.Generic;
    using System.IO.Abstractions;

    internal interface IAppSettings
    {
        bool Configured { get; set; }

        IGameSettings Game { get; set; }

        IEnumerable<IGameSettings> Installed { get; set; }

        IDirectoryInfo ProductFolder { get; }

        ISettings Settings { get; }

        IDirectoryInfo UserFolder { get; }
    }
}