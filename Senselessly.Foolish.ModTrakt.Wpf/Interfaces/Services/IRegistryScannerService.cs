namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Models.App;

    internal interface IRegistryScannerService
    {
        IEnumerable<RegistryResult> Results { get; }

        Task<bool> ReadAsync(string root, string path, CancellationToken cancel = default, params string[] keys);
    }
}