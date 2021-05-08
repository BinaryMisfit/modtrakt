namespace Senselessly.Foolish.Bethesda.Archives.Tests.Services.PluginLocatorService
{
    using System.IO.Abstractions.TestingHelpers;
    using Archives.Services;
    using Xunit;

    [Collection("PluginLocationServiceInit")]
    [Trait(name: "Category", value: "PluginLocatorService")]
    public class PluginLocatorServiceInitTests
    {
        [Fact]
        public void PluginLocatorService_Accepts_IFileSystem()
        {
            var service = new PluginLocatorService(new MockFileSystem());
            Assert.True(service.IsLocatorReady());
        }
    }
}