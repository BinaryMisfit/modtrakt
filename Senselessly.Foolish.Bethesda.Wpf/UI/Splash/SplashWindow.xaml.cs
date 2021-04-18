namespace Senselessly.Foolish.Bethesda.Wpf.UI.Splash
{
    using MaterialDesignExtensions.Controls;

    public partial class SplashWindow : MaterialWindow
    {
        public SplashWindow(SplashWindowViewModel viewModel)
        {

            DataContext = viewModel;
            InitializeComponent();
        }
    }
}