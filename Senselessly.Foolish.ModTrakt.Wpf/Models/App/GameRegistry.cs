namespace Senselessly.Foolish.ModTrakt.Wpf.Models.App
{
    using Interfaces.App;

    internal sealed class GameRegistry : IGameRegistry
    {
        public string Root { get; set; }

        public string Path { get; set; }

        public string Key { get; set; }
    }
}