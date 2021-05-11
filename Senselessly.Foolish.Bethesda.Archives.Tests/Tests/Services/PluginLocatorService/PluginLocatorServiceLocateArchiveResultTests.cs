namespace Senselessly.Foolish.Bethesda.Archives.Tests.Tests.Services.PluginLocatorService
{
    using Archives.Services;
    using Enums;
    using FluentAssertions;
    using Helpers.Fixtures;
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
            results.Should().BeEmpty();
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Archive_Not_Found_Empty_Results()
        {
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModPluginsRoot);
            var results = service.Locate(path: path, type: ModTypes.Archive, recurse: false);
            results.Should().BeEmpty();
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Archive_Results()
        {
            const int expected = 6;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Archive, recurse: false);
            results.Should().HaveCount(expected);
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Archive_Results_Are_Correct()
        {
            const int expectedCount = 6;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Archive, recurse: false);
            results.Should()
                   .NotBeEmpty()
                   .And.HaveCount(expectedCount)
                   .And.SatisfyRespectively(result => result.Should().Match(FileSystemFixture.ModArchive01),
                        result => result.Should().Match(FileSystemFixture.ModArchive02),
                        result => result.Should().Match(FileSystemFixture.ModArchive03),
                        result => result.Should().Match(FileSystemFixture.ModArchive05),
                        result => result.Should().Match(FileSystemFixture.ModArchive06),
                        result => result.Should().Match(FileSystemFixture.ModArchive08));
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Archive_Recursive_Result()
        {
            const int expected = 8;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Archive, recurse: true);
            results.Should().HaveCount(expected);
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Archive_Recursive_Results_Are_Correct()
        {
            const int expectedCount = 8;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path: path, type: ModTypes.Archive, recurse: true);
            results.Should()
                   .NotBeEmpty()
                   .And.HaveCount(expectedCount)
                   .And.SatisfyRespectively(result => result.Should().Match(FileSystemFixture.ModArchive01),
                        result => result.Should().Match(FileSystemFixture.ModArchive02),
                        result => result.Should().Match(FileSystemFixture.ModArchive03),
                        result => result.Should().Match(FileSystemFixture.ModArchive04),
                        result => result.Should().Match(FileSystemFixture.ModArchive05),
                        result => result.Should().Match(FileSystemFixture.ModArchive06),
                        result => result.Should().Match(FileSystemFixture.ModArchive07),
                        result => result.Should().Match(FileSystemFixture.ModArchive08));
        }
    }
}