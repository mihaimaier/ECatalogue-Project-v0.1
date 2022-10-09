using ProjectOnlineCatalogueData.Models;
using System.ComponentModel.DataAnnotations;

namespace ECatalogueApi.DTO
{
    public class TeacherToCreate
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Rank is required")]
        public Rank Rank { get; set; }
    }
}
