namespace Senselessly.Foolish.ModTrakt.Wpf.Models.App
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interface.Models;
    using Services.Storage;

    public class GameDictionary : IGameDictionary
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Publisher { get; set; }

        public IEnumerable<IGameRegistry> Registry { get; set; }

        public async Task<IEnumerable<GameDictionary>> LoadAsync(string gameDictionaryKey) =>
            await JsonFile.LoadResourceAsync<IEnumerable<GameDictionary>>(gameDictionaryKey);
    }
}