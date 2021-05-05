namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    using System.Collections.Generic;

    internal interface IConfigurationFile
    {
        IEnumerable<IFolderMap> Folders { get; }
    }
}