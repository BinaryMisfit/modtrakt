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

        public SettingsViewModel(IAppSettings settings)
        {
            _browseStaging = new RelayCommand(execute: OnBrowseStaging, canExecute: CanBrowseStaging);
            _browseWorking = new RelayCommand(execute: OnBrowseWorking, canExecute: CanBrowseWorking);
            AppSettings = SetProperties(settings);
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

        public IAppSettings AppSettings { get; }

        private async void OnBrowseStaging()
        {
            _canBrowse = false;
            var dialogArgs = new OpenDirectoryDialogArguments()
            {
                Width = 600, Height = 400, CreateNewDirectoryEnabled = false, CurrentDirectory = _stagingFolder,
            };
            var result = await OpenDirectoryDialog.ShowDialogAsync(dialogHostName: "SettingsDialog", args: dialogArgs);
            if (!result.Canceled)
            {
                AppSettings.Settings.StagingFolder = result.Directory;
                StagingFolder = AppSettings.Settings.StagingFolder;
            }

            _canBrowse = true;
        }

        private bool CanBrowseStaging()
        {
            return _canBrowse;
        }

        private async void OnBrowseWorking()
        {
            _canBrowse = false;
            var dialogArgs = new OpenDirectoryDialogArguments()
            {
                Width = 600, Height = 400, CreateNewDirectoryEnabled = true, CurrentDirectory = _workingFolder,
            };
            var result = await OpenDirectoryDialog.ShowDialogAsync(dialogHostName: "SettingsDialog", args: dialogArgs);
            if (!result.Canceled)
            {
                AppSettings.Settings.WorkingFolder = result.Directory;
                WorkingFolder = AppSettings.Settings.WorkingFolder;
            }

            _canBrowse = true;
        }

        private bool CanBrowseWorking()
        {
            return _canBrowse;
        }

        private IAppSettings SetProperties(IAppSettings appSettings)
        {
            StagingFolder = appSettings.Settings.StagingFolder;
            WorkingFolder = appSettings.Settings.WorkingFolder;
            return appSettings;
        }
    }
}