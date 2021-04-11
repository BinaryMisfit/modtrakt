using System;
using System.Linq;
using System.Windows;

namespace Binary.Misfit.Bethesda
{
    using System.IO;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ActionClick(object sender,
            RoutedEventArgs e)
        {
            var stageFolders = Directory.EnumerateDirectories(@"D:\Mods\fallout4", "*.*").ToList();
            DirectoryCount.Content = $"{stageFolders.Count()} folder(s) found";
            stageFolders.OrderBy(stageFolder => stageFolder)
                .ToList()
                .ForEach(stageFolder =>
                {
                    Status.Content = $"{stageFolder}";
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