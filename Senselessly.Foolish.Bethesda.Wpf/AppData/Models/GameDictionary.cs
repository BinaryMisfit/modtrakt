namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interface;
    using Modules;

    public class GameDictionary : IGameDictionary
    {
        public string Game { get; set; }

        public string Publisher { get; set; }

        public IEnumerable<GameRegistry> Registry { get; set; }

        public async Task<IEnumerable<GameDictionary>> Load(string gameDictionaryKey)
        {
            return await JsonFile.LoadResourceAsync<IEnumerable<GameDictionary>>(gameDictionaryKey);
        }
    }
}