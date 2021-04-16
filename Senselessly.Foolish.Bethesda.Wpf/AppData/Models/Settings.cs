namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Models
{
    using Interface;

    public class Settings : ISettings
    {
        public string ActiveGame { get; set; }

        public bool SelectGame { get; set; }

        public string LastGame { get; set; }
    }
}