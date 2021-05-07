namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    using System.Collections.Generic;

    internal interface IConfigurationFileConfig
    {
        IEnumerable<IConfigurationFileSection> Sections { get; }
    }
}