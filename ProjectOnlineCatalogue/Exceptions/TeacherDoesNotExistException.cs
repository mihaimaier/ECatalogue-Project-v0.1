using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOnlineCatalogueData.Exceptions
{
    public class TeacherDoesNotExistException : Exception
    {
        public readonly string message = "";


        public TeacherDoesNotExistException(int id)
        {
            this.message = string.Format("Teacher with ID {0} does not exists", id);
        }
    }
}
