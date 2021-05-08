namespace Senselessly.Foolish.Bethesda.Archives.Interfaces.Services
{
    using System.Collections.Generic;
    using System.IO;
    using Enums;

    public interface IPluginLocatorService
    {
        ModTypes SearchType { get; }

        bool IsLocatorReady();

        IEnumerable<string> Locate(DirectoryInfo path, ModTypes type, bool recurse);
    }
}