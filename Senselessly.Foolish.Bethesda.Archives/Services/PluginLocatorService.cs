namespace Senselessly.Foolish.Bethesda.Archives.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using System.Linq;
    using Enums;
    using Interfaces.Services;
    using Models.Storage;

    public sealed class PluginLocatorService : IPluginLocatorService
    {
        private readonly IFileSystem _system;

        public PluginLocatorService(IFileSystem system) => _system = system;

        public bool IsLocatorReady() => _system != null;

        public IEnumerable<string> Locate(IDirectoryInfo path, ModTypes type, bool recurse)
        {
            SearchPath = path;
            SearchType = type;
            SearchRecursive = recurse;
            var filter = type == ModTypes.Archive ? FileTypes.ArchiveTypes : FileTypes.PluginTypes;
            if (type == ModTypes.Both)
            {
                filter = FileTypes.ArchiveTypes;
                filter = filter.Concat(FileTypes.PluginTypes).ToArray();
            }

            var options = recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            var root = _system.DirectoryInfo.FromDirectoryName(path.FullName);
            var plugins = root.EnumerateFiles("*", options)
                              .Where(f => filter.Contains(f.Extension));
            return plugins.Select(f => f.Name);
        }

        public IDirectoryInfo SearchPath { get; private set; }

        public bool SearchRecursive { get; private set; }

        public ModTypes SearchType { get; private set; }
    }
}