namespace Senselessly.Foolish.ModTrakt.Wpf.Interface.Models
{
    using System;

    public interface IExceptionInfo
    {
        Exception Exception { get; }

        bool Handled { get; set; }
    }
}