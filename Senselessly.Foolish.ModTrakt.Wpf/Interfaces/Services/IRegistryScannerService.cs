namespace Senselessly.Foolish.ModTrakt.Wpf.Interface.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Wpf.Models.App;

    public interface IRegistryScannerService
    {
        IEnumerable<RegistryResult> Results { get; }

        Task<bool> ReadAsync(string root, string path, CancellationToken cancel = default, params string[] keys);
    }
}