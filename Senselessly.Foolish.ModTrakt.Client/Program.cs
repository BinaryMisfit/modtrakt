﻿namespace Senselessly.Foolish.ModTrakt.Client
{
    using System.CommandLine;
    using System.IO.Abstractions;
    using System.Threading.Tasks;
    using Bethesda.Archives.Helpers;
    using Bethesda.Archives.Interfaces;
    using Commands;
    using Commands.Interfaces;
    using Commands.List;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    internal class Program
    {
        private static ServiceProvider Provider { get; set; }

        public static async Task<int> Main(params string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            Provider = services.BuildServiceProvider();
            var logger = Provider.GetService<ILoggerFactory>()?.CreateLogger<Program>();
            logger?.LogDebug("Test");
            var builder = Provider.GetService<IRootCommandBuilder>();
            if (builder == null) { return 0; }

            var rootCommand = builder.BuildCommand();
            return await rootCommand.InvokeAsync(args);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddScoped<IPluginLocator, PluginLocator>();
            services.AddScoped<ICommandBuilder, ListCommandBuilder>();
            services.AddSingleton<IRootCommandBuilder, RootCommandBuilder>();
        }
    }
}