namespace Senselessly.Foolish.Bethesda.Wpf.UI.Splash
{
    using Microsoft.Toolkit.Mvvm.DependencyInjection;

    public partial class SplashWindow
    {
        public SplashWindow()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<SplashWindowViewModel>();
        }
    }
}