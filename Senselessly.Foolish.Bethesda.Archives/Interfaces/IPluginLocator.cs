namespace Senselessly.Foolish.Bethesda.Archives.Interfaces
{
    using System.Collections.Generic;
    using System.IO;
    using LibraryData;

    public interface IPluginLocator
    {
        IEnumerable<string> Locate(DirectoryInfo path, ModType type, bool recurse);
    }
}