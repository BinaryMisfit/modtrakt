namespace Senselessly.Foolish.ModTrakt.Wpf.Context.Messages
{
    using Interface;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    public class ShowWindowMessage : ValueChangedMessage<IOptionsShowWindow>
    {
        public ShowWindowMessage(IOptionsShowWindow value) : base(value) { }
    }
}