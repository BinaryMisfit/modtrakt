namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using System.Collections.Generic;
    using Interfaces.Settings;

    internal sealed class AppSettings : IAppSettings
    {
        private IGameSettings _active;

        public AppSettings(IGameSettings game, ISettings settings)
        {
            Game = game;
            Settings = settings;
        }

        public IGameSettings Game
        {
            get => _active;
            set {
                if (string.IsNullOrEmpty(value.Code)) { return; }

                _active = value;
                Settings.ActiveGame = _active.Code;
            }
        }

        public IEnumerable<IGameSettings> Installed { get; set; }

        public ISettings Settings { get; }
    }
}