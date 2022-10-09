using System.ComponentModel.DataAnnotations;

namespace ECatalogueApi.DTO
{
    public class SubjectToCreate
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
