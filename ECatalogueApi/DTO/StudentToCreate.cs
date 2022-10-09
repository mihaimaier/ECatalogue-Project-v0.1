using System.ComponentModel.DataAnnotations;

namespace ECatalogueApi.DTO
{
    public class StudentToCreate
    {
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Age is requied")]
        [Range(1, int.MaxValue, ErrorMessage = "Age cannot be less than 1")]
        public int Age { get; set; }
    }
}
