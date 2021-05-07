namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    using System.Collections.Generic;

    internal interface IAppSettingsFolders : IAppSettingsSection
    {
        string Data { get; }

        string ExternalModules { get; }

        string ExternalPlugins { get; }

        string Games { get; }

        string Modules { get; }

        string Plugins { get; }

        string Product { get; }

        string User { get; }

        IEnumerable<string> ToStringArray();
    }
}