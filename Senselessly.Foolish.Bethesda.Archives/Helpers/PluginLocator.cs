namespace Senselessly.Foolish.Bethesda.Archives.Helpers
{
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using System.Linq;
    using Interfaces;
    using LibraryData;

    public class PluginLocator : IPluginLocator
    {
        private readonly IFileSystem _system;

        public PluginLocator(IFileSystem system) => _system = system;

        public IEnumerable<string> Locate(DirectoryInfo path, ModType type, bool recurse)
        {
            string[] filter;
            switch (type)
            {
                case ModType.Archive:
                    filter = Constants.ArchiveTypes;
                    break;
                case ModType.Both:
                    filter = Constants.PluginTypes;
                    filter = filter.Concat(Constants.ArchiveTypes).ToArray();
                    break;
                case ModType.Plugin:
                    filter = Constants.PluginTypes;
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