namespace Senselessly.Foolish.Bethesda.Archives.Tests.Services.PluginLocatorService
{
    using Archives.Services;
    using Enums;
    using Fixtures;
    using Xunit;

    [Collection("PluginLocationServiceLocateInit")]
    [Trait(name: "Category", value: "PluginLocatorService")]
    public sealed class PluginLocatorServiceLocateInitTests : IClassFixture<FileSystemFixture>
    {
        private readonly FileSystemFixture _fileSystem;

        public PluginLocatorServiceLocateInitTests(FileSystemFixture fileSystem) => _fileSystem = fileSystem;

        [Fact]
        public void PluginLocatorService_Locate_Sets_ModType()
        {
            const ModTypes expected = ModTypes.Plugin;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            service.Locate(path: path, type: expected, recurse: false);
            Assert.Equal(expected: expected, actual: service.SearchType);
        }

        [Fact]
        public void PluginLocatorService_Locate_Sets_SearchPath()
        {
            var expected = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var service = new PluginLocatorService(_fileSystem.Storage);
            service.Locate(path: expected, type: ModTypes.Both, recurse: false);
            Assert.Equal(expected: expected, actual: service.SearchPath);
        }

        [Fact]
        public void PluginLocatorService_Locate_Sets_Recurse()
        {
            const bool expected = true;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            service.Locate(path: path, type: ModTypes.Both, recurse: expected);
            Assert.True(service.SearchRecursive);
        }
    }
}