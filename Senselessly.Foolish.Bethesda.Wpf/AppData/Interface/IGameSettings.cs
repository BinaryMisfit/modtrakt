namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Interface
{
    public interface IGameSettings
    {
        string Code { get; set; }
        
        string Name { get; set; }

        string GamePath { get; set; }
    }
}