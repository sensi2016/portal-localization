using System;
using System.Runtime.Serialization;

namespace Portal.Application.Service
{
    [Serializable]
    internal class SyncDataMismatchException : Exception
    {
        public SyncDataMismatchException()
        {
        }

        public SyncDataMismatchException(string message) : base(message)
        {
        }

        public SyncDataMismatchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SyncDataMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}