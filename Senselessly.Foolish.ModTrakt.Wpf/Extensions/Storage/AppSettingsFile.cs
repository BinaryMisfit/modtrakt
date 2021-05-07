namespace Senselessly.Foolish.ModTrakt.Wpf.Extensions.Storage
{
    using System;
    using System.IO.Abstractions;
    using Interfaces.Settings;
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
    using Models.App;
    using Models.Settings;
    using SharpConfig;

    internal static class AppSettingsFile
    {
        public static AppSettings LoadConfigFile(this IAppSettings sender, string file)
        {
            if (sender == null) return null;

            IFileSystem storage = null;
            try { storage = Ioc.Default.GetService<IFileSystem>(); }
            catch { storage = new FileSystem(); }
            finally { storage ??= new FileSystem(); }

            var defaults = DefaultConfig(storage);
            var path = storage.Path.Combine(path1: AppContext.BaseDirectory, path2: file);
            var fileInfo = storage.FileInfo.FromFileName(path);
            if (!fileInfo.Exists) return defaults;

            var configuration = Configuration.LoadFromFile(fileInfo.FullName);
            var settings = new AppSettings {
                General = new AppSettingsGeneral {
                    ActiveGame = configuration[ConfigKeys.SectionGeneral][ConfigKeys.GeneralActiveGame].StringValue
                },
                FirstRun = false
            };
            return settings;
        }

        public static void SaveConfigFile(this IAppSettings sender, string filePath)
        {
            if (sender == null) return;

            var storage = Ioc.Default.GetService<IFileSystem>();
            if (storage == null) return;

            var saveFile = storage.Path.Combine(path1: sender.Folders.Product, path2: filePath);
            var file = storage.FileInfo.FromFileName(saveFile);
            if (file.Directory?.Exists == false) file.Directory.Create();
            file.Refresh();

            var configuration = new Configuration();
            configuration[ConfigKeys.SectionGeneral][ConfigKeys.GeneralActiveGame].StringValue =
                sender.General.ActiveGame ?? "";
            configuration[ConfigKeys.SectionFolders][ConfigKeys.FolderUserData].StringValue = sender.Folders.User ?? "";
            configuration.SaveToFile(file.FullName);
        }

        private static AppSettings DefaultConfig(IFileSystem storage)
        {
            var product = AppContext.BaseDirectory;
            var userRoot = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var settings = new AppSettings {
                Folders = new AppSettingsFolders {
                    Data = storage.Path.Combine(path1: userRoot, path2: "Data"),
                    ExternalModules = storage.Path.Combine(path1: userRoot, path2: "Modules"),
                    ExternalPlugins = storage.Path.Combine(path1: userRoot, path2: "Plugins"),
                    Games = storage.Path.Combine(path1: product, path2: "Games"),
                    Modules = storage.Path.Combine(path1: product, path2: "Modules"),
                    Plugins = storage.Path.Combine(path1: product, path2: "Plugins"),
                    Product = product,
                    User = userRoot
                },
                FirstRun = true
            };
            return settings;
        }
    }
}