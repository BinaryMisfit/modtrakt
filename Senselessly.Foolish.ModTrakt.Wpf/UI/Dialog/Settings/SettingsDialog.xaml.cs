namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Dialog.Settings
{
    using Microsoft.Toolkit.Mvvm.DependencyInjection;

    public partial class SettingsDialog
    {
        public SettingsDialog()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<SettingsViewModel>();
        }
    }
}