namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messaging
{
    using Interfaces.App;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    internal sealed class ExceptionRaisedMessage : ValueChangedMessage<IExceptionInfo>
    {
        public ExceptionRaisedMessage(IExceptionInfo value) : base(value) { }
    }
}