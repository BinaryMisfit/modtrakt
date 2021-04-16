namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Interface
{
    public interface ISettings
    {
        string ActiveGame { get; set; }

        bool SelectGame { get; set; }
        
        string LastGame { get; set; }
    }
}