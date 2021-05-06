namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.Linq;
    using System.Threading.Tasks;
    using Extensions.Messages;
    using Extensions.Storage;
    using Interfaces.Settings;
    using Properties;

    internal sealed class Configurator : IConfigurator
    {
        private readonly IAppSettings _settings;
        private readonly IFileSystem _storage;

        public Configurator(IAppSettings settings, IFileSystem storage)
        {
            _settings = settings;
            _storage = storage;
        }

        public async Task<bool> Check(string key)
        {
            this.SendStatusUpdate(Resources.Check_Start);
            var config = await LoadConfigJson(key);
            if (config == null)
            {
                this.SendStatusUpdate(Resources.Check_Failed);
                return false;
            }

            var update = string.Format(format: Resources.Check_Folders, arg0: config.Version);
            this.SendStatusUpdate(update);
            var configured = await CheckFolders(version: config.Version, folders: config.Folders);
            if (!configured) { return false; }

            configured = CheckConfigFile();
            return configured;
        }

        private async Task<bool> CheckFolders(int version, IEnumerable<FolderMap> folders)
        {
            if (folders == null)
            {
                this.SendStatusUpdate("Configuration file corrupted. Application reinstall required.");
                return false;
            }

            var update = string.Format(format: Resources.Check_Folders, arg0: version);
            this.SendStatusUpdate(update);
            var sorted = await folders.ToAsyncEnumerable().OrderBy(folder => folder.Name).ToListAsync();
            var productFolder = _settings.ProductFolder;
            var userFolder = _settings.UserFolder;

            await foreach (var folder in sorted.ToAsyncEnumerable())
            {
                var name = folder.Name;
                var path = folder.Path;
                if (path.Equals("productPath")) { path = "${productFolder}"; }

                var checkPath = _storage.DirectoryInfo.FromDirectoryName(path);
                if (!checkPath.Exists) { }
            }

            return true;
        }

        private bool CheckConfigFile()
        {
            this.SendStatusUpdate(Resources.Check_Config_File);
            return false;
        }

        private async Task<ConfigurationFile> LoadConfigJson(string key) =>
            await this.JsonResourceAsync<ConfigurationFile>(key);
    }
}