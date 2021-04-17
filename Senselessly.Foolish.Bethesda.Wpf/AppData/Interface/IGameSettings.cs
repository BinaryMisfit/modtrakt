namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Interface
{
    public interface IGameSettings
    {
        string Name { get; set; }

        string Publisher { get; set; }

        string GamePath { get; set; }
    }
}