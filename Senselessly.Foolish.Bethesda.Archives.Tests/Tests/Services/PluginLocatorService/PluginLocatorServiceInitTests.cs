namespace Senselessly.Foolish.Bethesda.Archives.Tests.Tests.Services.PluginLocatorService
{
    using Archives.Services;
    using FluentAssertions;
    using Helpers.Fixtures;
    using Xunit;

    [Collection("PluginLocationServiceInit")]
    [Trait("Category", "PluginLocatorService")]
    public sealed class PluginLocatorServiceInitTests : IClassFixture<FileSystemFixture>
    {
        private readonly FileSystemFixture _fileSystem;

        public PluginLocatorServiceInitTests(FileSystemFixture fileSystem) => _fileSystem = fileSystem;

        [Fact]
        public void PluginLocatorService_Accepts_IFileSystem()
        {
            var service = new PluginLocatorService(_fileSystem.Storage);
            service.IsLocatorReady().Should().BeTrue();
        }
    }
}