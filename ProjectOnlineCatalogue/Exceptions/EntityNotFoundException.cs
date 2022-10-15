using System.Runtime.Serialization;

namespace ProjectOnlineCatalogueData.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public readonly string message = "";

        public EntityNotFoundException(int id)
        {
            this.message = string.Format("Entity cannot be found in the system");
        }
    }
}