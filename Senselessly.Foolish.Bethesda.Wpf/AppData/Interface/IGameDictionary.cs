namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IGameDictionary
    {
        string Name { get; set; }

        string Publisher { get; set; }

        IEnumerable<GameRegistry> Registry { get; set; }

        Task<IAsyncEnumerable<GameDictionary>> LoadAsync(string gameDictionaryKey);
    }
}