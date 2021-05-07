namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    internal interface IAppSettings
    {
        bool FirstRun { get; set; }

        IAppSettingsFolders Folders { get; }

        IAppSettingsGeneral General { get; }

        bool IsConfigured { get; }
    }
}