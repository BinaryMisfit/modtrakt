namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Splash
{
    using System.Windows;
    using Context.Models;
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.Messaging;

    public partial class SplashWindow
    {
        public SplashWindow()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<SplashViewModel>();
            Loaded += WindowLoaded;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Register<WindowCloseMessage>(recipient: this,
                handler: (r, m) => {
                    var options = m.Value;
                    if (options.Source != typeof(SplashViewModel)) { return; }

                    if (options.Close) { Close(); }

                    if (options.Shutdown) { Application.Current.Shutdown(); }

                    WeakReferenceMessenger.Default.Unregister<WindowCloseMessage>(this);
                });
        }
    }
}