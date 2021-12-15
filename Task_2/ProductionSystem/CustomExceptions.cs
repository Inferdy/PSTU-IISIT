using System.Runtime.Serialization;

namespace ProductionSystem
{
    public class WrongFormatException : Exception
    {
        public WrongFormatException() { }

        public WrongFormatException(string message) : base(message) { }

        public WrongFormatException(string message, Exception inner) : base(message, inner) { }

        protected WrongFormatException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
