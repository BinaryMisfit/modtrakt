namespace Senselessly.Foolish.Bethesda.Archives.Interfaces.Services
{
    using System.Collections.Generic;
    using System.IO;
    using Enums;

    public interface IPluginLocatorService
    {
        IEnumerable<string> Locate(DirectoryInfo path, ModType type, bool recurse);
    }
}