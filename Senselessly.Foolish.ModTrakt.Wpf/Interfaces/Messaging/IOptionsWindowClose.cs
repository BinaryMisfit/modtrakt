namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Messaging
{
    using System;

    internal interface IOptionsWindowClose
    {
        bool Close { get; }

        bool Handled { get; set; }

        bool Shutdown { get; }

        Type Source { get; }
    }
}