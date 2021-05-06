namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Services
{
    using System.Threading.Tasks;
    using Extensions;

    internal interface IConfiguratorService : IConfigSource, IMessageStatusUpdate, IJsonSource
    {
        Task<bool> Check(string key);
    }
}