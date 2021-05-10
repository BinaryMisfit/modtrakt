namespace Senselessly.Foolish.Bethesda.Archives.Tests.Tests.Models.Files
{
    using Archives.Models.Files;
    using Helpers.Fixtures;
    using Xunit;

    [Collection("PluginInit")]
    [Trait(name: "Category", value: "PluginInit")]
    public class PluginInitTests
    {
        [Fact]
        public void Plugin_Accepts_FolderName()
        {
            const string expected = FileSystemFixture.ModRootSub01;
            var plugin = new Plugin(expected);
            Assert.Equal(expected: expected, actual: plugin.FolderName);
        }

        [Fact]
        public void Plugin_Sets_ModId_Found()
        {
            const int expected = 2;
            var plugin = new Plugin(FileSystemFixture.ModRootSub01);
            Assert.Equal(expected: expected, actual: plugin.ModId);
        }

        [Fact]
        public void Plugin_Sets_ModId_Missing()
        {
            const int expected = -1;
            var plugin = new Plugin(FileSystemFixture.ModRootSub02);
            Assert.Equal(expected: expected, actual: plugin.ModId);
        }

        [Fact]
        public void Plugin_Sets_ModName_Found()
        {
            const string expected = "Plugin Two";
            var plugin = new Plugin(FileSystemFixture.ModRootSub01);
            Assert.Equal(expected: expected, actual: plugin.ModName);
        }

        [Fact]
        public void Plugin_Sets_ModName_Missing_Folder_Name()
        {
            const string expected = "Plugin06";
            var plugin = new Plugin(FileSystemFixture.ModRootSub02);
            Assert.Equal(expected: expected, actual: plugin.ModName);
        }
    }
}