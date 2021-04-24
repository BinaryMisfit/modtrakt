namespace Senselessly.Foolish.Bethesda.Wpf.Services.Game
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AppData.Interface;
    using AppData.Models;
    using Interface;
    using Models;

    public class GameLocator : IGameLocator
    {
        private readonly IGameDictionary _dictionary;
        private readonly IRegistryScanner _registry;

        private IEnumerable<IGameDictionary> _games;

        public GameLocator(IGameDictionary dictionary, IRegistryScanner registry)
        {
            _dictionary = dictionary;
            _registry = registry;
        }

        public IEnumerable<IGameSettings> InstalledGames { get; private set; }

        public int Load(string gameDictionaryKey)
        {
            _games = _dictionary.Load(gameDictionaryKey);
            return _games?.Count() ?? 0;
        }

        public EventHandler<GameLocatorArgs> Progress { get; set; }

        public int Locate()
        {
            if (_games == null)
            {
                return 0;
            }

            var count = _games.Count();
            var index = 0;
            var located = new List<IGameSettings>();
            _games.ToList()
                .ForEach(game =>
                {
                    index++;
                    Progress(sender: this, e: new GameLocatorArgs(game: game.Game, current: index, remaining: count));
                    var registry = new List<RegistryResult>();
                    game.Registry.GroupBy(entry => entry.Path)
                        .ToList()
                        .ForEach(group =>
                        {
                            var found = _registry.Read(path: group.Key, keys: group.Select(item => item.Key).ToArray());
                            if (found)
                            {
                                registry.AddRange(_registry.Results);
                            }
                        });
                    var installed = registry.FirstOrDefault(result =>
                                result.Key ==
                                game.Registry.Where(entry => entry.Usage == "Install")
                                    .Select(entry => $"{entry.Path}\\${entry.Key}")
                                    .FirstOrDefault())
                            ?.Value !=
                        null;
                    if (!installed)
                    {
                        return;
                    }

                    located.Add(new GameSettings());
                    InstalledGames = located;
                });
            return InstalledGames?.Count() ?? 0;
        }
    }
}