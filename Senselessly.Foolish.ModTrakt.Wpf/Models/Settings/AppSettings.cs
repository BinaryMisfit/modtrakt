namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using Interfaces.Settings;

    internal sealed class AppSettings : IAppSettings
    {
        public int CompareTo(IAppSettings other)
        {
            if (other == null) return 1;

            var compare = General.CompareTo(other.General);
            compare += Folders.CompareTo(other.Folders);
            if (compare != 0) compare = compare > 0 ? 1 : -1;

            return compare;
        }

        public IAppSettingsFolders Folders { get; set; }

        public IAppSettingsGeneral General { get; set; }
    }
}