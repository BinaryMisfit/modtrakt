namespace Senselessly.Foolish.ModTrakt.Wpf.Context.Options
{
    using System;
    using Interface;

    public class WindowCloseOptions : IOptionsWindowClose
    {
        public WindowCloseOptions(Type source, bool close, bool shutdown)
        {
            Source = source;
            Close = close;
            Shutdown = shutdown;
        }

        public bool Close { get; }

        public bool Handled { get; set; }

        public bool Shutdown { get; }

        public Type Source { get; }
    }
}