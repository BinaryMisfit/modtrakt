namespace Senselessly.Foolish.Bethesda.Wpf.Services.Game
{
    public struct GameLocatorArgs
    {
        public string Game { get; }
        public int Current { get; }
        public int Remaining { get; }

        public GameLocatorArgs(string game, int current, int remaining)
        {
            Game = game;
            Current = current;
            Remaining = remaining;
        }
    }
}