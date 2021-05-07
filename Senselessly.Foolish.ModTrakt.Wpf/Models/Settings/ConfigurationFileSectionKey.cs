namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using Interfaces.Settings;

    internal sealed class ConfigurationFileSectionKey : IConfigurationFileSectionKey
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}