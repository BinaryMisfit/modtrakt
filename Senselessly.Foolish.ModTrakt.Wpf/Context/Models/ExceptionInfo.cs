namespace Senselessly.Foolish.ModTrakt.Wpf.Context.Models
{
    using System;
    using Interface;

    public class ExceptionInfo : IExceptionInfo
    {
        public ExceptionInfo(Exception e) => Exception = e;

        public Exception Exception { get; }

        public bool Handled { get; set; }
    }
}