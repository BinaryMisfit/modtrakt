namespace Senselessly.Foolish.ModTrakt.Wpf.Context.Interface
{
    using System;

    public interface IOptionsShowWindow
    {
        Type Caller { get; }

        bool CloseCaller { get; }

        bool Handled { get; set; }

        Type Window { get; }
    }
}