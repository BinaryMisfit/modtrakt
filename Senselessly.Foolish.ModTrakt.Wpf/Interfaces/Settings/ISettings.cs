namespace Senselessly.Foolish.ModTrakt.Wpf.Interface.Settings
{
    public interface ISettings
    {
        string ActiveGame { get; set; }

        bool SelectGame { get; set; }

        string LastGame { get; set; }
    }
}