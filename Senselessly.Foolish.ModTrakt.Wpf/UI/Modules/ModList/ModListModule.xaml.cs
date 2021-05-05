namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Modules.ModList
{
    using Microsoft.Toolkit.Mvvm.DependencyInjection;

    public partial class ModListModule
    {
        public ModListModule()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<ModListViewModel>();
        }
    }
}