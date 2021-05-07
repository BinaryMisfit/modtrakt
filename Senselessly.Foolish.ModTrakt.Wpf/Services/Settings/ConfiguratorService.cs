namespace Senselessly.Foolish.ModTrakt.Wpf.Services.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using System.Linq;
    using System.Threading.Tasks;
    using Extensions.Messages;
    using Extensions.Storage;
    using Interfaces.Services;
    using Interfaces.Settings;
    using Models.App;
    using Models.Settings;
    using Properties;

    internal sealed class ConfiguratorService : IConfiguratorService
    {
        private readonly string _productFolder;
        private readonly IFileSystem _storage;
        private readonly string _userFolder;

        public ConfiguratorService(IFileSystem storage)
        {
            _productFolder = AppContext.BaseDirectory;
            _userFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _storage = storage;
        }

        public async Task<IAppSettings> Load(string key)
        {
            this.SendStatusUpdate(Resources.Check_Start);
            var config = await LoadConfigJson(key);
            if (config == null)
            {
                this.SendStatusUpdate(Resources.Check_Failed);
                return null;
            }

            var settings = new AppSettings();
            var folders = await LoadFolders(config.Folders);
            if (folders == null)
            {
                var ex = new ExceptionInfo(sourceName: nameof(ConfiguratorService),
                    source: typeof(ConfiguratorService),
                    e: new InvalidDataException());
                ex.SendException();
                return null;
            }

            settings.Folders = folders;
            var check = await LoadConfigFiles(config.FileConfig);
            return !check ? null : settings;
        }

        public async Task<bool> CheckFolders(IAppSettingsFolders folders)
        {
            if (folders == null)
            {
                this.SendStatusUpdate(Resources.Internal_Corrupt);
                return false;
            }

            var update = Resources.Check_Folders;
            this.SendStatusUpdate(update);
            var sorted = await folders.ToStringArray().ToAsyncEnumerable().OrderBy(f => f).ToListAsync();

            var valid = false;
            await foreach (var folder in sorted.ToAsyncEnumerable())
            {
                valid = CheckFolder(folder);
                if (!valid) break;
            }

            return valid;
        }

        private bool CheckFolder(string folder)
        {
            var pathInfo = _storage.DirectoryInfo.FromDirectoryName(folder);
            if (pathInfo.Exists) return pathInfo.Exists;

            try
            {
                pathInfo.Create();
                pathInfo.Refresh();
                if (!pathInfo.Exists) return false;
            }
            catch (Exception e)
            {
                var message = new ExceptionInfo(sourceName: folder, source: typeof(ConfiguratorService), e: e);
                message.SendException();
                return false;
            }

            return pathInfo.Exists;
        }

        private async Task<bool> LoadConfigFiles(IConfigurationFileConfig fileConfig)
        {
            this.SendStatusUpdate(Resources.Check_Config_File);
            if (fileConfig?.Sections == null)
            {
                this.SendStatusUpdate(Resources.Internal_Corrupt);
                return false;
            }

            await foreach (var section in fileConfig.Sections.OrderBy(s => s.Name).ToAsyncEnumerable()) { }

            return false;
        }

        private async Task<IConfigurationFile> LoadConfigJson(string key) =>
            await this.JsonResourceAsync<IConfigurationFile>(key);

        private async Task<IAppSettingsFolders> LoadFolders(IEnumerable<IConfigurationFileFolder> maps)
        {
            if (maps == null) return null;

            var folderMaps = maps as IConfigurationFileFolder[] ?? maps.ToArray();
            var source = folderMaps.ToList();
            var section = new AppSettingsFolders {
                Data = await ReadFolderFromMap(maps: source, key: ConfigKeys.DataKey),
                ExternalModules = await ReadFolderFromMap(maps: source, key: ConfigKeys.ExternalModulesKey),
                ExternalPlugins = await ReadFolderFromMap(maps: source, key: ConfigKeys.ExternalPluginsKey),
                Games = await ReadFolderFromMap(maps: source, key: ConfigKeys.GamesKey),
                Modules = await ReadFolderFromMap(maps: source, key: ConfigKeys.ModulesKey),
                Plugins = await ReadFolderFromMap(maps: source, key: ConfigKeys.PluginsKey),
                Product = await ReadFolderFromMap(maps: source, key: ConfigKeys.ProductKey),
                User = await ReadFolderFromMap(maps: source, key: ConfigKeys.UserKey)
            };
            return section;
        }

        private async Task<string> ReadFolderFromMap(IEnumerable<IConfigurationFileFolder> maps, string key)
        {
            var map = await maps.OrderBy(m => m.Name).ToAsyncEnumerable().FirstOrDefaultAsync(m => m.Name == key);
            if (map == null) return null;

            var path = map.Path;
            if (path.Contains(ConfigKeys.ProductFolder))
                path = path.Replace(oldValue: ConfigKeys.ProductFolder, newValue: _productFolder);
            if (path.Contains(ConfigKeys.UserFolder))
                path = path.Replace(oldValue: ConfigKeys.UserFolder, newValue: _userFolder);
            return path;
        }
    }
}