namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    using System;
    using Extensions;

    internal interface IAppSettings : IConfigSource, IComparable<IAppSettings>
    {
        IAppSettingsFolders Folders { get; }

        IAppSettingsGeneral General { get; }
    }
}