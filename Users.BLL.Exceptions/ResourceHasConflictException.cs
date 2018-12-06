using System;
using System.Runtime.Serialization;

namespace Users.BLL.Exceptions
{
    [Serializable]
    public class ResourceHasConflictException : Exception
    {
        public ResourceHasConflictException()
        {
        }

        public ResourceHasConflictException(string message)
            : base(message)
        {
        }

        public ResourceHasConflictException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ResourceHasConflictException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
