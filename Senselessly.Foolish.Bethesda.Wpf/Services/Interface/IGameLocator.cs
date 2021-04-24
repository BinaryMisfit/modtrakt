namespace Senselessly.Foolish.Bethesda.Wpf.Services.Interface
{
    using System;
    using System.Collections.Generic;
    using AppData.Interface;
    using Game;

    public interface IGameLocator
    {
        IEnumerable<IGameSettings> InstalledGames { get; }

        EventHandler<GameLocatorArgs> Progress { get; set; }

        int Load(string gameDictionaryKey);

        int Locate();
    }
}