namespace Senselessly.Foolish.Bethesda.Archives.Helpers
{
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using System.Linq;
    using Enums;
    using Interfaces.Services;
    using Models.Storage;

    public class PluginLocatorServiceService : IPluginLocatorService
    {
        private readonly IFileSystem _system;

        public PluginLocatorServiceService(IFileSystem system) => _system = system;

        public IEnumerable<string> Locate(DirectoryInfo path, ModType type, bool recurse)
        {
            string[] filter;
            switch (type)
            {
                case ModType.Archive:
                    filter = FileTypes.ArchiveTypes;
                    break;
                case ModType.Both:
                    filter = FileTypes.PluginTypes;
                    filter = filter.Concat(FileTypes.ArchiveTypes).ToArray();
                    break;
                case ModType.Plugin:
                    filter = FileTypes.PluginTypes;
                    break;
                default:
                    filter = null;
                    break;
            }

            var options = recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            if (filter == null) { return null; }

            var root = _system.DirectoryInfo.FromDirectoryName(path.FullName);
            var plugins = root?.EnumerateFiles(searchPattern: "*", searchOption: options)
                               .Where(f => filter.Contains(f.Extension));
            return plugins?.Select(f => f.Name);
        }
    }
}