namespace Senselessly.Foolish.Bethesda.Archives.Models.Files
{
    using System.Collections.Generic;
    using System.Linq;
    using Enums;
    using Extensions;
    using Interfaces.Extensions;

    public sealed class Plugin : IPlugin
    {
        public Plugin(string folderName)
        {
            FolderName = folderName;
            ModName = this.ParseModName();
            ModId = this.ParseModId();
        }

        public ArchiveTypes Archives { get; set; }

        public string ExtraFolders
        {
            get => string.IsNullOrEmpty(Folders) ? "None" : Folders;
        }

        public string ExtraFiles
        {
            get => string.IsNullOrEmpty(Files) ? "None" : Files;
        }

        public string FolderName { get; }

        public string Folders { get; set; }

        public string Files { get; set; }

        public int ModId { get; }

        public string ModName { get; }

        public LooseTypes Loose { get; set; }

        public IDictionary<PluginTypes, int> TypeDict { get; set; }

        public string Types
        {
            get =>
                TypeDict == null
                    ? $"{PluginTypes.None}"
                    : string.Join(separator: ", ",
                        values: TypeDict.OrderBy(kv => $"{kv.Key}").Select(kv => $"{kv.Key}: {kv.Value}"));
        }
    }
}