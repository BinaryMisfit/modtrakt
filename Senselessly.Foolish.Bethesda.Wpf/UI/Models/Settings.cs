namespace Senselessly.Foolish.Bethesda.Wpf.UI.Models
{
    using AppData.Interface;

    public class Settings : ISettings
    {
        public string StagingFolder { get; set; }

        public string WorkingFolder { get; set; }
    }
}