namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.App
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.App;

    internal interface IGameDictionary
    {
        string Code { get; set; }

        string Name { get; set; }

        string Publisher { get; set; }

        IEnumerable<IGameRegistry> Registry { get; set; }

        Task<IEnumerable<GameDictionary>> LoadAsync(string gameDictionaryKey);
    }
}