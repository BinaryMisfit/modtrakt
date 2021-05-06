namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using System.Collections.Generic;
    using Interfaces.Settings;

    internal sealed class ConfigurationFile : IConfigurationFile
    {
        public int Version { get; set; }

        public IEnumerable<IFolderMap> Folders { get; set; }
    }
}