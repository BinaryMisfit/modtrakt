namespace Senselessly.Foolish.Bethesda.Wpf.Context.Models
{
    using AppData.Interface;

    public class RegistryResult
    {
        public RegistryResult(string id, IGameRegistry registry)
        {
            Id = id;
            Registry = registry;
            Usage = registry.Usage;
        }

        public string Id { get; }

        public IGameRegistry Registry { get; }

        public string Usage { get; }

        public object Value { get; set; }
    }
}