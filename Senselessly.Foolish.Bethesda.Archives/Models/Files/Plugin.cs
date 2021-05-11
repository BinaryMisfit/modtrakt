namespace Senselessly.Foolish.Bethesda.Archives.Models.Files
{
    using System.Collections.Generic;
    using System.Linq;
    using Enums;
    using Extensions;
    using Interfaces.Extensions;

    public sealed class Plugin : IPlugin
    {
        private int _modId;
        private string _modName;

        public Plugin(string folderName) => FolderName = folderName;

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

        public int ModId
        {
            get {
                if (_modId == 0) _modId = this.ParseModId();

                return _modId;
            }
        }

        public string ModName
        {
            get {
                if (string.IsNullOrEmpty(_modName)) _modName = this.ParseModName();

                return _modName;
            }
        }

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