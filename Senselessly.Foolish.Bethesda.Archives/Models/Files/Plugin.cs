namespace Senselessly.Foolish.Bethesda.Archives.Models.Files
{
    using System.Collections.Generic;
    using System.Linq;
    using Enums;

    public sealed class Plugin
    {
        public Plugin(string folderName)
        {
            FolderName = folderName;
            ModName = ParseModName(folderName);
            ModId = ParseModId(folderName);
        }

        public ArchiveType Archives { get; set; }

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

        public string ModName { get; }

        public int ModId { get; }

        public LooseType Loose { get; set; }

        public Dictionary<PluginType, int> TypeDict { get; set; }

        public string Types
        {
            get =>
                TypeDict == null
                    ? $"{PluginType.None}"
                    : string.Join(separator: ", ",
                        values: TypeDict.OrderBy(kv => $"{kv.Key}").Select(kv => $"{kv.Key}: {kv.Value}"));
        }

        private static string ParseModName(string folderName)
        {
            var separator = folderName.IndexOf('-');
            if (separator <= 0) { return folderName; }

            var modName = folderName[..separator];
            var modPart = folderName[separator..];
            var parts = modPart.Split('-');
            var isNumeric = false;
            var max = parts.Length;
            var index = 0;
            while (!isNumeric && index + 1 < max)
            {
                isNumeric = int.TryParse(s: parts[index], result: out var _);
                if (!isNumeric) { modName += $"{parts[index]}"; }

                index++;
            }

            return modName;
        }

        private static int ParseModId(string folderName)
        {
            var separator = folderName.IndexOf('-');
            if (separator <= 0) { return 0; }

            var modId = 0;
            var modPart = folderName[separator..];
            var parts = modPart.Split('-');
            var isNumeric = false;
            var max = parts.Length;
            var index = 0;
            while (!isNumeric && index + 1 < max)
            {
                isNumeric = int.TryParse(s: parts[index], result: out modId);
                index++;
            }

            return modId;
        }
    }
}