namespace Senselessly.Foolish.ModTrakt.Wpf.Context.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AppData.Interface;
    using Game;

    public interface IGameLocatorService
    {
        IEnumerable<IGameSettings> InstalledGames { get; }

        EventHandler<GameLocatorArgs> Progress { get; set; }

        Task<int> LoadAsync(string gameDictionaryKey);

        Task<int> Locate(CancellationToken cancel = default);
    }
}