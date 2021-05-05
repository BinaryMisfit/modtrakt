namespace Senselessly.Foolish.ModTrakt.Wpf.Models.App
{
    using Interface.Models;

    public class GameRegistry : IGameRegistry
    {
        public string Root { get; set; }

        public string Path { get; set; }

        public string Key { get; set; }

        public string Usage { get; set; }
    }
}