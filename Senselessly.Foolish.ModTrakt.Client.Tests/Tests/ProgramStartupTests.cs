namespace Senselessly.Foolish.ModTrakt.Client.Tests.Tests
{
    using FluentAssertions;
    using Xunit;

    [Collection("ProgramStartup")]
    [Trait(name: "Category", value: "ProgramTests")]
    public sealed class ProgramStartupTests
    {
        [Fact]
        public async void Program_Startup_No_Args_Return_1()
        {
            const int expected = 1;
            var result = await Program.Main();
            result.Should().Be(expected);
        }
    }
}