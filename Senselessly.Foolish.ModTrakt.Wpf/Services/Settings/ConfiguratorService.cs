namespace Senselessly.Foolish.ModTrakt.Wpf.Services.Settings
{
    using System;
    using System.Collections.Generic;
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
        private const string ProductFolder = "{ProductFolder}";
        private const string UserFolder = "{UserFolder}";
        private readonly IAppSettings _settings;
        private readonly IFileSystem _storage;

        public ConfiguratorService(IAppSettings settings, IFileSystem storage)
        {
            _settings = settings;
            _storage = storage;
        }

        public async Task<bool> Check(string key)
        {
            this.SendStatusUpdate(Resources.Check_Start);
            var configs = await LoadConfigJson(key);
            if (configs == null)
            {
                this.SendStatusUpdate(Resources.Check_Failed);
                return false;
            }

            var configured = false;
            await foreach (var config in configs.ToAsyncEnumerable())
            {
                var update = string.Format(format: Resources.Check_Folders, arg0: config.Version);
                this.SendStatusUpdate(update);
                configured = await CheckFolders(version: config.Version, folders: config.Folders);
                if (!configured) break;

                configured = CheckConfigFile();
            }

            return configured;
        }

        private async Task<bool> CheckFolders(int version, IEnumerable<IFolderMap> folders)
        {
            if (folders == null)
            {
                this.SendStatusUpdate("Configuration file corrupted. Application reinstall required.");
                return false;
            }

            var update = string.Format(format: Resources.Check_Folders, arg0: version);
            this.SendStatusUpdate(update);
            var sorted = await folders.ToAsyncEnumerable().OrderBy(folder => folder.Name).ToListAsync();

            var valid = false;
            await foreach (var folder in sorted.ToAsyncEnumerable())
            {
                valid = await CheckFolderMap(folder);
                if (!valid) break;
            }

            return valid;
        }

        private async Task<bool> CheckFolderMap(IFolderMap map, IFileSystemInfo parent = null)
        {
            var productFolder = _settings.ProductFolder;
            var userFolder = _settings.UserFolder;
            var name = map.Name;
            var path = map.Path;
            if (path.Contains(ProductFolder))
                path = path.Replace(oldValue: ProductFolder, newValue: productFolder.FullName);

            if (path.Contains(UserFolder)) path = path.Replace(oldValue: UserFolder, newValue: userFolder.FullName);

            var checkPath = path;
            if (parent != null) checkPath = _storage.Path.Combine(path1: parent.FullName, path2: path);

            var pathInfo = _storage.DirectoryInfo.FromDirectoryName(checkPath);
            if (!pathInfo.Exists)
                try
                {
                    pathInfo.Create();
                    pathInfo.Refresh();
                    if (!pathInfo.Exists) return false;
                }
                catch (Exception e)
                {
                    var message = new ExceptionInfo(sourceName: $"Folder: {name}",
                        source: typeof(ConfiguratorService),
                        e: e);
                    message.SendException();
                    return false;
                }

            if (map.Folders == null || !map.Folders.Any()) return true;

            await foreach (var folder in map.Folders.ToAsyncEnumerable())
            {
                await CheckFolderMap(map: folder, parent: pathInfo);
            }

            return true;
        }

        private bool CheckConfigFile()
        {
            this.SendStatusUpdate(Resources.Check_Config_File);
            return false;
        }

        private async Task<IEnumerable<ConfigurationFile>> LoadConfigJson(string key) =>
            await this.JsonResourceAsync<IEnumerable<ConfigurationFile>>(key);
    }
}