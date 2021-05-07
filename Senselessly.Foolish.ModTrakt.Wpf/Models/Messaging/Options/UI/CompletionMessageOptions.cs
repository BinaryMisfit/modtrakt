namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messaging.Options.UI
{
    using System;
    using Interfaces.Messaging.UI;

    public class CompletionMessageOptions : IOptionsCompletionMessage
    {
        public CompletionMessageOptions(Guid identifier, bool isSuccessful)
        {
            Identifier = identifier;
            IsSuccessful = isSuccessful;
        }

        public Guid Identifier { get; }

        public bool IsSuccessful { get; }
    }
}