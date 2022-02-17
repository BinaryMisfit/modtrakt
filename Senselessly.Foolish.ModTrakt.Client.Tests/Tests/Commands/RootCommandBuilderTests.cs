namespace Senselessly.Foolish.ModTrakt.Client.Tests.Tests.Commands
{
    using System.IO.Abstractions.TestingHelpers;
    using System.Linq;
    using Bethesda.Archives.Services;
    using Client.Commands;
    using Client.Commands.List;
    using FluentAssertions;
    using Properties;
    using Xunit;

    [Collection("RootCommandBuilder")]
    [Trait("Category", "CommandOptionsBuilderTests")]
    public sealed class RootCommandBuilderTests
    {
        private readonly RootCommandBuilder _builder =
            new RootCommandBuilder(new ListCommandBuilder(new PluginLocatorService(new MockFileSystem())));

        [Fact]
        public void RootCommandBuilder_Builds_Command()
        {
            var command = _builder.BuildCommand();
            command.Should().NotBeNull();
        }

        [Fact]
        public void RootCommandBuilder_Sets_Description()
        {
            var command = _builder.BuildCommand();
            command.Description.Should().Be(Resources.Application_Description);
        }

        [Fact]
        public void RootCommand_Has_Correct_Command_Count()
        {
            const int expected = 1;
            var command = _builder.BuildCommand();
            command.Children.Should().HaveCount(expected);
        }

        [Fact]
        public void RootCommand_Has_Command_List()
        {
            var command = _builder.BuildCommand();
            var list = command.Children.FirstOrDefault(c => c.Name == "list");
            list.Should().NotBeNull();
        }
    }
}