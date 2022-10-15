using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOnlineCatalogueData.Exceptions
{
    public class StudentDoesNotExistsException : Exception
    {
        public readonly string message = "";

        public StudentDoesNotExistsException(int id)
        {
            this.message = string.Format("Student with ID {0} does not exists", id);
        }
    }
}
