namespace Senselessly.Foolish.ModTrakt.Client.Tests.Tests.Commands.List
{
    using System.CommandLine;
    using System.CommandLine.IO;
    using System.IO.Abstractions.TestingHelpers;
    using System.Threading.Tasks;
    using Bethesda.Archives.Services;
    using Client.Commands;
    using Client.Commands.List;
    using FluentAssertions;
    using Xunit;

    [Collection("ProgramListOutput")]
    [Trait(name: "Category", value: "ProgramTests")]
    public class ListCommandOutputTests
    {
        private readonly RootCommandBuilder _builder =
            new RootCommandBuilder(new ListCommandBuilder(new PluginLocatorService(new MockFileSystem())));

        private readonly IConsole _console = new TestConsole();

        [Fact]
        public async Task Program_Arg_Alias_Li_Returns_Help()
        {
            var expected = $"list*{Properties.Resources.Command_List_Description}*";
            var rootCommand = _builder.BuildCommand();
            await rootCommand.InvokeAsync(commandLine: "li", console: _console);
            _console.Out.ToString().Should().Match(expected);
        }

        [Fact]
        public async Task Program_Arg_Alias_List_Returns_Help()
        {
            var expected = $"list*{Properties.Resources.Command_List_Description}*";
            var rootCommand = _builder.BuildCommand();
            await rootCommand.InvokeAsync(commandLine: "list", console: _console);
            _console.Out.ToString().Should().Match(expected);
        }

        [Fact]
        public async Task Program_Arg_List_Path_Invalid_Returns_Error()
        {
            const string expected = "Directory does not exist: C:\\Mods\\\r\n\r\n";
            var rootCommand = _builder.BuildCommand();
            await rootCommand.InvokeAsync(commandLine: "list C:\\Mods\\", console: _console);
            _console.Error.ToString().Should().Match(expected);
        }
    }
}