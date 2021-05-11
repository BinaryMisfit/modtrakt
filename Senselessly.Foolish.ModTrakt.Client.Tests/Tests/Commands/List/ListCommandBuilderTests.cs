namespace Senselessly.Foolish.ModTrakt.Client.Tests.Tests.Commands.List
{
    using System.Collections.Generic;
    using System.IO.Abstractions.TestingHelpers;
    using System.Linq;
    using Bethesda.Archives.Services;
    using Client.Commands.List;
    using FluentAssertions;
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
            command.Should().NotBeNull();
        }

        [Fact]
        public void ListCommandBuilder_Sets_Description()
        {
            var command = _builder.BuildCommand();
            command.Description.Should().Match(Resources.Command_List_Description);
        }

        [Fact]
        public void ListCommandBuilder_Sets_Alias()
        {
            const int expectedCount = 2;
            var expectedList = new List<string> {"list", "li"};
            var command = _builder.BuildCommand();
            command.Aliases.Should().NotBeEmpty().And.HaveCount(expectedCount).And.ContainInOrder(expectedList);
        }

        [Fact]
        public void ListCommandBuilder_Sets_Handler()
        {
            var command = _builder.BuildCommand();
            command.Should().NotBeNull();
        }

        [Fact]
        public void ListCommand_Has_Correct_Argument_Count()
        {
            const int expected = 1;
            var command = _builder.BuildCommand();
            command.Arguments.Should().HaveCount(expected);
        }

        [Fact]
        public void ListCommand_Has_Correct_Arguments()
        {
            var command = _builder.BuildCommand();
            var arg = command.Arguments.Where(a => a.Name == "Path");
            arg.Should().NotBeNull();
        }

        [Fact]
        public void ListCommand_Has_Correct_Option_Count()
        {
            const int expected = 2;
            var command = _builder.BuildCommand();
            command.Options.Should().HaveCount(expected);
        }

        [Fact]
        public void ListCommand_Has_Correct_Options()
        {
            const int expectedCount = 2;
            var command = _builder.BuildCommand();
            command.Options.Should()
                   .NotBeEmpty()
                   .And.HaveCount(expectedCount)
                   .And.SatisfyRespectively(option => option.Name.Should().Match("type"),
                        option => option.Name.Should().Match("recurse"));
        }
    }
}