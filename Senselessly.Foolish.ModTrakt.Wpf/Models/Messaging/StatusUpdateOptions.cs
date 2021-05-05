namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messaging
{
    using Interfaces.Messaging;

    public class StatusUpdateOptions : IOptionsStatusMessage
    {
        public StatusUpdateOptions(string message)
        {
            Message = message;
            Clear = string.IsNullOrEmpty(message);
        }

        public string Message { get; }

        public bool Clear { get; }
    }
}