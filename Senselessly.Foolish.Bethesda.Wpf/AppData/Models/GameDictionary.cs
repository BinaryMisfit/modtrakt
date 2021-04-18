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

        public IEnumerable<IGameDictionary> Load(string gameDictionaryKey)
        {
            return JsonFile.LoadResource<IEnumerable<GameDictionary>>(gameDictionaryKey);
        }
    }
}