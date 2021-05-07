namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Messaging.Options.UI
{
    using Interfaces.Messaging.UI;

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