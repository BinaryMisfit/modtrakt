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
            var path = storage.Path.Combine(AppContext.BaseDirectory, file);
            var fileInfo = storage.FileInfo.FromFileName(path);
            if (!fileInfo.Exists) return defaults;

            var product = AppContext.BaseDirectory;
            var userRoot = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            userRoot = storage.Path.Combine(userRoot, ConfigKeys.UserPath);
            var configuration = Configuration.LoadFromFile(fileInfo.FullName);
            if (configuration.Contains(ConfigKeys.SectionFolders, ConfigKeys.FolderUserData))
                userRoot = configuration[ConfigKeys.SectionFolders][ConfigKeys.FolderUserData].StringValue;

            var settings = new AppSettings {
                General =
                    new AppSettingsGeneral {
                        ActiveGame = configuration[ConfigKeys.SectionGeneral][ConfigKeys.GeneralActiveGame].StringValue
                    },
                Folders = new AppSettingsFolders {
                    User = userRoot,
                    Data = storage.Path.Combine(userRoot, ConfigKeys.DataPath),
                    ExternalModules = storage.Path.Combine(userRoot, ConfigKeys.UserModules),
                    ExternalPlugins = storage.Path.Combine(userRoot, ConfigKeys.UserPlugins),
                    Games = storage.Path.Combine(product, ConfigKeys.CoreGames),
                    Modules = storage.Path.Combine(product, ConfigKeys.CoreModules),
                    Plugins = storage.Path.Combine(product, ConfigKeys.CorePlugins)
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

            var saveFile = storage.Path.Combine(sender.Folders.Product, filePath);
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
                    Data = storage.Path.Combine(userRoot, ConfigKeys.DataPath),
                    ExternalModules = storage.Path.Combine(userRoot, ConfigKeys.UserModules),
                    ExternalPlugins = storage.Path.Combine(userRoot, ConfigKeys.UserPlugins),
                    Games = storage.Path.Combine(product, ConfigKeys.CoreGames),
                    Modules = storage.Path.Combine(product, ConfigKeys.CoreModules),
                    Plugins = storage.Path.Combine(product, ConfigKeys.CorePlugins),
                    Product = product,
                    User = userRoot
                },
                FirstRun = true
            };
            return settings;
        }
    }
}