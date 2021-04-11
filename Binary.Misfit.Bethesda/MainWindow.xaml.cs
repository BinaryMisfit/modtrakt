namespace Binary.Misfit.Bethesda
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.IO;

    public partial class MainWindow
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        public MainWindow()
        {
            _mainWindowViewModel = new MainWindowViewModel();
            DataContext = _mainWindowViewModel;
            InitializeComponent();
        }

        private void ActionClick(object sender,
            RoutedEventArgs e)
        {
            var stageFolders = Directory.EnumerateDirectories(@"D:\Mods\fallout4", "*.*").ToList();
            _mainWindowViewModel.Summary = $"{stageFolders.Count()} folder(s) found";
            _mainWindowViewModel.OnPropertyChanged(nameof(_mainWindowViewModel.Summary));
            stageFolders.OrderBy(stageFolder => stageFolder)
                .ToList()
                .ForEach(stageFolder =>
                {
                    _mainWindowViewModel.Status = $"{stageFolder}";
                    _mainWindowViewModel.OnPropertyChanged(nameof(_mainWindowViewModel.Status));
                    var stageFolderInfo = new DirectoryInfo(stageFolder);
                    var stageFiles = Directory.EnumerateFileSystemEntries(stageFolder);
                    ModPath.Text += $"{stageFolderInfo.Name}\n";
                });
        }

        private void ExitClick(object sender,
            EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}