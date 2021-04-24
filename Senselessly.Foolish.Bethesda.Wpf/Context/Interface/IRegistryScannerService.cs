namespace Senselessly.Foolish.Bethesda.Wpf.Context.Interface
{
    using System.Collections.Generic;
    using Models;

    public interface IRegistryScannerService
    {
        IEnumerable<RegistryResult> Results { get; }

        bool Read(string path, params string[] keys);
    }
}