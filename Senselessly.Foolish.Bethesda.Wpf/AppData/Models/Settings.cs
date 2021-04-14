namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Models
{
    using Interface;

    public class Settings : ISettings
    {
        public string StagingFolder { get; set; }

        public string WorkingFolder { get; set; }
    }
}