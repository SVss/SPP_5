using System;
using System.Runtime.Serialization;

namespace CalcClientLib
{
    [Serializable]
    public class InvalidBracketsException : Exception
    {
        public InvalidBracketsException()
        {
        }

        public InvalidBracketsException(string message) : base(message)
        {
        }

        public InvalidBracketsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidBracketsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class InvalidExprException : Exception
    {
        public InvalidExprException()
        {
        }

        public InvalidExprException(string message) : base(message)
        {
        }

        public InvalidExprException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidExprException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
