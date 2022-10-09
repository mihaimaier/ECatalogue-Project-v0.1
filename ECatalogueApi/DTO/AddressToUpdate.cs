using System.ComponentModel.DataAnnotations;

namespace ECatalogueApi.DTO
{
    public class AddressToUpdate
    {
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Number is required")]
        public int Number { get; set; }
    }
}
