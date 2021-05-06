namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Controls.ExceptionBox
{
    using Microsoft.Toolkit.Mvvm.DependencyInjection;

    internal partial class ExceptionBox
    {
        public ExceptionBox()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<ExceptionViewModel>();
        }
    }
}