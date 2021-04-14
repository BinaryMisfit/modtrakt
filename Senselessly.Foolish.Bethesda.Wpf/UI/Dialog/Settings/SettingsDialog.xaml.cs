namespace Senselessly.Foolish.Bethesda.Wpf.UI.Dialog.Settings
{
    public partial class SettingsDialog
    {
        public SettingsDialog(SettingsViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}