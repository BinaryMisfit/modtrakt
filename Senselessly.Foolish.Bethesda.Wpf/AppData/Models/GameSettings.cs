namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interface;
    using Microsoft.Win32;
    using Modules;

    public class GameSettings : IGameSettings
    {
        public string ConfigName { get; set; }
        public string Name { get; set; }

        public string Publisher { get; set; }

        public string GamePath { get; set; }

        public static async Task<List<IGameSettings>> LoadDictionary(string jsonKey)
        {
            var gameDictionary = await JsonFile.LoadResourceAsync<IEnumerable<GameDictionary>>(jsonKey);
            var gameList = new List<IGameSettings>();
            gameDictionary.ToList()
                .ForEach(entry =>
                {
                    var pathKey = entry.Registry?.FirstOrDefault(key => key.Usage == "Install");
                    if (pathKey == null)
                    {
                        return;
                    }

                    var gamePath = Registry.GetValue(keyName: pathKey.Path, valueName: pathKey.Key, defaultValue: null)
                        ?.ToString();
                    if (gamePath == null)
                    {
                        return;
                    }

                    var gameName = entry.Name;
                    var nameKey = entry.Registry.FirstOrDefault(key => key.Usage == "Name");
                    if (nameKey != null)
                    {
                        gameName = Registry.GetValue(keyName: nameKey.Path,
                                valueName: pathKey.Key,
                                defaultValue: gameName)
                            ?.ToString();
                    }

                    var gamePublisher = entry.Publisher;
                    var publisherKey = entry.Registry.FirstOrDefault(key => key.Usage == "Publisher");
                    if (publisherKey != null)
                    {
                        gamePublisher = Registry.GetValue(keyName: publisherKey.Path,
                                valueName: publisherKey.Key,
                                defaultValue: gamePublisher)
                            ?.ToString();
                    }

                    var gameInstall = gamePath;
                    var installKey = entry.Registry.FirstOrDefault(key => key.Usage == "Path");
                    if (installKey != null)
                    {
                        gameInstall = Registry.GetValue(keyName: installKey.Path,
                                valueName: installKey.Key,
                                defaultValue: gameInstall)
                            ?.ToString();
                    }

                    var configName = gameName?.Replace(oldValue: " ", newValue: string.Empty).Trim();
                    gameList.Add(new GameSettings
                    {
                        Name = gameName, GamePath = gameInstall, Publisher = gamePublisher, ConfigName = configName,
                    });
                });
            return gameList;
        }
    }
}