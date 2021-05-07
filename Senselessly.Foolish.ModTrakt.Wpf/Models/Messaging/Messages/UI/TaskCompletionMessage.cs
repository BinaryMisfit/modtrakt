namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messaging.Messages.UI
{
    using Interfaces.Messaging.UI;
    using Microsoft.Toolkit.Mvvm.Messaging.Messages;

    internal sealed class TaskCompletionMessage : ValueChangedMessage<IOptionsCompletionMessage>
    {
        public TaskCompletionMessage(IOptionsCompletionMessage value) : base(value) { }
    }
}