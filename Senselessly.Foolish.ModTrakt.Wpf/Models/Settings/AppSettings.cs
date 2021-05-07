namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using Interfaces.Settings;

    internal sealed class AppSettings : IAppSettings
    {
        public bool FirstRun { get; set; }

        public IAppSettingsFolders Folders { get; set; }

        public IAppSettingsGeneral General { get; set; }

        public bool IsConfigured
        {
            get => Folders != null && General != null;
        }
    }
}