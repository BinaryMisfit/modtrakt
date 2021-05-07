namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messaging.Options.UI
{
    using System;
    using Interfaces.Messaging.UI;

    internal sealed class ShowWindowOptions : IOptionsShowWindow
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