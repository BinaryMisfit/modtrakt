namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Interface
{
    using System;
    using System.Collections.Generic;
    using Models;

    public interface IGameDictionary
    {
        string Game { get; set; }

        string Publisher { get; set; }

        IEnumerable<GameRegistry> Registry { get; set; }

        static IEnumerable<IGameSettings> Load(string gameDictionaryKey) =>
            throw new NotImplementedException();
    }
}