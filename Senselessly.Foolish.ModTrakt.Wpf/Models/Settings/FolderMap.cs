namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using System.Collections.Generic;
    using Interfaces.Settings;

    internal sealed class FolderMap : IFolderMap
    {
        public string Name { get; }

        public string Path { get; }

        public IEnumerable<IFolderMap> Folders { get; }
    }
}