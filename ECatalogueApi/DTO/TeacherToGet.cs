using ProjectOnlineCatalogue.Models;
using ProjectOnlineCatalogueData.Models;

namespace ECatalogueApi.DTO
{
    public class TeacherToGet
    {
        public string Name { get; set; }

        public Rank Rank { get; set; }

        public string? City { get; set; }

        public string? Street { get; set; }

        public int? Number { get; set; }

        public Subject Subject { get; set; }
    }
}
