namespace Senselessly.Foolish.ModTrakt.Wpf.Context.Models
{
    using AppData.Interface;

    public class RegistryResult
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