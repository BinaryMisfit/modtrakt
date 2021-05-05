namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Messaging
{
    using System;

    internal interface IOptionsConfirmExit
    {
        Action CancelAction { get; }

        bool Close { get; }

        bool Handled { get; set; }

        string Host { get; }

        bool Shutdown { get; }
    }
}