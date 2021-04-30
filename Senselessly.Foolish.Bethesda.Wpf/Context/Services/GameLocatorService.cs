namespace Senselessly.Foolish.Bethesda.Wpf.Context.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AppData.Interface;
    using AppData.Models;
    using Game;
    using Interface;
    using Models;

    public class GameLocatorService : IGameLocatorService
    {
        private const string GamePath = "Path";
        private const string GameInstall = "Install";

        private readonly IExceptionService _ex;
        private readonly IFileSystem _files;
        private readonly IGameDictionary _dictionary;
        private readonly IRegistryScannerService _registry;

        private IEnumerable<GameDictionary> _games;

        public GameLocatorService(IExceptionService ex,
            IFileSystem files,
            IGameDictionary dictionary,
            IRegistryScannerService registry)
        {
            _dictionary = dictionary;
            _ex = ex;
            _files = files;
            _registry = registry;
        }

        public IEnumerable<IGameSettings> InstalledGames { get; private set; }

        public async Task<int> LoadAsync(string gameDictionaryKey)
        {
            _games = await _dictionary.LoadAsync(gameDictionaryKey);
            return _games?.Count() ?? 0;
        }

        public EventHandler<GameLocatorArgs> Progress { get; set; }

        public async Task<int> Locate(CancellationToken cancel = default)
        {
            InstalledGames = null;
            if (_games == null)
            {
                return 0;
            }

            List<IGameSettings> games = null;
            var check = _games.OrderBy(game => game.Code)
                .SelectMany(game =>
                    game.Registry.Where(entry => entry.Usage.Equals(GameInstall))
                        .Select(entry => new RegistryResult(id: game.Code, registry: entry)))
                .ToArray();
            await foreach (var result in check.ToAsyncEnumerable().WithCancellation(cancel))
            {
                if (cancel.IsCancellationRequested)
                {
                    break;
                }

                try
                {
                    if (await _registry.ReadAsync(root: result.Registry.Root,
                        path: result.Registry.Path,
                        cancel: cancel,
                        keys: result.Registry.Key))
                    {
                        result.Value = _registry.Results.First().Value;
                    }
                }
                catch (Exception e)
                {
                    _ex.Send(new ExceptionInfo(e));
                    return 0;
                }

                var found = check.Where(code => code.Value != null).Select(code => code.Id).ToArray();
                if (!found.Any())
                {
                    return found.Length;
                }

                try
                {
                    var verified = _games.Where(game => found.Contains(game.Code)).OrderBy(game => game.Code);
                    await foreach (var game in verified.ToAsyncEnumerable().WithCancellation(cancel))
                    {
                        if (cancel.IsCancellationRequested)
                        {
                            break;
                        }

                        var registry = game.Registry.FirstOrDefault(entry => entry.Usage.Equals(GamePath));
                        if (registry == null)
                        {
                            continue;
                        }

                        if (!await _registry.ReadAsync(root: registry.Root,
                            path: registry.Path,
                            cancel: cancel,
                            keys: registry.Key))
                        {
                            continue;
                        }

                        var settings = new GameSettings
                        {
                            ConfigName = game.Code, GamePath = _registry.Results.First().Value.ToString(),
                        };

                        var installed = _files.DirectoryInfo.FromDirectoryName(settings.GamePath);
                        if (!installed.Exists)
                        {
                            continue;
                        }

                        games ??= new List<IGameSettings>();
                        games.Add(settings);
                    }
                }
                catch (Exception e)
                {
                    _ex.Send(new ExceptionInfo(e));
                    return 0;
                }
            }

            if (!cancel.IsCancellationRequested)
            {
                InstalledGames = games;
            }

            return InstalledGames?.Count() ?? 0;
        }
    }
}