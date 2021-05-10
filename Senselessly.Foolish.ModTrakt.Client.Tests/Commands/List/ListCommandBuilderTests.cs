namespace Senselessly.Foolish.ModTrakt.Client.Tests.Commands.List
{
    using System.IO.Abstractions.TestingHelpers;
    using System.Linq;
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

        [Fact]
        public void ListCommandBuilder_Sets_Alias()
        {
            var command = _builder.BuildCommand();
            Assert.Collection(collection: command.Aliases,
                a => Assert.Equal(expected: "list", actual: a),
                a => Assert.Equal(expected: "li", actual: a));
        }

        [Fact]
        public void ListCommandBuilder_Sets_Handler()
        {
            var command = _builder.BuildCommand();
            Assert.NotNull(command.Handler);
        }

        [Fact]
        public void ListCommand_Has_Correct_Argument_Count()
        {
            const int expected = 1;
            var command = _builder.BuildCommand();
            Assert.Equal(expected: expected, actual: command.Arguments.Count);
        }

        [Fact]
        public void ListCommand_Has_Correct_Arguments()
        {
            var command = _builder.BuildCommand();
            var arg = command.Arguments.Where(a => a.Name == "Path");
            Assert.NotNull(arg);
        }

        [Fact]
        public void ListCommand_Has_Correct_Option_Count()
        {
            const int expected = 2;
            var command = _builder.BuildCommand();
            Assert.Equal(expected: expected, actual: command.Options.Count);
        }

        [Fact]
        public void ListCommand_Has_Correct_Options()
        {
            var command = _builder.BuildCommand();
            Assert.Collection(collection: command.Options,
                o => {
                    Assert.Collection(collection: o.Aliases,
                        a => Assert.Equal(expected: "-t", actual: a),
                        a => Assert.Equal(expected: "--type", actual: a));
                },
                o => {
                    Assert.Collection(collection: o.Aliases,
                        a => Assert.Equal(expected: "-r", actual: a),
                        a => Assert.Equal(expected: "--recurse", actual: a));
                });
        }
    }
}