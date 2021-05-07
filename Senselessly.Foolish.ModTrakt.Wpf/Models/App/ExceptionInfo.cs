namespace Senselessly.Foolish.ModTrakt.Wpf.Models.App
{
    using System;
    using Interfaces.App;
    using Properties;

    internal sealed class ExceptionInfo : IExceptionInfo
    {
        public ExceptionInfo(Exception e, Action close = null)
        {
            Close = close;
            Exception = e;
            Timestamp = DateTime.Now;
        }

        public Action Close { get; }

        public Exception Exception { get; }

        public bool Handled { get; set; }

        public string SourceName
        {
            get => Exception.Source ?? Resources.Application_Name;
        }

        public Type Source
        {
            get => Exception.TargetSite?.GetType() ?? Exception.GetType();
        }

        public DateTime Timestamp { get; }
    }
}