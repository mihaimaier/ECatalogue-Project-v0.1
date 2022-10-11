using System.Runtime.Serialization;

namespace ProjectOnlineCatalogueData.Exceptions
{
    [Serializable]
    internal class EntityNotFoundException : Exception
    {
        private int teacherId;

        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string? message) : base(message)
        {
        }

        public EntityNotFoundException(int teacherId)
        {
            this.teacherId = teacherId;
        }

        public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}