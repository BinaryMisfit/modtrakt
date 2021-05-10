namespace Senselessly.Foolish.Bethesda.Archives.Tests.Tests.Services.PluginLocatorService
{
    using System.Linq;
    using Archives.Services;
    using Enums;
    using Helpers.Fixtures;
    using Xunit;

    [Collection("PluginLocationServiceLocateArchive")]
    [Trait(name: "Category", value: "PluginLocatorService")]
    public class PluginLocatorServiceLocateBothResultTests : IClassFixture<FileSystemFixture>
    {
        private readonly FileSystemFixture _fileSystem;

        public PluginLocatorServiceLocateBothResultTests(FileSystemFixture fileSystem) => _fileSystem = fileSystem;

        [Fact]
        public void PluginLocatorService_Locate_Returns_Both_Not_Found_Empty_Results()
        {
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModEmptyRoot);
            var results = service.Locate(path: path, type: ModTypes.Both, recurse: false);
            Assert.Empty(results);
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Both_Results()
        {
            const int expected = 10;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Both, recurse: false);
            Assert.Equal(expected: expected, actual: results.Count());
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Both_Results_Are_Correct()
        {
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Both, recurse: false);
            Assert.Collection(collection: results,
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin01, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin03, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin04, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin05, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModArchive01, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModArchive02, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModArchive03, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModArchive05, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModArchive06, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModArchive08, actual: result));
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Both_Recursive_Result()
        {
            const int expected = 14;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Both, recurse: true);
            Assert.Equal(expected: expected, actual: results.Count());
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Both_Recursive_Results_Are_Correct()
        {
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Both, recurse: true);
            Assert.Collection(collection: results,
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin01, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin02, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin03, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin04, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin05, actual: result),
                result => Assert.Equal(expected: FileSystemFixture.ModPlugin06, actual: result),
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