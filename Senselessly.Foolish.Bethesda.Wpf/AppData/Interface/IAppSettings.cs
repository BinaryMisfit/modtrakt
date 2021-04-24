namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Interface
{
    public interface IAppSettings
    {
        ISettings Settings { get; }

        IGameSettings Game { get; }

        bool Missing =>
            Settings?.ActiveGame == null;

        bool PromptGame =>
            Settings?.ActiveGame == null || Settings.SelectGame;
    }
}