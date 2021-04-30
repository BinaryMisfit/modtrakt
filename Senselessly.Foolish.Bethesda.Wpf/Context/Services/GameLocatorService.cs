namespace Senselessly.Foolish.Bethesda.Wpf.Context.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using AppData.Interface;
    using AppData.Models;
    using Game;
    using Interface;
    using Models;

    public class GameLocatorService : IGameLocatorService
    {
        private readonly IGameDictionary _dictionary;
        private readonly IRegistryScannerService _registry;

        private IEnumerable<GameDictionary> _games;

        public GameLocatorService(IGameDictionary dictionary, IRegistryScannerService registry)
        {
            _dictionary = dictionary;
            _registry = registry;
        }

        public IEnumerable<IGameSettings> InstalledGames { get; private set; }

        public async Task<int> LoadAsync(string gameDictionaryKey)
        {
            _games = await _dictionary.LoadAsync(gameDictionaryKey);
            return _games?.Count() ?? 0;
        }

        public EventHandler<GameLocatorArgs> Progress { get; set; }

        public async Task<int> Locate()
        {
            if (_games == null)
            {
                return 0;
            }

            var count = _games.Count();
            var index = 0;
            var located = new List<IGameSettings>();
            var installed = true;
            await _games.OrderBy(game => game.Name)
                .ToAsyncEnumerable()
                .ForEachAsync(async game =>
                {
                    index++;
                    Progress(sender: this, e: new GameLocatorArgs(game: game.Name, current: index, remaining: count));
                    var registry = new List<RegistryResult>();
                    await game.Registry.GroupBy(entry => entry.Path)
                        .ToAsyncEnumerable()
                        .ForEachAsync(group =>
                        {
                            var found = _registry.Read(path: group.Key, keys: group.Select(item => item.Key).ToArray());
                            if (found)
                            {
                                registry.AddRange(_registry.Results);
                            }
                        });
                    var settings = new GameSettings();
                    await game.Registry.ToAsyncEnumerable()
                        .ForEachAsync(entry =>
                        {
                            if (!installed)
                            {
                                installed = true;
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
                    if (installed && !string.IsNullOrEmpty(settings.GamePath))
                    {
                        var gameFolder = new DirectoryInfo(settings.GamePath);
                        installed = gameFolder.Exists;
                    }
                    
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