namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Interface
{
    using System.Collections.Generic;
    using Models;

    public interface IGameDictionary
    {
        string Game { get; set; }

        string Publisher { get; set; }

        IEnumerable<GameRegistry> Registry { get; set; }

        IEnumerable<IGameDictionary> Load(string gameDictionaryKey);
    }
}