using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOnlineCatalogueData.Exceptions
{
    public class SubjectDoesNotExistException : Exception
    {
        public readonly string message = "";

        public SubjectDoesNotExistException(int id)
        {
            this.message = string.Format("Subject with ID {0} does not exists", id);
        }
    }
}
