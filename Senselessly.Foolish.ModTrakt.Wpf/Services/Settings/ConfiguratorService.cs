namespace Senselessly.Foolish.ModTrakt.Wpf.Services.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.Linq;
    using System.Threading.Tasks;
    using Extensions.Messages;
    using Extensions.Settings;
    using Extensions.Storage;
    using Interfaces.Services;
    using Interfaces.Settings;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Models.App;
    using Models.Exceptions.Settings;
    using Models.Messaging.Messages.UI;
    using Models.Messaging.Options.UI;
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

        public async Task LoadAsync(string key)
        {
            await LoadConfigurationAsync(key);
        }

        public async Task CheckFoldersAsync(Guid identifier, IAppSettingsFolders folders)
        {
            await CheckConfigurationFoldersAsync(identifier, folders);
        }

        private async Task LoadConfigurationAsync(string key)
        {
            this.SendStatusUpdate(Resources.Check_Start);
            var config = await LoadConfigJson(key);
            if (config == null)
            {
                this.SendStatusUpdate(Resources.Check_Failed);
                throw new ConfigurationCorruptException();
            }

            var settings = new AppSettings();
            var folders = await LoadFolders(config.Folders);
            if (folders == null)
            {
                this.SendStatusUpdate(Resources.Check_Failed);
                throw new ConfigurationSectionMissingException(ConfigKeys.SectionFolders);
            }

            var sections = await LoadConfig(config.Config);
            if (sections != null)
            {
                settings.Folders = sections.Folders;
                settings.General = sections.General;
            }

            settings.Folders = folders;
            settings.SendSettings();
        }

        private async Task CheckConfigurationFoldersAsync(Guid identifier, IAppSettingsFolders folders)
        {
            if (folders == null)
            {
                this.SendStatusUpdate(Resources.Internal_Corrupt);
                return;
            }

            var update = Resources.Check_Folders;
            this.SendStatusUpdate(update);
            var sorted = await folders.ToStringArray().ToAsyncEnumerable().OrderBy(f => f).ToListAsync();

            var valid = false;
            await foreach (var folder in sorted.ToAsyncEnumerable())
            {
                valid = CheckFolder(folder);
                if (!valid) throw new ConfigurationCorruptException();
            }

            WeakReferenceMessenger.Default.Send(
                new TaskCompletionMessage(new CompletionMessageOptions(identifier, valid)));
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
                var message = new ExceptionInfo(e);
                message.SendException();
                return false;
            }

            return pathInfo.Exists;
        }

        private async Task<IAppSettings> LoadConfig(IConfigurationFileConfig fileConfig)
        {
            this.SendStatusUpdate(Resources.Check_Config_File);
            if (fileConfig?.Sections == null)
            {
                this.SendStatusUpdate(Resources.Internal_Corrupt);
                return null;
            }

            IAppSettings settings = new AppSettings();
            await foreach (var section in fileConfig.Sections.OrderBy(s => s.Name).ToAsyncEnumerable())
            {
                IAppSettingsSection configSection = section.Name switch {
                    ConfigKeys.SectionGeneral => new AppSettingsGeneral(),
                    ConfigKeys.SectionFolders => new AppSettingsFolders(),
                    _                         => null
                };

                if (configSection == null) continue;

                configSection = await configSection.LoadKeys(section.Keys);
                if (configSection == null) continue;

                await foreach (var property in configSection.GetType().GetProperties().ToAsyncEnumerable())
                {
                    var value = property.GetValue(configSection)?.ToString();
                    if (string.IsNullOrEmpty(value)) continue;

                    value = value.Replace(ConfigKeys.ProductFolder, _productFolder);
                    value = value.Replace(ConfigKeys.UserFolder, _userFolder);
                    property.SetValue(configSection, value);
                }

                settings.GetType().GetProperty(section.Name)?.SetValue(settings, configSection);
            }

            return settings;
        }

        private async Task<IConfigurationFile> LoadConfigJson(string key) =>
            await this.JsonResourceAsync<IConfigurationFile>(key);

        private async Task<IAppSettingsFolders> LoadFolders(IEnumerable<IConfigurationFileFolder> maps)
        {
            if (maps == null) return null;

            var folderMaps = maps as IConfigurationFileFolder[] ?? maps.ToArray();
            var source = folderMaps.ToList();
            var section = new AppSettingsFolders {
                Data = await ReadFolderFromMap(source, ConfigKeys.DataKey),
                ExternalModules = await ReadFolderFromMap(source, ConfigKeys.ExternalModulesKey),
                ExternalPlugins = await ReadFolderFromMap(source, ConfigKeys.ExternalPluginsKey),
                Games = await ReadFolderFromMap(source, ConfigKeys.GamesKey),
                Modules = await ReadFolderFromMap(source, ConfigKeys.ModulesKey),
                Plugins = await ReadFolderFromMap(source, ConfigKeys.PluginsKey),
                Product = await ReadFolderFromMap(source, ConfigKeys.ProductKey),
                User = await ReadFolderFromMap(source, ConfigKeys.UserKey)
            };
            return section;
        }

        private async Task<string> ReadFolderFromMap(IEnumerable<IConfigurationFileFolder> maps, string key)
        {
            var map = await maps.OrderBy(m => m.Name).ToAsyncEnumerable().FirstOrDefaultAsync(m => m.Name == key);
            if (map == null) return null;

            var path = map.Path;
            if (path.Contains(ConfigKeys.ProductFolder))
                path = path.Replace(ConfigKeys.ProductFolder, _productFolder);
            if (path.Contains(ConfigKeys.UserFolder))
                path = path.Replace(ConfigKeys.UserFolder, _userFolder);
            return path;
        }
    }
}