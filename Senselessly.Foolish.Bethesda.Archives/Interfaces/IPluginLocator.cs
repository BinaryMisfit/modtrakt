namespace Senselessly.Foolish.Bethesda.Archives.Interfaces
{
    using System.Collections.Generic;
    using System.IO;

    public interface IPluginLocator
    {
        IEnumerable<string> Locate(DirectoryInfo path);
    }
}