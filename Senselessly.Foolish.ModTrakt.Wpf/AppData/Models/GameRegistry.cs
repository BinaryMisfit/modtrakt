namespace Senselessly.Foolish.ModTrakt.Wpf.AppData.Models
{
    using Interface;

    public class GameRegistry : IGameRegistry
    {
        public string Root { get; set; }

        public string Path { get; set; }

        public string Key { get; set; }

        public string Usage { get; set; }
    }
}