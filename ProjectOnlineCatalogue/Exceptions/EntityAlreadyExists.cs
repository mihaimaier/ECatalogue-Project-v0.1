using System.Runtime.Serialization;

namespace ProjectOnlineCatalogueData.Exceptions
{
    [Serializable]
    internal class EntityAlreadyExists : Exception
    {
        public EntityAlreadyExists()
        {
        }

        public EntityAlreadyExists(string? message) : base(message)
        {
        }

        public EntityAlreadyExists(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EntityAlreadyExists(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}