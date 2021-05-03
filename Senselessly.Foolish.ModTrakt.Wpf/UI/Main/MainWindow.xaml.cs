namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Main
{
    using Microsoft.Toolkit.Mvvm.DependencyInjection;

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<MainViewModel>();
        }
    }
}