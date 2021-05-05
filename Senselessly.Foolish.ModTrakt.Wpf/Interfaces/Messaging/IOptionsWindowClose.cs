namespace Senselessly.Foolish.ModTrakt.Wpf.Interface.Messaging
{
    using System;

    public interface IOptionsWindowClose
    {
        bool Close { get; }

        bool Handled { get; set; }

        bool Shutdown { get; }

        Type Source { get; }
    }
}