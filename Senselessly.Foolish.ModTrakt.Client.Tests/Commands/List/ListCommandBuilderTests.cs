namespace Senselessly.Foolish.ModTrakt.Client.Tests.Commands.List
{
    using System.IO.Abstractions.TestingHelpers;
    using Bethesda.Archives.Services;
    using Client.Commands.List;
    using Properties;
    using Xunit;

    [Collection("ListCommandBuilder")]
    [Trait(name: "Category", value: "CommandOptionsBuilderTests")]
    public sealed class ListCommandBuilderTests
    {
        private readonly ListCommandBuilder _builder =
            new ListCommandBuilder(new PluginLocatorService(new MockFileSystem()));

        [Fact]
        public void ListCommandBuilder_Builds_Command()
        {
            var command = _builder.BuildCommand();
            Assert.NotNull(command);
        }

        [Fact]
        public void ListCommandBuilder_Sets_Description()
        {
            var command = _builder.BuildCommand();
            Assert.Equal(expected: Resources.Command_List_Description, actual: command.Description);
        }
    }
}