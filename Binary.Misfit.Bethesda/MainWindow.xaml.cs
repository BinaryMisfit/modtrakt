﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Binary.Misfit.Bethesda
{
    using System.IO;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ActionClick(object sender,
            RoutedEventArgs e)
        {
            var stageFolders = Directory.EnumerateDirectories(@"D:\Mods\fallout4", "*.*");
            directoryCount.Content = $"{stageFolders.Count()} folder(s) found";
            stageFolders.OrderBy(stageFolder => stageFolder)
                .ToList()
                .ForEach(stageFolder =>
                {
                    status.Content = $"{stageFolder}";
                    var stageFolderInfo = new DirectoryInfo(stageFolder);
                    var stageFiles = Directory.EnumerateFileSystemEntries(stageFolder);
                    modPath.Text += $"{stageFolderInfo.Name}\n";
                });
        }
    }
}