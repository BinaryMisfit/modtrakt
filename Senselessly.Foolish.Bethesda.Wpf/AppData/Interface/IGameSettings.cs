namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Interface
{
    using System;
    using System.Collections.Generic;

    public interface IGameSettings
    {
        string Name { get; set; }

        string Publisher { get; set; }

        string GamePath { get; set; }

        static IEnumerable<IGameSettings> LoadDictionary(string jsonKey) =>
            throw new NotImplementedException();
    }
}