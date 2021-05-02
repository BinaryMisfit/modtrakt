namespace Senselessly.Foolish.ModTrakt.Wpf.AppData.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IGameDictionary
    {
        string Code { get; set; }

        string Name { get; set; }

        string Publisher { get; set; }

        IEnumerable<IGameRegistry> Registry { get; set; }

        Task<IEnumerable<GameDictionary>> LoadAsync(string gameDictionaryKey);
    }
}