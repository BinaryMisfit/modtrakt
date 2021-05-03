namespace Senselessly.Foolish.ModTrakt.Wpf.Context.Interface
{
    public interface IOptionsConfirmExit
    {
        bool Close { get; }

        bool Handled { get; set; }

        string Host { get; }

        bool Shutdown { get; }
    }
}