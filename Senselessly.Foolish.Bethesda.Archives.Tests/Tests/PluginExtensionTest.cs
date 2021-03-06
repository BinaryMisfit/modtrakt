namespace Senselessly.Foolish.Bethesda.Archives.Tests.Tests
{
    using Archives.Models.Files;
    using Extensions;
    using FluentAssertions;
    using Helpers.Fixtures;
    using Xunit;

    [Collection("PluginExtension")]
    [Trait("Category", "PluginExtension")]
    public class PluginExtensionTest
    {
        [Fact]
        public void Plugin_ParseModId_Returns_Id()
        {
            const int expected = 2;
            var plugin = new Plugin(FileSystemFixture.ModRootSub01);
            var actual = plugin.ParseModId();
            actual.Should<int>().Be(expected);
        }

        [Fact]
        public void Plugin_ParseModId_Returns_Negative_On_Null()
        {
            const int expected = -1;
            var plugin = new Plugin(null);
            var actual = plugin.ParseModId();
            actual.Should<int>().Be(expected);
        }

        [Fact]
        public void Plugin_ParseModId_Returns_Negative_On_Missing()
        {
            const int expected = -1;
            var plugin = new Plugin(FileSystemFixture.ModRootSub02);
            var actual = plugin.ParseModId();
            actual.Should<int>().Be(expected);
        }

        [Fact]
        public void Plugin_ParseModName_Returns_Name()
        {
            const string expected = "Plugin Two";
            var plugin = new Plugin(FileSystemFixture.ModRootSub01);
            var actual = plugin.ParseModName();
            actual.Should<string>().Be(expected);
        }

        [Fact]
        public void Plugin_ParseModName_Returns_Null()
        {
            var plugin = new Plugin(null);
            var actual = plugin.ParseModName();
            actual.Should().BeNull();
        }
    }
}