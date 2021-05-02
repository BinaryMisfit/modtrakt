namespace Senselessly.Foolish.ModTrakt.Wpf.AppData.Default
{
    using System;

    public static class Config
    {
        public const string GameDictionaryKey = "GameDictionary";

        private const string SettingsFileName = @"\ModTrakt\modtrakt.ini";

        public static string SettingsPath =>
            $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}{SettingsFileName}";
    }
}