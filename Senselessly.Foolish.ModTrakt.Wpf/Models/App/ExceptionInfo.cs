namespace Senselessly.Foolish.ModTrakt.Wpf.Models.App
{
    using System;
    using Interfaces.App;

    internal sealed class ExceptionInfo : IExceptionInfo
    {
        public ExceptionInfo(string sourceName, Type source, Exception e)
        {
            Exception = e;
            SourceName = sourceName;
            Source = source;
            Timestamp = DateTime.Now;
        }

        public Exception Exception { get; }

        public bool Handled { get; set; }

        public string SourceName { get; }

        public Type Source { get; }

        public DateTime Timestamp { get; }
    }
}