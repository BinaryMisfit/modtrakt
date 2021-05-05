namespace Senselessly.Foolish.ModTrakt.Wpf.Interface.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Wpf.Models.App;

    public interface IGameDictionary
    {
        string Code { get; set; }

        string Name { get; set; }

        string Publisher { get; set; }

        IEnumerable<IGameRegistry> Registry { get; set; }

        Task<IEnumerable<GameDictionary>> LoadAsync(string gameDictionaryKey);
    }
}