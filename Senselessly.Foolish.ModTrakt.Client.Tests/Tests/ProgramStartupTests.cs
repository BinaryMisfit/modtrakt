namespace Senselessly.Foolish.ModTrakt.Client.Tests.Tests
{
    using FluentAssertions;
    using Helpers;
    using Xunit;

    [Collection("ProgramStartup")]
    [Trait(name: "Category", value: "ProgramTests")]
    public sealed class ProgramStartupTests : IClassFixture<ProgramFixture>
    {
        private readonly ProgramFixture _program;

        public ProgramStartupTests(ProgramFixture program) => _program = program;

        [Fact]
        public async void Program_Startup_No_Args_Return_1()
        {
            const int expected = 1;
            var result = await Program.Main();
            result.Should().Be(expected);
        }

        [Fact]
        public async void Program_Startup_No_Services_Return_255()
        {
            const int expected = 255;
            var result = await _program.Run();
            result.Should().Be(expected);
        }
    }
}