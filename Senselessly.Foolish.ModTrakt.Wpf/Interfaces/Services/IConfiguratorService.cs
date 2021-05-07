namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Services
{
    using System;
    using System.Threading.Tasks;
    using Extensions;
    using Settings;

    internal interface IConfiguratorService : IMessageStatusUpdate, IJsonSource
    {
        Task LoadAsync(string key);

        Task CheckFoldersAsync(Guid identifier, IAppSettingsFolders folders);
    }
}