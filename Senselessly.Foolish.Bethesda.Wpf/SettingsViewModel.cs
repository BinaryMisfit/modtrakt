namespace Senselessly.Foolish.Bethesda.Wpf
{
    using System.Windows.Input;
    using MaterialDesignExtensions.Controls;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;

    public class SettingsViewModel : ObservableObject
    {
        private bool _canBrowse = true;
        private readonly RelayCommand _browseStaging;
        private readonly RelayCommand _browseWorking;
        private string _stagingFolder;
        private string _workingFolder;

        public SettingsViewModel()
        {
            _browseStaging = new RelayCommand(execute: OnLoadStaging, canExecute: CanLoadStaging);
            _browseWorking = new RelayCommand(execute: OnLoadWorking, canExecute: CanLoadWorking);
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

        private async void OnLoadStaging()
        {
            _canBrowse = false;
            var dialogArgs = new OpenDirectoryDialogArguments()
            {
                Width = 600, Height = 400, CreateNewDirectoryEnabled = false,
            };

            var result = await OpenDirectoryDialog.ShowDialogAsync(dialogHostName: "SettingsDialog", args: dialogArgs);
            if (!result.Canceled)
            {
                StagingFolder = result.Directory;
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
                Width = 600, Height = 400, CreateNewDirectoryEnabled = true,
            };

            var result = await OpenDirectoryDialog.ShowDialogAsync(dialogHostName: "SettingsDialog", args: dialogArgs);
            if (!result.Canceled)
            {
                WorkingFolder = result.Directory;
            }

            _canBrowse = true;
        }

        private bool CanLoadWorking()
        {
            return _canBrowse;
        }
    }
}