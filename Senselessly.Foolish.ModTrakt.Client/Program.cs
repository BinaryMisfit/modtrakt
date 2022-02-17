using System.IO.Abstractions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Senselessly.Foolish.Bethesda.Archives.Interfaces.Services;
using Senselessly.Foolish.Bethesda.Archives.Services;
using Senselessly.Foolish.ModTrakt.Client.Commands;
using Senselessly.Foolish.ModTrakt.Client.Commands.List;
using Senselessly.Foolish.ModTrakt.Client.Conceptual;
using Senselessly.Foolish.ModTrakt.Client.Interfaces.Commands;

namespace Senselessly.Foolish.ModTrakt.Client
{
    internal class Program : ConsoleProgram
    {
        internal static async Task<int> Main(params string[] args)
        {
            var program = new Program();
            return await program.RunAsync(args);
        }

        internal override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddScoped<IPluginLocatorService, PluginLocatorService>();
            services.AddScoped<ICommandBuilder, ListCommandBuilder>();
            services.AddSingleton<IRootCommandBuilder, RootCommandBuilder>();
        }
    }
}
