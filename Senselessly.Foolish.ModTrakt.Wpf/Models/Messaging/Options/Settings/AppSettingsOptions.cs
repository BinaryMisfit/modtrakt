namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messaging.Options.Settings
{
    using Interfaces.Messaging.Settings;
    using Interfaces.Settings;

    internal sealed class AppSettingsOptions : IOptionsAppSettings
    {
        public AppSettingsOptions(IAppSettings settings) => Settings = settings;

        public IAppSettings Settings { get; }

        public bool Handled { get; set; }
    }
}