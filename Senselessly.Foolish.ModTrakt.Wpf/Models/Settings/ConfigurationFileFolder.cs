namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using Interfaces.Settings;

    internal sealed class ConfigurationFileFolder : IConfigurationFileFolder
    {
        public string Name { get; set; }

        public string Path { get; set; }
    }
}