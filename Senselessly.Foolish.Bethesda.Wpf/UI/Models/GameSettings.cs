namespace Senselessly.Foolish.Bethesda.Wpf.UI.Models
{
    using System.Collections.Generic;

    public class GameSettings
    {
        public string Game { get; set; }

        public string Publisher { get; set; }

        public IEnumerable<GameRegistry> Registry { get; set; }
    }
}