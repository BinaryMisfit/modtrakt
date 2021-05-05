namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    using System.Collections.Generic;

    internal interface IFolderMap
    {
        string Name { get; }

        string Path { get; }

        IEnumerable<IFolderMap> Folders { get; }
    }
}