namespace Senselessly.Foolish.ModTrakt.Wpf.Models.App
{
    using System;
    using Interfaces.App;

    internal sealed class ExceptionInfo : IExceptionInfo
    {
        public ExceptionInfo(Exception e) => Exception = e;

        public Exception Exception { get; }

        public bool Handled { get; set; }
    }
}