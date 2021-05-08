namespace Senselessly.Foolish.Bethesda.Archives.Interfaces.Services
{
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using Enums;

    public interface IPluginLocatorService
    {
        IDirectoryInfo SearchPath { get; }

        bool SearchRecursive { get; }

        ModTypes SearchType { get; }

        bool IsLocatorReady();

        IEnumerable<string> Locate(IDirectoryInfo path, ModTypes type, bool recurse);
    }
}