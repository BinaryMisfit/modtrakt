namespace Senselessly.Foolish.ModTrakt.Wpf.Context.Messages
{
    using Interface;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    public class WindowCloseMessage : ValueChangedMessage<IOptionsWindowClose>
    {
        public WindowCloseMessage(IOptionsWindowClose value) : base(value) { }
    }
}