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

        public IEnumerable<string> Locate(DirectoryInfo path)
        {
            var root = _system.DirectoryInfo.FromDirectoryName(path.FullName);
            var plugins = root?.EnumerateFiles().Where(f => Constants.PluginTypes.Contains(f.Extension));
            return plugins?.Select(f => f.Name);
        }
    }
}