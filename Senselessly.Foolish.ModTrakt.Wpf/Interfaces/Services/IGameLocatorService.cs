namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Settings;

    internal interface IGameLocatorService
    {
        IEnumerable<IGameSettings> Found { get; }

        Task<int> LoadAsync(string gameDictionaryKey);

        Task<int> Locate(CancellationToken cancel = default);
    }
}