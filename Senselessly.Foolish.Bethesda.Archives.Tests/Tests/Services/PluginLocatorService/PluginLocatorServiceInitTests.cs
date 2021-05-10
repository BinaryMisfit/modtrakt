namespace Senselessly.Foolish.Bethesda.Archives.Tests.Tests.Services.PluginLocatorService
{
    using Archives.Services;
    using Helpers.Fixtures;
    using Xunit;

    [Collection("PluginLocationServiceInit")]
    [Trait(name: "Category", value: "PluginLocatorService")]
    public sealed class PluginLocatorServiceInitTests : IClassFixture<FileSystemFixture>
    {
        private readonly FileSystemFixture _fileSystem;

        public PluginLocatorServiceInitTests(FileSystemFixture fileSystem) => _fileSystem = fileSystem;

        [Fact]
        public void PluginLocatorService_Accepts_IFileSystem()
        {
            var service = new PluginLocatorService(_fileSystem.Storage);
            Assert.True(service.IsLocatorReady());
        }
    }
}