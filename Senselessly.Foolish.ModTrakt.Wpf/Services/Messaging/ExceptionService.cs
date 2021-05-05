namespace Senselessly.Foolish.ModTrakt.Wpf.Services.Messaging
{
    using Interface.Models;
    using Interface.Services;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Models.Messages;

    public class ExceptionService : IExceptionService
    {
        public void Send(IExceptionInfo e)
        {
            WeakReferenceMessenger.Default.Send(new ExceptionRaisedMessage(e));
        }
    }
}