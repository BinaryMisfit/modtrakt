namespace Senselessly.Foolish.ModTrakt.Wpf.Context.Options
{
    using System;
    using Interface;

    public class ShowWindowOptions : IOptionsShowWindow
    {
        public ShowWindowOptions(Type caller, Type window, bool closeCaller)
        {
            Caller = caller;
            Window = window;
            CloseCaller = closeCaller;
        }

        public Type Caller { get; }

        public bool CloseCaller { get; }

        public bool Handled { get; set; }

        public Type Window { get; }
    }
}