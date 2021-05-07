namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using System.Collections.Generic;
    using Interfaces.Settings;

    internal sealed class ConfigurationFileConfig : IConfigurationFileConfig
    {
        public IEnumerable<IConfigurationFileSection> Sections { get; set; }
    }
}