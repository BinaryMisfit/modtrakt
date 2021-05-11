namespace Senselessly.Foolish.Bethesda.Archives.Tests.Tests.Services.PluginLocatorService
{
    using Archives.Services;
    using Enums;
    using FluentAssertions;
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
            results.Should().BeEmpty();
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Plugin_Not_Found_Empty_Results()
        {
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModArchivesRoot);
            var results = service.Locate(path: path, type: ModTypes.Plugin, recurse: false);
            results.Should().BeEmpty();
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Plugin_Results()
        {
            const int expected = 4;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Plugin, recurse: false);
            results.Should().HaveCount(expected);
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Plugin_Results_Are_Correct()
        {
            const int expectedCount = 4;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Plugin, recurse: false);
            results.Should()
                   .NotBeEmpty()
                   .And.HaveCount(expectedCount)
                   .And.SatisfyRespectively(result => result.Should().Match(FileSystemFixture.ModPlugin01),
                        result => result.Should().Match(FileSystemFixture.ModPlugin03),
                        result => result.Should().Match(FileSystemFixture.ModPlugin04),
                        result => result.Should().Match(FileSystemFixture.ModPlugin05));
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Plugin_Recursive_Results()
        {
            const int expected = 6;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Plugin, recurse: true);
            results.Should().HaveCount(expected);
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Plugin_Recursive_Results_Are_Correct()
        {
            const int expectedCount = 6;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Plugin, recurse: true);
            results.Should()
                   .NotBeEmpty()
                   .And.HaveCount(expectedCount)
                   .And.SatisfyRespectively(result => result.Should().Match(FileSystemFixture.ModPlugin01),
                        result => result.Should().Match(FileSystemFixture.ModPlugin02),
                        result => result.Should().Match(FileSystemFixture.ModPlugin03),
                        result => result.Should().Match(FileSystemFixture.ModPlugin04),
                        result => result.Should().Match(FileSystemFixture.ModPlugin05),
                        result => result.Should().Match(FileSystemFixture.ModPlugin06));
        }
    }
}