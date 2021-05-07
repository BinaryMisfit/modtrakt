namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using System.Collections.Generic;
    using Interfaces.Settings;

    internal sealed class ConfigurationFile : IConfigurationFile
    {
        public IConfigurationFileConfig FileConfig { get; set; }

        public IEnumerable<IConfigurationFileFolder> Folders { get; set; }
    }
}