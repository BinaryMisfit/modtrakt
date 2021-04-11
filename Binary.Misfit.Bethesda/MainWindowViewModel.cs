namespace Binary.Misfit.Bethesda
{
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;

    public sealed class MainWindowViewModel : ViewModelBase
    {
        private readonly DelegateCommand _performAction;
        private readonly DelegateCommand _exitCommand;
        private string _log;
        private string _status;
        private string _summary;

        public MainWindowViewModel()
        {
            _performAction = new DelegateCommand(executeAction: OnPerformAction, canExecuteAction: CanPerformAction);
            _exitCommand = new DelegateCommand(executeAction: OnExitApp, canExecuteAction: CanExitApp);
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

        private void OnPerformAction(object command)
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

        private bool CanPerformAction(object command) =>
            true;

        private void OnExitApp(object command) =>
            Application.Current.Shutdown();

        private bool CanExitApp(object command) =>
            true;
    }
}