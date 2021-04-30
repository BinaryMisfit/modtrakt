namespace Senselessly.Foolish.Bethesda.Wpf.AppData.Interface
{
    public interface IGameRegistry
    {
        string Root { get; set; }
        
        string Path { get; set; }
        
        string Key { get; set; }
        
        string Usage { get; set; }
    }
}