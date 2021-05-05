namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messages
{
    using Interface.Messaging;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    public class ShowWindowMessage : ValueChangedMessage<IOptionsShowWindow>
    {
        public ShowWindowMessage(IOptionsShowWindow value) : base(value) { }
    }
}