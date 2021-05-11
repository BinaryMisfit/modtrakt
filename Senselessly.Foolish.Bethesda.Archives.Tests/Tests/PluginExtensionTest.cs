namespace Senselessly.Foolish.Bethesda.Archives.Tests.Tests
{
    using Archives.Models.Files;
    using Extensions;
    using Helpers.Fixtures;
    using Xunit;

    [Collection("PluginExtension")]
    [Trait(name: "Category", value: "PluginExtension")]
    public class PluginExtensionTest
    {
        [Fact]
        public void Plugin_ParseModId_Returns_Id()
        {
            const int expected = 2;
            var plugin = new Plugin(FileSystemFixture.ModRootSub01);
            var actual = plugin.ParseModId();
            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact]
        public void Plugin_ParseModId_Returns_Negative_On_Null()
        {
            const int expected = -1;
            var plugin = new Plugin(null);
            var actual = plugin.ParseModId();
            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact]
        public void Plugin_ParseModId_Returns_Negative_On_Missing()
        {
            const int expected = -1;
            var plugin = new Plugin(FileSystemFixture.ModRootSub02);
            var actual = plugin.ParseModId();
            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact]
        public void Plugin_ParseModName_Returns_Name()
        {
            const string expected = "Plugin Two";
            var plugin = new Plugin(FileSystemFixture.ModRootSub01);
            var actual = plugin.ParseModName();
            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact]
        public void Plugin_ParseModName_Returns_Null()
        {
            var plugin = new Plugin(null);
            var actual = plugin.ParseModName();
            Assert.Null(actual);
        }
    }
}