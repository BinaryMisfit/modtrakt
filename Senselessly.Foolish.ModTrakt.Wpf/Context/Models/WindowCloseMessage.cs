namespace Senselessly.Foolish.ModTrakt.Wpf.Context.Models
{
    using Interface;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    public class WindowCloseMessage : ValueChangedMessage<IWindowCloseOptions>
    {
        public WindowCloseMessage(IWindowCloseOptions value) : base(value) { }
    }
}