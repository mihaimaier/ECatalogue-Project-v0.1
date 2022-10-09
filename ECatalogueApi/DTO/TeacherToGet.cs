using ProjectOnlineCatalogue.Models;
using ProjectOnlineCatalogueData.Models;

namespace ECatalogueApi.DTO
{
    public class TeacherToGet
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Rank Rank { get; set; }

        public Address Address { get; set; }

        public Subject Subject { get; set; }
    }
}
