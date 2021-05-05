namespace Senselessly.Foolish.ModTrakt.Wpf.Models.App
{
    using Interfaces.App;

    internal sealed class RegistryResult
    {
        public RegistryResult(string id, IGameRegistry registry)
        {
            Id = id;
            Registry = registry;
        }

        public string Id { get; }

        public IGameRegistry Registry { get; }

        public object Value { get; set; }
    }
}