namespace Senselessly.Foolish.Bethesda.Wpf.UI.Dialog.Settings
{
    using System.Windows.Input;
    using AppData.Interface;
    using MaterialDesignExtensions.Controls;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;

    public sealed class SettingsViewModel : ObservableObject
    {
        private bool _canBrowse = true;
        private readonly RelayCommand _browseStaging;
        private readonly RelayCommand _browseWorking;
        private string _stagingFolder;
        private string _workingFolder;

        public SettingsViewModel(ISettings settings)
        {
            _browseStaging = new RelayCommand(execute: OnLoadStaging, canExecute: CanLoadStaging);
            _browseWorking = new RelayCommand(execute: OnLoadWorking, canExecute: CanLoadWorking);
            LoadSettings(settings);
        }

        public ICommand BrowseStaging =>
            _browseStaging;

        public ICommand BrowseWorking =>
            _browseWorking;

        public string StagingFolder
        {
            get =>
                _stagingFolder;
            set =>
                SetProperty(field: ref _stagingFolder, newValue: value);
        }

        public string WorkingFolder
        {
            get =>
                _workingFolder;
            set =>
                SetProperty(field: ref _workingFolder, newValue: value);
        }

        public ISettings Settings { get; private set; }

        private async void OnLoadStaging()
        {
            _canBrowse = false;
            var dialogArgs = new OpenDirectoryDialogArguments()
            {
                Width = 600, Height = 400, CreateNewDirectoryEnabled = false, CurrentDirectory = _stagingFolder,
            };
            var result = await OpenDirectoryDialog.ShowDialogAsync(dialogHostName: "SettingsDialog", args: dialogArgs);
            if (!result.Canceled)
            {
                Settings.StagingFolder = result.Directory;
                StagingFolder = Settings.StagingFolder;
            }

            _canBrowse = true;
        }

        private bool CanLoadStaging()
        {
            return _canBrowse;
        }

        private async void OnLoadWorking()
        {
            _canBrowse = false;
            var dialogArgs = new OpenDirectoryDialogArguments()
            {
                Width = 600, Height = 400, CreateNewDirectoryEnabled = true, CurrentDirectory = _workingFolder,
            };
            var result = await OpenDirectoryDialog.ShowDialogAsync(dialogHostName: "SettingsDialog", args: dialogArgs);
            if (!result.Canceled)
            {
                Settings.WorkingFolder = result.Directory;
                WorkingFolder = Settings.WorkingFolder;
            }

            _canBrowse = true;
        }

        private bool CanLoadWorking()
        {
            return _canBrowse;
        }

        private void LoadSettings(ISettings settings)
        {
            Settings = settings;
            StagingFolder = settings.StagingFolder;
            WorkingFolder = settings.WorkingFolder;
        }
    }
}