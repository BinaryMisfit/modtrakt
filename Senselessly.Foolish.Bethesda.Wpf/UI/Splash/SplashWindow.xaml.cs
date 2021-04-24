namespace Senselessly.Foolish.Bethesda.Wpf.UI.Splash
{
    public partial class SplashWindow
    {
        public SplashWindow(SplashWindowViewModel viewModel)
        {

            DataContext = viewModel;
            InitializeComponent();
        }
    }
}