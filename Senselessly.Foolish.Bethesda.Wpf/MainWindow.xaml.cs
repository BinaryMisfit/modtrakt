namespace Senselessly.Foolish.Bethesda.Wpf
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            var mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;
            InitializeComponent();
        }
    }
}