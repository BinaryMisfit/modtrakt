namespace Senselessly.Foolish.ModTrakt.Wpf.Services.App
{
    using System;
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Interfaces.App;
    using Interfaces.Services;
    using Interfaces.Settings;
    using Models.App;
    using Models.Settings;

    internal sealed class GameLocatorService : IGameLocatorService
    {
        private readonly IGameDictionary _dictionary;

        private readonly IExceptionService _ex;
        private readonly IFileSystem _files;
        private readonly IRegistryScannerService _registry;

        private IEnumerable<GameDictionary> _games;

        public GameLocatorService(
            IExceptionService ex,
            IFileSystem files,
            IGameDictionary dictionary,
            IRegistryScannerService registry)
        {
            _dictionary = dictionary;
            _ex = ex;
            _files = files;
            _registry = registry;
        }

        public IEnumerable<IGameSettings> Found { get; private set; }

        public async Task<int> LoadAsync(string gameDictionaryKey)
        {
            _games = await _dictionary.LoadAsync(gameDictionaryKey);
            return _games?.Count() ?? 0;
        }

        public async Task<int> Locate(CancellationToken cancel = default)
        {
            Found = null;
            if (_games == null) { return 0; }

            List<IGameSettings> games = null;
            // TODO Refactor for Async
            var check = _games.OrderBy(game => game.Code)
                              .SelectMany(game =>
                                   game.Registry.Take(1)
                                       .Select(entry => new RegistryResult(id: game.Code, registry: entry)))
                              .ToArray();
            await foreach (var result in check.ToAsyncEnumerable().WithCancellation(cancel))
            {
                if (cancel.IsCancellationRequested) { break; }

                try
                {
                    if (await _registry.ReadAsync(root: result.Registry.Root,
                            path: result.Registry.Path,
                            cancel: cancel,
                            keys: result.Registry.Key))
                    {
                        result.Value = _registry.Results.First().Value;
                        var installed = _files.DirectoryInfo.FromDirectoryName(result.Value.ToString());
                        if (!installed.Exists) { continue; }

                        var game = _games.First(find => find.Code == result.Id);
                        var settings = new GameSettings {
                            Code = game.Code, Name = game.Name, GamePath = result.Value.ToString()
                        };
                        games ??= new List<IGameSettings>();
                        games.Add(settings);
                    }
                }
                catch (Exception e)
                {
                    _ex.Send(new ExceptionInfo(sourceName: nameof(GameLocatorService),
                        source: typeof(GameLocatorService),
                        e: e));
                    return 0;
                }
            }

            if (!cancel.IsCancellationRequested) { Found = games; }

            return Found?.Count() ?? 0;
        }
    }
}