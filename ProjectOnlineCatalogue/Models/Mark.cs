namespace ProjectOnlineCatalogue.Models
{
    public class Mark
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public int SubjectId { get; set; }

        public int StudentId { get; set; }

        public int TeacherId { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
