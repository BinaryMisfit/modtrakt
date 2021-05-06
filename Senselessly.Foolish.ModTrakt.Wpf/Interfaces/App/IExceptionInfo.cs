namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.App
{
    using System;

    internal interface IExceptionInfo
    {
        Exception Exception { get; }

        bool Handled { get; set; }

        string SourceName { get; }

        Type Source { get; }

        DateTime Timestamp { get; }
    }
}