namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Settings
{
    using System;

    public static class Config
    {
        public const string GameDictionaryKey = "GameDictionary";

        private const string SettingsFileName = @"\ModTrakt\modtrakt.ini";

        public static string SettingsPath
        {
            get => $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}{SettingsFileName}";
        }
    }
}