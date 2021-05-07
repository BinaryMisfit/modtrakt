namespace Senselessly.Foolish.ModTrakt.Wpf.Interfaces.Messaging.UI
{
    using System;

    internal interface IOptionsCompletionMessage
    {
        Guid Identifier { get; }

        bool IsSuccessful { get; }
    }
}