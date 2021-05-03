namespace Senselessly.Foolish.ModTrakt.Wpf.Context.Options
{
    using Interface;

    public class ConfirmExit : IOptionsConfirmExit
    {
        public ConfirmExit(string host, bool close, bool shutdown)
        {
            Close = close;
            Host = host;
            Shutdown = shutdown;
        }

        public bool Close { get; }

        public bool Handled { get; set; }

        public string Host { get; }

        public bool Shutdown { get; }
    }
}