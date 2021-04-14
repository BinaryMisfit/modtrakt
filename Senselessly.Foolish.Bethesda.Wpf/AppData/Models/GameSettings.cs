namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Models
{
    using System.Collections.Generic;
    using Interface;

    public class GameSettings : IGameSettings
    {
        public string Game { get; set; }

        public string Publisher { get; set; }

        public IEnumerable<GameRegistry> Registry { get; set; }
    }
}