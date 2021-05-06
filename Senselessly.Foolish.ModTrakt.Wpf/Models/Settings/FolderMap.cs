namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using System.Collections.Generic;

    internal sealed class FolderMap
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public IEnumerable<FolderMap> Folders { get; set; }
    }
}