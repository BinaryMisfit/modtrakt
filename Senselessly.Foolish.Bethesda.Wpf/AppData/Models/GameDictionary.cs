namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Models
{
    using System.Collections.Generic;
    using Interface;
    using Modules;

    public class GameDictionary : IGameDictionary
    {
        public string Game { get; set; }

        public string Publisher { get; set; }

        public IEnumerable<GameRegistry> Registry { get; set; }
        
        public static IEnumerable<IGameDictionary> Load(string gameDictionaryKey) =>
            JsonFile.LoadResource<IEnumerable<IGameDictionary>>(gameDictionaryKey);
    }
}