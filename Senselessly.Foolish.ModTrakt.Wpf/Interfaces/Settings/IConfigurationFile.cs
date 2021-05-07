namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    using System.Collections.Generic;

    internal interface IConfigurationFile
    {
        IConfigurationFileConfig Config { get; }

        IEnumerable<IConfigurationFileFolder> Folders { get; }
    }
}