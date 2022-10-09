using ProjectOnlineCatalogue.Models;
using System.ComponentModel.DataAnnotations;

namespace ECatalogueApi.DTO
{
    public class StudentToUpdate
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(1, 100, ErrorMessage = "You have incorrectly inputed your age. Please make sure to input a number between 1 and 100.")]
        public int Age { get; set; }
    }
}
