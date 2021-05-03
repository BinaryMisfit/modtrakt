namespace Senselessly.Foolish.ModTrakt.Wpf.UI.GameList
{
    using Microsoft.Toolkit.Mvvm.DependencyInjection;

    public partial class GameListWindow
    {
        public GameListWindow()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<GameListViewModel>();
        }
    }
}