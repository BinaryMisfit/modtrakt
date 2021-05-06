namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    using System.Collections.Generic;
    using Models.Settings;

    internal interface IConfigurationFile
    {
        int Version { get; set; }

        IEnumerable<FolderMap> Folders { get; set; }
    }
}