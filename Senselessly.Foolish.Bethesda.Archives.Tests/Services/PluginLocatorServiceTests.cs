namespace Senselessly.Foolish.Bethesda.Archives.Tests.Services
{
    using System.IO.Abstractions.TestingHelpers;
    using Archives.Services;
    using Xunit;

    public class PluginLocatorServiceTests
    {
        [Fact]
        public void PluginLocatorServiceAcceptsFileSystem()
        {
            var service = new PluginLocatorService(new MockFileSystem());
            Assert.True(service.IsLocatorReady());
        }
    }
}