namespace Senselessly.Foolish.Bethesda.Wpf.Services.Interface
{
    using System.Collections.Generic;
    using Models;

    public interface IRegistryScanner
    {
        IEnumerable<RegistryResult> Results { get; }

        bool Read(string path, params string[] keys);
    }
}