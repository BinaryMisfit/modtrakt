namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Messaging
{
    using System;

    internal interface IOptionsShowWindow
    {
        Type Caller { get; }

        bool CloseCaller { get; }

        bool Handled { get; set; }

        Type Window { get; }
    }
}