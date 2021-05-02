namespace Senselessly.Foolish.ModTrakt.Wpf.Context.Models
{
    using System;
    using Interface;

    public class ExceptionInfo : IExceptionInfo
    {
        public ExceptionInfo(Exception e)
        {
            this.Exception = e;
        }

        public Exception Exception { get; }
    }
}