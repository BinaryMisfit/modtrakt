namespace Senselessly.Foolish.ModTrakt.Wpf.Models.Exceptions.Settings
{
    using System;
    using System.Runtime.Serialization;
    using Properties;

    [Serializable]
    public sealed class ConfigurationCorruptException : Exception
    {
        public ConfigurationCorruptException() : base(Resources.Exception_Config_Corrupt) { }

        public ConfigurationCorruptException(string message) : base(message) { }

        public ConfigurationCorruptException(string message, Exception inner) : base(message: message,
            innerException: inner) { }

        private ConfigurationCorruptException(SerializationInfo info, StreamingContext context) : base(info: info,
            context: context) { }
    }
}