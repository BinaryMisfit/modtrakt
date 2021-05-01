namespace Senselessly.Foolish.Bethesda.Wpf.Tests.Fixtures
{
    using System;
    using Wpf.UI.Splash;

    public sealed class SplashWindowFixture : IDisposable
    {
        public SplashWindowFixture() => ViewModel = new SplashViewModel();

        public SplashViewModel ViewModel { get; private set; }

        public void Dispose()
        {
            ViewModel = null;
        }
    }
}