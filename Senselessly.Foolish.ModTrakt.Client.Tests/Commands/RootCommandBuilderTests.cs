namespace Senselessly.Foolish.ModTrakt.Client.Tests.Commands
{
    using System.IO.Abstractions.TestingHelpers;
    using System.Linq;
    using Bethesda.Archives.Services;
    using Client.Commands;
    using Client.Commands.List;
    using Properties;
    using Xunit;

    [Collection("RootCommandBuilder")]
    [Trait(name: "Category", value: "CommandOptionTest")]
    public sealed class RootCommandBuilderTests
    {
        private readonly RootCommandBuilder _builder =
            new RootCommandBuilder(new ListCommandBuilder(new PluginLocatorService(new MockFileSystem())));

        [Fact]
        public void RootCommandBuilder_Builds_Command()
        {
            var command = _builder.BuildCommand();
            Assert.NotNull(command);
        }

        [Fact]
        public void RootCommandBuilder_Sets_Description()
        {
            var command = _builder.BuildCommand();
            Assert.Equal(expected: Resources.Application_Description, actual: command.Description);
        }

        [Fact]
        public void RootCommandBuilder_Has_Correct_Command_Count()
        {
            var command = _builder.BuildCommand();
            Assert.Single(command.Children);
        }

        [Fact]
        public void RootCommandBuilder_Has_Command_List()
        {
            var command = _builder.BuildCommand();
            var list = command.Children.FirstOrDefault(c => c.Name == "list");
            Assert.NotNull(list);
        }
    }
}