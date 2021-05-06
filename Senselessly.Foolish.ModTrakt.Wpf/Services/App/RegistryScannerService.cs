namespace Senselessly.Foolish.ModTrakt.Wpf.Services.App
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using DotNetWindowsRegistry;
    using Interfaces.Services;
    using Microsoft.Win32;
    using Models.App;

    internal sealed class RegistryScannerService : IRegistryScannerService
    {
        private const string RegistryResult = "Result";
        private readonly IExceptionService _ex;
        private readonly IRegistry _registry;

        public RegistryScannerService(IRegistry registry, IExceptionService ex)
        {
            _ex = ex;
            _registry = registry;
        }

        public IEnumerable<RegistryResult> Results { get; private set; }

        public async Task<bool> ReadAsync(
            string root,
            string path,
            CancellationToken cancel = default,
            params string[] keys)
        {
            List<RegistryResult> results = null;
            try
            {
                var hive = root switch {
                    "HKEY_LOCAL_MACHINE" => RegistryHive.LocalMachine,
                    var _                => RegistryHive.CurrentUser
                };
                using var registryLocal = _registry.OpenBaseKey(hKey: hive, view: RegistryView.Registry64);
                using var registryGame = registryLocal.OpenSubKey(path);
                if (registryGame == null) { return false; }

                await foreach (var key in keys.ToAsyncEnumerable().WithCancellation(cancel))
                {
                    var registryValue = registryGame.GetValue(name: $"{key}", defaultValue: null);
                    if (registryValue == null) { continue; }

                    var result = new RegistryResult(id: RegistryResult,
                        registry: new GameRegistry {Key = key, Path = path, Root = root}) {Value = registryValue};
                    results ??= new List<RegistryResult>();
                    results.Add(result);
                }
            }
            catch (Exception e)
            {
                _ex?.Send(new ExceptionInfo(sourceName: nameof(RegistryScannerService),
                    source: typeof(RegistryScannerService),
                    e: e));
            }

            if (!cancel.IsCancellationRequested) { Results = results; }

            return Results != null;
        }
    }
}