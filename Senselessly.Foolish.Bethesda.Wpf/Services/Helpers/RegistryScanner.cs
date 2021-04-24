namespace Senselessly.Foolish.Bethesda.Wpf.Services.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using Interface;
    using Microsoft.Win32;
    using Models;

    public class RegistryScanner : IRegistryScanner
    {
        public IEnumerable<RegistryResult> Results { get; private set; }

        public bool Read(string path, params string[] keys)
        {
            var results = new List<RegistryResult>();
            keys?.ToList()
                .ForEach(key =>
                {
                    var value = Registry.GetValue(keyName: path, valueName: key, defaultValue: null);
                    if (value != null)
                    {
                        results.Add(new RegistryResult
                        {
                            Key = $"{path}\\{key}", Value = value,
                        });
                    }
                });
            if (results.Any())
            {
                Results = results;
            }

            return Results != null;
        }
    }
}