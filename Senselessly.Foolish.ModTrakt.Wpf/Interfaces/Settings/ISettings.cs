namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Settings
{
    internal interface ISettings
    {
        string ActiveGame { get; set; }

        bool SelectGame { get; set; }

        string LastGame { get; set; }
    }
}