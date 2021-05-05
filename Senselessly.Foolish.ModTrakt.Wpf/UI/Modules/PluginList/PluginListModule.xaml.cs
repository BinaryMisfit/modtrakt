namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Modules.PluginList
{
    using Microsoft.Toolkit.Mvvm.DependencyInjection;

    public partial class PluginListModule
    {
        public PluginListModule()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<PluginListViewModel>();
        }
    }
}