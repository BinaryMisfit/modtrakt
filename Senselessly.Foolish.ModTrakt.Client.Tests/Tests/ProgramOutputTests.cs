namespace Senselessly.Foolish.ModTrakt.Client.Tests.Tests
{
    using System.CommandLine;
    using System.CommandLine.IO;
    using System.IO.Abstractions.TestingHelpers;
    using Bethesda.Archives.Services;
    using Client.Commands;
    using Client.Commands.List;
    using FluentAssertions;
    using Xunit;

    [Collection("ProgramOutput")]
    [Trait(name: "Category", value: "ProgramTests")]
    public class ProgramOutputTests
    {
        private readonly RootCommandBuilder _builder =
            new RootCommandBuilder(new ListCommandBuilder(new PluginLocatorService(new MockFileSystem())));

        private readonly IConsole _console = new TestConsole();

        [Fact]
        public async void Program_Returns_No_Args_Help_Text()
        {
            const string expected = "*Options:*--version*Show version information*";
            var rootCommand = _builder.BuildCommand();
            await rootCommand.InvokeAsync(commandLine: "", console: _console);
            _console.Out.ToString().Should().Match(expected);
        }
    }
}