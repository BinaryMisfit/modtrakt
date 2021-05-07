namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    using System.Collections.Generic;

    internal interface IConfigurationFileSection
    {
        string Name { get; }

        IEnumerable<IConfigurationFileSectionKey> Keys { get; }
    }
}