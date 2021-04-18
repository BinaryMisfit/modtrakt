namespace Senselessly.Foolish.Bethesda.Wpf.Services.Game
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AppData.Interface;
    using Interface;

    public class GameLocator : IGameLocator
    {
        private readonly IGameDictionary _dictionary;
        private IEnumerable<IGameDictionary> _games;

        public GameLocator(IGameDictionary dictionary)
        {
            _dictionary = dictionary;
        }

        public string Current { get; private set; }

        public int Count { get; private set; }

        public int Index { get; private set; }

        public bool Load(string gameDictionaryKey)
        {
            _games = _dictionary.Load(gameDictionaryKey);
            return _games != null && _games.Any();

        }

        public bool Locate()
        {
            if (_games == null)
            {
                return false;
            }

            Count = _games.Count();
            Index = 0;
            _games.ToList()
                .ForEach(game =>
                {
                    Index++;
                    Current = $"{game.Game} by {game.Publisher}";
                });

            return true;
        }

        public IEnumerable<IGameSettings> Result()
        {
            throw new NotImplementedException();
        }
    }
}