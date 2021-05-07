namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Services
{
    using System.Threading.Tasks;
    using Extensions;
    using Settings;

    internal interface IConfiguratorService : IConfigSource, IMessageStatusUpdate, IJsonSource
    {
        Task<IAppSettings> Load(string key);

        Task<bool> CheckFolders(IAppSettingsFolders folders);
    }
}