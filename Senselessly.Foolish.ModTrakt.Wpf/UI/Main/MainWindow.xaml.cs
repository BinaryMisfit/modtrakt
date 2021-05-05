namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Main
{
    using Microsoft.Toolkit.Mvvm.DependencyInjection;

    internal partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<MainViewModel>();
        }
    }
}