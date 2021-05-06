namespace Senselessly.Foolish.ModTrakt.Wpf.Extensions.Storage
{
    using System.IO.Abstractions;
    using Interfaces.Extensions;
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
    using SharpConfig;

    internal static class ConfigFile
    {
        public static T LoadConfigFile<T>(this IConfigSource sender, string filePath) where T : new()
        {
            if (sender == null) return default;

            var storage = Ioc.Default.GetService<IFileSystem>();
            if (storage == null) return default;

            var file = storage.FileInfo.FromFileName(filePath);
            var section = typeof(T).Name;
            if (!file.Exists) return new T();

            var configuration = Configuration.LoadFromFile(file.FullName);
            return configuration[section].ToObject<T>();
        }

        public static void SaveConfigFile<T>(this IConfigSource sender, T config, string filePath)
        {
            if (sender == null) return;

            var storage = Ioc.Default.GetService<IFileSystem>();
            if (storage == null) return;

            var file = storage.FileInfo.FromFileName(filePath);
            if (file.Directory?.Exists == false) file.Directory.Create();

            var section = config.GetType().Name;
            var configuration = new Configuration {Section.FromObject(name: section, obj: config)};
            configuration.SaveToFile(filePath);
        }
    }
}