namespace Senselessly.Foolish.ModTrakt.Client.Tests.Tests
{
    using System.CommandLine;
    using System.CommandLine.IO;
    using System.IO.Abstractions.TestingHelpers;
    using System.Reflection;
    using System.Threading.Tasks;
    using Bethesda.Archives.Services;
    using Client.Commands;
    using Client.Commands.List;
    using FluentAssertions;
    using Xunit;

    [Collection("ProgramOutput")]
    [Trait(name: "Category", value: "ProgramTests")]
    public class ProgramOutputTests
    {
        private static readonly string Version = (Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly())
                                                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                               ?.InformationalVersion;

        private readonly RootCommandBuilder _builder =
            new RootCommandBuilder(new ListCommandBuilder(new PluginLocatorService(new MockFileSystem())));

        private readonly IConsole _console = new TestConsole();

        [Fact]
        public async Task Program_Arg_Question_Returns_Help()
        {
            const string expected = "*Options:*--version*Show version information*";
            var rootCommand = _builder.BuildCommand();
            await rootCommand.InvokeAsync(commandLine: "-?", console: _console);
            _console.Out.ToString().Should().Match(expected);
        }

        [Fact]
        public async Task Program_Arg_H_Returns_Help()
        {
            const string expected = "*Options:*--version*Show version information*";
            var rootCommand = _builder.BuildCommand();
            await rootCommand.InvokeAsync(commandLine: "-h", console: _console);
            _console.Out.ToString().Should().Match(expected);
        }

        [Fact]
        public async Task Program_Arg_Help_Returns_Help()
        {
            const string expected = "*Options:*--version*Show version information*";
            var rootCommand = _builder.BuildCommand();
            await rootCommand.InvokeAsync(commandLine: "--help", console: _console);
            _console.Out.ToString().Should().Match(expected);
        }

        [Fact]
        public async Task Program_Arg_None_Returns_Help()
        {
            const string expected = "*Options:*--version*Show version information*";
            var rootCommand = _builder.BuildCommand();
            await rootCommand.InvokeAsync(commandLine: "", console: _console);
            _console.Out.ToString().Should().Match(expected);
        }

        [Fact]
        public async Task Program_Arg_Version_Returns_Version()
        {
            var rootCommand = _builder.BuildCommand();
            await rootCommand.InvokeAsync(commandLine: "--version", console: _console);
            _console.Out.ToString().Should().Match($"{Version}\r\n");
        }
    }
}