namespace Binary.Misfit.Bethesda
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