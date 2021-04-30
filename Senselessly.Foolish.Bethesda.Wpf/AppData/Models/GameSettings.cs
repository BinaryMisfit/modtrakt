namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Models
{
    using Interface;

    public class GameSettings : IGameSettings
    {
        public string Code { get; set; }
        
        public string Name { get; set; }

        public string GamePath { get; set; }
    }
}