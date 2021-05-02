namespace Senselessly.Foolish.ModTrakt.Wpf.Tests.AppData.Default
{
    using System;
    using Wpf.AppData.Default;
    using Xunit;

    [Trait(name: "Category", value: "Configuration")]
    public class ConfigTest
    {
        [Fact]
        public void ConfigReturnsCorrectGameDictionaryKey()
        {
            Assert.Equal(expected: "GameDictionary", actual: Config.GameDictionaryKey);
        }

        [Fact]
        public void ConfigReturnsCorrectSettingsPath()
        {
            var expected =
                $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\ModTrakt\\modtrakt.ini";
            Assert.Equal(expected: expected, actual: Config.SettingsPath);
        }
    }
}