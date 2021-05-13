namespace Senselessly.Foolish.ModTrakt.Client.Tests.Helpers
{
    using System;
    using Conceptual;
    using Microsoft.Extensions.DependencyInjection;

    public class ProgramFixture : ConsoleProgram, IDisposable
    {
        public void Dispose() { }

        internal override void ConfigureServices(IServiceCollection services) { }
    }
}