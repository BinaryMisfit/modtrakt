namespace Senselessly.Foolish.Bethesda.Wpf
{
    using System.IO.Abstractions;
    using System.Windows;
    using AppData.Interface;
    using AppData.Models;
    using Context.Interface;
    using Context.Models;
    using Context.Services;
    using DotNetWindowsRegistry;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using UI.Dialog.Settings;
    using UI.GameList;
    using UI.Main;
    using UI.Splash;

    public partial class App
    {
        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            Ioc.Default.ConfigureServices(services.BuildServiceProvider());
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddScoped<IRegistry, WindowsRegistry>();
            services.AddScoped<ISettings, Settings>();
            services.AddScoped<IGameDictionary, GameDictionary>();
            services.AddScoped<IGameLocatorService, GameLocatorService>();
            services.AddScoped<IGameSettings, GameSettings>();
            services.AddSingleton<IAppSettings, AppSettings>();
            services.AddSingleton<IExceptionService, ExceptionService>();
            services.AddSingleton<IRegistryScannerService, RegistryScannerService>();
            services.AddTransient<GameListWindow>();
            services.AddTransient<GameListViewModel>();
            services.AddSingleton<MainWindow>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SplashViewModel>();
            services.AddTransient<SplashWindow>();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Current.DispatcherUnhandledException += (_, args) => {
                WeakReferenceMessenger.Default.Send(new ExceptionInfo(args.Exception));
                args.Handled = true;
            };
            var splashWindow = Ioc.Default.GetService<SplashWindow>();
            splashWindow?.Show();
        }
    }
}