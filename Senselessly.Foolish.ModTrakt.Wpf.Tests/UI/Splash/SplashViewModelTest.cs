namespace Senselessly.Foolish.ModTrakt.Wpf.Tests.UI.Splash
{
    using Fixtures;
    using Xunit;

    [Trait(name: "Category", value: "UI")]
    public class SplashViewModelTest : IClassFixture<SplashWindowFixture>
    {
        private readonly SplashWindowFixture _splash;

        public SplashViewModelTest(SplashWindowFixture splash) => _splash = splash;

        [Fact]
        public void SplashViewModel_CanSetStatusMessage()
        {
            const string expected = "Status Message Set";
            _splash.ViewModel.Status = expected;
            Assert.Equal(expected: expected, actual: _splash.ViewModel.Status);
        }
    }
}