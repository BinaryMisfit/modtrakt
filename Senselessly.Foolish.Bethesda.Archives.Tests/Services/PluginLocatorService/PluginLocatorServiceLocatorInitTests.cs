namespace Senselessly.Foolish.Bethesda.Archives.Tests.Services.PluginLocatorService
{
    using Archives.Services;
    using Enums;
    using Fixtures;
    using Xunit;

    [Collection("PluginLocationServiceLocatorInit")]
    [Trait(name: "Category", value: "PluginLocatorService")]
    public class PluginLocatorServiceLocatorInitTests : IClassFixture<FileSystemFixture>
    {
        private const string SearchFileRoot = "C:\\Mods";
        private readonly FileSystemFixture _fileSystem;

        public PluginLocatorServiceLocatorInitTests(FileSystemFixture fileSystem) => _fileSystem = fileSystem;

        [Fact]
        public void PluginLocatorService_Locator_Sets_ModType()
        {
            const ModTypes expected = ModTypes.Plugin;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(SearchFileRoot);
            service.Locate(path: path, type: expected, recurse: false);
            Assert.Equal(expected: expected, actual: service.SearchType);
        }

        [Fact]
        public void PluginLocatorService_Locator_Sets_SearchPath()
        {
            var expected = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(SearchFileRoot);
            var service = new PluginLocatorService(_fileSystem.Storage);
            service.Locate(path: expected, type: ModTypes.Both, recurse: false);
            Assert.Equal(expected: expected, actual: service.SearchPath);
        }

        [Fact]
        public void PluginLocatorService_Locator_Sets_Recurse()
        {
            const bool expected = true;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(SearchFileRoot);
            service.Locate(path: path, type: ModTypes.Both, recurse: expected);
            Assert.True(service.SearchRecursive);
        }
    }
}