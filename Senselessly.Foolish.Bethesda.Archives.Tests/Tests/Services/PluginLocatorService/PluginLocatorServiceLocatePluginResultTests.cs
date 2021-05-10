namespace Senselessly.Foolish.Bethesda.Archives.Tests.Tests.Services.PluginLocatorService
{
    using System.Linq;
    using Archives.Services;
    using Enums;
    using Helpers.Fixtures;
    using Xunit;

    [Collection("PluginLocationServiceLocatePlugins")]
    [Trait(name: "Category", value: "PluginLocatorService")]
    public sealed class PluginLocatorServiceLocatePluginResultTests : IClassFixture<FileSystemFixture>
    {
        private readonly FileSystemFixture _fileSystem;

        public PluginLocatorServiceLocatePluginResultTests(FileSystemFixture fileSystem) => _fileSystem = fileSystem;

        [Fact]
        public void PluginLocatorService_Locate_Returns_Plugin_Empty_Results()
        {
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModEmptyRoot);
            var results = service.Locate(path: path, type: ModTypes.Plugin, recurse: false);
            Assert.Empty(results);
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Plugin_Not_Found_Empty_Results()
        {
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModArchivesRoot);
            var results = service.Locate(path: path, type: ModTypes.Plugin, recurse: false);
            Assert.Empty(results);
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Plugin_Results()
        {
            const int expected = 4;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Plugin, recurse: false);
            Assert.Equal(expected: expected, actual: results.Count());
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Plugin_Results_Are_Correct()
        {
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Plugin, recurse: false);
            Assert.Collection(collection: results,
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin01, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin03, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin04, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin05, actual: result));
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Plugin_Recursive_Results()
        {
            const int expected = 6;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Plugin, recurse: true);
            Assert.Equal(expected: expected, actual: results.Count());
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Plugin_Recursive_Results_Are_Correct()
        {
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Plugin, recurse: true);
            Assert.Collection(collection: results,
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin01, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin02, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin03, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin04, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin05, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin06, actual: result));
        }
    }
}