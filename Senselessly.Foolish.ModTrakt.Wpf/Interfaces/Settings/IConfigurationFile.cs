namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    using System.Collections.Generic;

    internal interface IConfigurationFile
    {
        IConfigurationFileConfig FileConfig { get; }

        IEnumerable<IConfigurationFileFolder> Folders { get; }
    }
}