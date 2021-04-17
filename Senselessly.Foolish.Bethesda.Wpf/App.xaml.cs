namespace Senselessly.Foolish.Bethesda.Wpf
{
    using System.IO.Abstractions;
    using System.Windows;
    using AppData.Default;
    using Microsoft.Extensions.DependencyInjection;
    using UI.Dialog.Settings;
    using UI.Main;
    using UI.Splash;

    public partial class App
    {
        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            Shared.Provider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<SettingsDialog>();
            services.AddSingleton<SplashWindowViewModel>();
            services.AddSingleton<SplashWindow>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainWindow>();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var splashWindow = Shared.Provider.GetService<SplashWindow>();
            splashWindow?.Show();
        }
    }
}