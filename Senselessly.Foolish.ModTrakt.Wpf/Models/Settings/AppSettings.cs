namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using Interfaces.Settings;

    internal sealed class AppSettings : IAppSettings
    {
        private IGameSettings _active;

        public AppSettings(IFileSystem storage, IGameSettings game, ISettings settings)
        {
            var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            ProductFolder = storage.DirectoryInfo.FromDirectoryName(AppContext.BaseDirectory);
            UserFolder = storage.DirectoryInfo.FromDirectoryName(userFolder);
            Game = game;
            Settings = settings;
        }

        public bool Configured { get; set; }

        public IGameSettings Game
        {
            get => _active;
            set {
                if (string.IsNullOrEmpty(value.Code)) return;

                _active = value;
                Settings.ActiveGame = _active.Code;
            }
        }

        public IEnumerable<IGameSettings> Installed { get; set; }

        public IDirectoryInfo ProductFolder { get; }

        public ISettings Settings { get; }

        public IDirectoryInfo UserFolder { get; }
    }
}