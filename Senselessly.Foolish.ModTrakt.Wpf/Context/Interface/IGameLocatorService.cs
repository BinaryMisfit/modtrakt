namespace Senselessly.Foolish.ModTrakt.Wpf.Context.Interface
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AppData.Interface;

    public interface IGameLocatorService
    {
        IEnumerable<IGameSettings> InstalledGames { get; }

        Task<int> LoadAsync(string gameDictionaryKey);

        Task<int> Locate(CancellationToken cancel = default);
    }
}