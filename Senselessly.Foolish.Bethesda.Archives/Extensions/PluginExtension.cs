namespace Senselessly.Foolish.Bethesda.Archives.Extensions
{
    using Interfaces.Extensions;

    internal static class PluginExtension
    {
        public static int ParseModId(this IPlugin plugin)
        {
            if (string.IsNullOrEmpty(plugin.FolderName)) return -1;

            var folderName = plugin.FolderName;
            var separator = folderName.IndexOf('-');
            if (separator <= 0) return -1;

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

        public static string ParseModName(this IPlugin plugin)
        {
            if (string.IsNullOrEmpty(plugin.FolderName)) return null;

            var folderName = plugin.FolderName;
            if (folderName.Contains('\\')) folderName = folderName[(folderName.LastIndexOf('\\') + 1)..];

            var separator = folderName.IndexOf('-');
            if (separator <= 0) return folderName;

            var modName = folderName[..separator];
            var modPart = folderName[separator..];
            var parts = modPart.Split('-');
            var isNumeric = false;
            var max = parts.Length;
            var index = 0;
            while (!isNumeric && index + 1 < max)
            {
                isNumeric = int.TryParse(s: parts[index], result: out _);
                if (!isNumeric) modName = $"{modName}{parts[index]}";

                index++;
            }

            return modName.Trim();
        }
    }
}