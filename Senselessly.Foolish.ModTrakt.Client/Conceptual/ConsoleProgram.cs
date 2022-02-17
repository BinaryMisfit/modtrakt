using System.CommandLine;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Senselessly.Foolish.ModTrakt.Client.Interfaces.Commands;

namespace Senselessly.Foolish.ModTrakt.Client.Conceptual
{
    public abstract class ConsoleProgram
    {
        internal async Task<int> RunAsync(params string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var provider = services.BuildServiceProvider();
            var builder = provider.GetService<IRootCommandBuilder>();
            if (builder == null) { return 255; }

            var rootCommand = builder.BuildCommand();
            return await rootCommand.InvokeAsync(args);
        }

        internal abstract void ConfigureServices(IServiceCollection services);
    }
}
