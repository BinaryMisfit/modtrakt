namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    using System.Collections.Generic;

    internal interface IConfigurationFile
    {
        int Version { get; set; }

        IEnumerable<IFolderMap> Folders { get; set; }
    }
}