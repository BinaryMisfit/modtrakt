namespace Senselessly.Foolish.Bethesda.Wpf.UI.Models
{
    using System;
    using System.IO;
    using SharpConfig;

    public class Settings
    {
        private const string SettingsFileName = @"\ModTrakt\modtrakt.ini";

        public static string SettingsPath =>
            $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}{SettingsFileName}";

        public string StagingFolder { get; set; }

        public string WorkingFolder { get; set; }

        public static Settings Load(string settingsFile)
        {
            if (!File.Exists(settingsFile))
            {
                return new Settings();
            }

            var configuration = Configuration.LoadFromFile(settingsFile);
            return configuration["Settings"].ToObject<Settings>();
        }

        public static void Save(Settings settings,
            string settingsFile)
        {
            if (!File.Exists(settingsFile))
            {
                var appData = Path.GetDirectoryName(settingsFile);
                if (!Directory.Exists(appData))
                {
                    Directory.CreateDirectory(appData);
                }
            }

            var configuration = new Configuration
            {
                Section.FromObject(name: "Settings", obj: settings),
            };
            configuration.SaveToFile(settingsFile);
        }
    }
}