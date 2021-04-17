namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Interface
{
    public interface IAppSettings
    {
        ISettings Settings { get; }

        IGameSettings Game { get; }
    }
}