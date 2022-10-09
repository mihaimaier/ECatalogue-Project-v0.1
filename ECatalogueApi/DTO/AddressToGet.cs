using ProjectOnlineCatalogue.Models;
using System.ComponentModel.DataAnnotations;

namespace ECatalogueApi.DTO
{
    public class AddressToGet
    {
        public string City { get; set; }

        public string Street { get; set; }

        public int Number { get; set; }
        
        public Address Address { get; set; }
    }
}
