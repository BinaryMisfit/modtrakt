namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Modules
{
    using System;
    using System.IO;
    using System.IO.Abstractions;
    using SharpConfig;

    public class ConfigFile
    {
        private readonly IFileSystem _fileSystem;

        public ConfigFile(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public T LoadIni<T>(string filePath) where T : new()
        {
            if (_fileSystem == null)
            {
                throw new SystemException("File system not initialized");
            }

            var file = new FileInfo(filePath);
            var section = typeof(T).Name;
            if (!file.Exists)
            {
                return new T();
            }

            var configuration = Configuration.LoadFromFile(file.FullName);
            return configuration[section].ToObject<T>();
        }

        public void SaveIni<T>(T config, string filePath)
        {
            var file = new FileInfo(filePath);
            if (file.Directory?.Exists == false)
            {
                file.Directory.Create();
            }

            var section = config.GetType().Name;
            var configuration = new Configuration
            {
                Section.FromObject(name: section, obj: config),
            };
            configuration.SaveToFile(filePath);
        }
    }
}