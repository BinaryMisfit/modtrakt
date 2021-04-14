namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Modules
{
    using System.IO;
    using SharpConfig;

    public static class ConfigFile
    {
        public static T LoadIni<T>(string filePath)
        where T : new()
        {
            var file = new FileInfo(filePath);
            var section = typeof(T).Name;
            if (!file.Exists)
            {
                return new T();
            }

            var configuration = Configuration.LoadFromFile(file.FullName);
            return configuration[section].ToObject<T>();
        }

        public static void SaveIni<T>(T config, string filePath)
        {
            var file = new FileInfo(filePath);
            if (file.Directory?.Exists == false)
            {
                file.Directory.Create();
            }

            var section = typeof(T).Name;
            var configuration = new Configuration
            {
                Section.FromObject(name: section, obj: config),
            };
            configuration.SaveToFile(filePath);
        }
    }
}