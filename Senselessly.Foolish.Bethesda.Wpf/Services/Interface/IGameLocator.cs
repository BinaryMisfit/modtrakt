namespace Senselessly.Foolish.Bethesda.Wpf.Services.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AppData.Interface;
    using Game;

    public interface IGameLocator
    {
        IEnumerable<IGameSettings> InstalledGames { get; }

        EventHandler<GameLocatorArgs> Progress { get; set; }

        Task<int> LoadAsync(string gameDictionaryKey);

        int Locate();
    }
}