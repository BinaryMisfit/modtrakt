namespace Senselessly.Foolish.ModTrakt.Wpf.Services.Messaging
{
    using Interfaces.App;
    using Interfaces.Services;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Models.Messaging.Messages.Exceptions;

    internal sealed class ExceptionService : IExceptionService
    {
        public void Send(IExceptionInfo e)
        {
            WeakReferenceMessenger.Default.Send(new ExceptionRaisedMessage(e));
        }
    }
}