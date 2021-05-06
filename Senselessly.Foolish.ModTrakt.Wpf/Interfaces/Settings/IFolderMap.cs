namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    using System.Collections.Generic;

    internal interface IFolderMap
    {
        string Name { get; set; }

        string Path { get; set; }

        IEnumerable<IFolderMap> Folders { get; set; }
    }
}