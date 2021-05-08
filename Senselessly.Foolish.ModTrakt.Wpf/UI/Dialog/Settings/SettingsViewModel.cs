namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Dialog.Settings
{
    using System.Windows.Input;
    using Interfaces.Settings;
    using MaterialDesignExtensions.Controls;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;

    internal sealed class SettingsViewModel : ObservableObject
    {
        private readonly RelayCommand _browseStaging;
        private readonly RelayCommand _browseWorking;
        private bool _canBrowse = true;
        private string _stagingFolder;
        private string _workingFolder;

        public SettingsViewModel() { }

        public SettingsViewModel(IAppSettings settings)
        {
            _browseStaging = new RelayCommand(execute: OnBrowseStaging, canExecute: CanBrowseStaging);
            _browseWorking = new RelayCommand(execute: OnBrowseWorking, canExecute: CanBrowseWorking);
            AppSettings = SetProperties(settings);
        }

        public ICommand BrowseStaging
        {
            get => _browseStaging;
        }

        public ICommand BrowseWorking
        {
            get => _browseWorking;
        }

        public string StagingFolder
        {
            get => _stagingFolder;
            set => SetProperty(field: ref _stagingFolder, newValue: value);
        }

        public string WorkingFolder
        {
            get => _workingFolder;
            set => SetProperty(field: ref _workingFolder, newValue: value);
        }

        public IAppSettings AppSettings { get; }

        private async void OnBrowseStaging()
        {
            _canBrowse = false;
            var dialogArgs = new OpenDirectoryDialogArguments {
                Width = 600, Height = 400, CreateNewDirectoryEnabled = false, CurrentDirectory = _stagingFolder
            };
            await OpenDirectoryDialog.ShowDialogAsync(dialogHostName: "SettingsDialog", args: dialogArgs);
            _canBrowse = true;
        }

        private bool CanBrowseStaging() => _canBrowse;

        private async void OnBrowseWorking()
        {
            _canBrowse = false;
            var dialogArgs = new OpenDirectoryDialogArguments {
                Width = 600, Height = 400, CreateNewDirectoryEnabled = true, CurrentDirectory = _workingFolder
            };
            await OpenDirectoryDialog.ShowDialogAsync(dialogHostName: "SettingsDialog", args: dialogArgs);
            _canBrowse = true;
        }

        private bool CanBrowseWorking() => _canBrowse;

        private IAppSettings SetProperties(IAppSettings appSettings) => appSettings;
    }
}