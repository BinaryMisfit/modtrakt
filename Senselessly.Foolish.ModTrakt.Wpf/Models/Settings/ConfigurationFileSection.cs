namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using System.Collections.Generic;
    using Interfaces.Settings;

    internal sealed class ConfigurationFileSection : IConfigurationFileSection
    {
        public string Name { get; set; }

        public IEnumerable<IConfigurationFileSectionKey> Keys { get; set; }
    }
}