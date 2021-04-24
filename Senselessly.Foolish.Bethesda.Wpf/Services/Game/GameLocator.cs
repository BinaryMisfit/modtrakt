namespace Senselessly.Foolish.Bethesda.Wpf.Services.Game
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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

        public async Task<int> LoadAsync(string gameDictionaryKey)
        {
            _games = await _dictionary.Load(gameDictionaryKey);
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
            var installed = true;
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
                    var settings = new GameSettings();
                    game.Registry.ToList()
                        .ForEach(entry =>
                        {
                            if (!installed)
                            {
                                return;
                            }

                            switch (entry.Usage)
                            {
                                case "Install":
                                    {
                                        var value = registry
                                            .FirstOrDefault(key => key.Key == $"{entry.Path}\\{entry.Key}")
                                            ?.Value.ToString();
                                        installed = value != null;
                                        break;
                                    }
                                case "Name":
                                    {
                                        var value = registry
                                            .FirstOrDefault(key => key.Key == $"{entry.Path}\\{entry.Key}")
                                            ?.Value.ToString();
                                        settings.Name = value;
                                        settings.ConfigName = value?.Replace(oldValue: " ", newValue: "").Trim();
                                        break;
                                    }
                                case "Path":
                                    {
                                        var value = registry
                                            .FirstOrDefault(key => key.Key == $"{entry.Path}\\{entry.Key}")
                                            ?.Value.ToString();
                                        settings.GamePath = value;
                                        break;
                                    }
                                case "Publisher":
                                    {
                                        var value = registry
                                            .FirstOrDefault(key => key.Key == $"{entry.Path}\\{entry.Key}")
                                            ?.Value.ToString();
                                        settings.Publisher = value;
                                        break;
                                    }
                            }
                        });
                    if (!installed)
                    {
                        return;
                    }

                    located.Add(settings);
                    InstalledGames = located;
                });
            return InstalledGames?.Count() ?? 0;
        }
    }
}