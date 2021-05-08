namespace Senselessly.Foolish.Bethesda.Archives.Tests.Services.PluginLocatorService
{
    using System.Linq;
    using Archives.Services;
    using Enums;
    using Fixtures;
    using Xunit;

    [Collection("PluginLocationServiceLocateArchive")]
    [Trait(name: "Category", value: "PluginLocatorService")]
    public class PluginLocatorServiceLocateArchiveResultTests : IClassFixture<FileSystemFixture>
    {
        private readonly FileSystemFixture _fileSystem;

        public PluginLocatorServiceLocateArchiveResultTests(FileSystemFixture fileSystem) => _fileSystem = fileSystem;

        [Fact]
        public void PluginLocatorService_Locate_Returns_Archive_Empty_Results()
        {
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModEmptyRoot);
            var results = service.Locate(path: path, type: ModTypes.Archive, recurse: false);
            Assert.Empty(results);
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Archive_Not_Found_Empty_Results()
        {
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModPluginsRoot);
            var results = service.Locate(path: path, type: ModTypes.Archive, recurse: false);
            Assert.Empty(results);
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Archive_Results()
        {
            const int expected = 6;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Archive, recurse: false);
            Assert.Equal(expected: expected, actual: results.Count());
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Archive_Results_Are_Correct()
        {
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Archive, recurse: false);
            Assert.Collection(collection: results,
                result => Assert.Equal(expected: FileSystemFixture.ModArchive01, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModArchive02, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModArchive03, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModArchive05, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModArchive06, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModArchive08, actual: result));
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Archive_Recursive_Result()
        {
            const int expected = 8;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Archive, recurse: true);
            Assert.Equal(expected: expected, actual: results.Count());
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Archive_Recursive_Results_Are_Correct()
        {
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Archive, recurse: true);
            Assert.Collection(collection: results,
                result => Assert.Equal(expected: FileSystemFixture.ModArchive01, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModArchive02, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModArchive03, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModArchive04, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModArchive05, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModArchive06, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModArchive07, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModArchive08, actual: result));
        }
    }
}