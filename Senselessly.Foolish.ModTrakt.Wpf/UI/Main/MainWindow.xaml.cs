namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Main
{
    public partial class MainWindow
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}