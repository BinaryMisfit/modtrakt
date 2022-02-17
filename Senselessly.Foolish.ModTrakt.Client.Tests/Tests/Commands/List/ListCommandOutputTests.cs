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
    using Helpers;
    using Xunit;

    [Collection("ProgramListOutput")]
    [Trait("Category", "ProgramTests")]
    public class ListCommandOutputTests : IClassFixture<FileSystemFixture>
    {
        private readonly RootCommandBuilder _builder =
            new RootCommandBuilder(new ListCommandBuilder(new PluginLocatorService(new MockFileSystem())));

        private readonly IConsole _console = new TestConsole();

        private readonly FileSystemFixture _fileSystem;

        public ListCommandOutputTests(FileSystemFixture fileSystem) => _fileSystem = fileSystem;

        [Fact]
        public async Task Program_Arg_Alias_Li_Returns_Help()
        {
            var expected = $"list*{Properties.Resources.Command_List_Description}*";
            var rootCommand = _builder.BuildCommand();
            await rootCommand.InvokeAsync("li", _console);
            _console.Out.ToString().Should().Match(expected);
        }

        [Fact]
        public async Task Program_Arg_Alias_List_Returns_Help()
        {
            var expected = $"list*{Properties.Resources.Command_List_Description}*";
            var rootCommand = _builder.BuildCommand();
            await rootCommand.InvokeAsync("list", _console);
            _console.Out.ToString().Should().Match(expected);
        }

        [Fact]
        public async Task Program_Arg_List_Path_Invalid_Returns_Error()
        {
            const string expected = "Directory does not exist: C:\\Mods\\\r\n\r\n";
            var rootCommand = _builder.BuildCommand();
            await rootCommand.InvokeAsync("list C:\\Mods\\", _console);
            _console.Error.ToString().Should().Match(expected);
        }
    }
}