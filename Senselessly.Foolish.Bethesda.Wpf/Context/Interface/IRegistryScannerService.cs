namespace Senselessly.Foolish.Bethesda.Wpf.Context.Interface
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    public interface IRegistryScannerService
    {
        IEnumerable<RegistryResult> Results { get; }

        Task<bool> ReadAsync(string root, string path, CancellationToken cancel = default, params string[] keys);
    }
}