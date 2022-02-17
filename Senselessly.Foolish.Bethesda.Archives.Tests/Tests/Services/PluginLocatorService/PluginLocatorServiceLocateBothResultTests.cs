namespace Senselessly.Foolish.Bethesda.Archives.Tests.Tests.Services.PluginLocatorService
{
    using Archives.Services;
    using Enums;
    using FluentAssertions;
    using Helpers.Fixtures;
    using Xunit;

    [Collection("PluginLocationServiceLocateArchive")]
    [Trait("Category", "PluginLocatorService")]
    public class PluginLocatorServiceLocateBothResultTests : IClassFixture<FileSystemFixture>
    {
        private readonly FileSystemFixture _fileSystem;

        public PluginLocatorServiceLocateBothResultTests(FileSystemFixture fileSystem) => _fileSystem = fileSystem;

        [Fact]
        public void PluginLocatorService_Locate_Returns_Both_Not_Found_Empty_Results()
        {
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModEmptyRoot);
            var results = service.Locate(path, ModTypes.Both, false);
            results.Should().BeEmpty();
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Both_Results()
        {
            const int expected = 10;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path, ModTypes.Both, false);
            results.Should().HaveCount(expected);
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Both_Results_Are_Correct()
        {
            const int expectedCount = 10;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path, ModTypes.Both, false);
            results.Should()
                   .NotBeEmpty()
                   .And.HaveCount(expectedCount)
                   .And.SatisfyRespectively(result => result.Should().Match(FileSystemFixture.ModPlugin01),
                        result => result.Should().Match(FileSystemFixture.ModPlugin03),
                        result => result.Should().Match(FileSystemFixture.ModPlugin04),
                        result => result.Should().Match(FileSystemFixture.ModPlugin05),
                        result => result.Should().Match(FileSystemFixture.ModArchive01),
                        result => result.Should().Match(FileSystemFixture.ModArchive02),
                        result => result.Should().Match(FileSystemFixture.ModArchive03),
                        result => result.Should().Match(FileSystemFixture.ModArchive05),
                        result => result.Should().Match(FileSystemFixture.ModArchive06),
                        result => result.Should().Match(FileSystemFixture.ModArchive08));
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Both_Recursive_Result()
        {
            const int expected = 14;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path, ModTypes.Both, true);
            results.Should().HaveCount(expected);
        }

        [Fact]
        public void PluginLocatorService_Locate_Returns_Both_Recursive_Results_Are_Correct()
        {
            const int expectedCount = 14;
            var service = new PluginLocatorService(_fileSystem.Storage);
            var path = _fileSystem.Storage.DirectoryInfo.FromDirectoryName(FileSystemFixture.ModRoot);
            var results = service.Locate(path, ModTypes.Both, true);
            results.Should()
                   .NotBeEmpty()
                   .And.HaveCount(expectedCount)
                   .And.SatisfyRespectively(result => result.Should().Match(FileSystemFixture.ModPlugin01),
                        result => result.Should().Match(FileSystemFixture.ModPlugin02),
                        result => result.Should().Match(FileSystemFixture.ModPlugin03),
                        result => result.Should().Match(FileSystemFixture.ModPlugin04),
                        result => result.Should().Match(FileSystemFixture.ModPlugin05),
                        result => result.Should().Match(FileSystemFixture.ModPlugin06),
                        result => result.Should().Match(FileSystemFixture.ModArchive01),
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