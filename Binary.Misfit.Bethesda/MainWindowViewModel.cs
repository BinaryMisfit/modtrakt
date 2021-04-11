namespace Binary.Misfit.Bethesda
{
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;

    public sealed class MainWindowViewModel : ObservableObject
    {
        private readonly RelayCommand _performAction;
        private readonly RelayCommand _exitCommand;
        private string _log;
        private string _status;
        private string _summary;

        public MainWindowViewModel()
        {
            _performAction = new RelayCommand(OnPerformAction, CanPerformAction);
            _exitCommand = new RelayCommand(OnExitApp, CanExitApp);
        }

        public string Log
        {
            get =>
                _log;
            set =>
                SetProperty(field: ref _log, newValue: value);
        }

        public string Status
        {
            get =>
                _status;
            set =>
                SetProperty(field: ref _status, newValue: value);
        }

        public string Summary
        {
            get =>
                _summary;
            set =>
                SetProperty(field: ref _summary, newValue: value);
        }

        public ICommand PerformAction =>
            _performAction;

        public ICommand ExitApp =>
            _exitCommand;

        private void OnPerformAction()
        {
            var stageFolders = Directory.EnumerateDirectories(@"D:\Mods\fallout4", "*.*").ToList();
            Summary = $"{stageFolders.Count()} folder(s) found";
            stageFolders.OrderBy(stageFolder => stageFolder)
                .ToList()
                .ForEach(stageFolder =>
                {
                    Status = $"{stageFolder}";
                    var stageFolderInfo = new DirectoryInfo(stageFolder);
                    var stageFiles = Directory.EnumerateFileSystemEntries(stageFolder);
                    Log += $"{stageFolderInfo.Name}\n";
                });
        }

        private bool CanPerformAction() =>
            true;

        private void OnExitApp() =>
            Application.Current.Shutdown();

        private bool CanExitApp() =>
            true;
    }
}