using System.ComponentModel.DataAnnotations;

namespace ECatalogueApi.DTO
{
    public class StudentToCreate
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Age cannot be less than 1")]
        public int Age { get; set; }
    }
}
