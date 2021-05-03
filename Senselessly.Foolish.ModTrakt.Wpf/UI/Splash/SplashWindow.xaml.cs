namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Splash
{
    using Microsoft.Toolkit.Mvvm.DependencyInjection;

    public partial class SplashWindow
    {
        public SplashWindow()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<SplashViewModel>();
        }
    }
}