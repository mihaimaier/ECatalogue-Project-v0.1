using Microsoft.AspNetCore.Mvc;
using ProjectOnlineCatalogue.Models;
using ProjectOnlineCatalogueData.Models;

namespace ECatalogueApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    #region Seeding Database
    public class SeedDbController : Controller
    {
        private readonly OnlineCatalogueDbContext context;

        public SeedDbController(OnlineCatalogueDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// SEEDs the Db.
        /// </summary>
        [HttpPost]
        public void Seed()
        {
            var student1 = new Student { FirstName = "Mihai", LastName = "Popescu", Age = 26 };
            var student2 = new Student { FirstName = "Andrei", LastName = "Vlad", Age = 21 };
            var student3 = new Student { FirstName = "Constantin", LastName = "Maier", Age = 28 };
            var student4 = new Student { FirstName = "Ana", LastName = "Miches", Age = 23 };
            var student5 = new Student { FirstName = "Alexandra", LastName = "Dan", Age = 29 };

            var teacher1 = new Teacher { Name = "Ioan Popescu"};
            var teacher2 = new Teacher { Name = "Alexandra Ielciu" };
            var teacher3 = new Teacher { Name = "Sabine Moldovan" };
            var teacher4 = new Teacher { Name = "Tibi Dragan" };
            var teacher5 = new Teacher { Name = "Madalina Gradinaru" };

            var address1 = new Address { City = "Iasi", Number = 3, Street = "STR. BARIŢIU GEORGE" };
            var address2 = new Address { City = "Vrancea", Number = 2, Street = "Strada Smardan" };
            var address3 = new Address { City = "Cluj", Number = 44, Street = "Matei Corvin" };
            var address4 = new Address { City = "Bucuresti", Number = 7, Street = "ŞOS. VITAN BÂRZEŞTI" };
            var address5 = new Address { City = "Galati", Number = 2, Street = "STR. STRUNGARILOR" };


            var address6 = new Address { City = "Timisoara", Number = 32, Street = "Mr.Avram Zenovie" };
            var address7 = new Address { City = "Cluj", Number = 17, Street = "STR. LISZT FRANZ" };
            var address8 = new Address { City = "Bucuresti", Number = 105, Street = "STR. VĂCĂRESCU BARBU" };
            var address9 = new Address { City = "Brasov", Number = 202, Street = " Piata Teatrului 6" };
            var address10 = new Address { City = "Valcea", Number = 151, Street = "Drumul Campului 15 A" };

            var mark1 = new Mark { Value = 6, CreationDate = DateTime.Now };
            var mark2 = new Mark { Value = 10, CreationDate = DateTime.Now };
            var mark3 = new Mark { Value = 8, CreationDate = DateTime.Now };
            var mark4 = new Mark { Value = 9, CreationDate = DateTime.Now };
            var mark5 = new Mark { Value = 10, CreationDate = DateTime.Now };

            var subject1 = new Subject { Name = "Mathematics" };
            var subject2 = new Subject { Name = "Physics" };
            var subject3 = new Subject { Name = "Art" };
            var subject4 = new Subject { Name = "Science" };
            var subject5 = new Subject { Name = "Chemistry" };

            var rank1 = Rank.Instructor;
            var rank2 = Rank.AssociateProfessor;
            var rank3 = Rank.AssistantProfessor;
            var rank4 = Rank.Professor;
            var rank5 = Rank.AssociateProfessor;

            student1.Address = address1;
            student2.Address = address2;
            student3.Address = address3;
            student4.Address = address4;
            student5.Address = address5;

            teacher1.Address = address6;
            teacher2.Address = address7;
            teacher3.Address = address8;
            teacher4.Address = address9;
            student5.Address = address9;

            teacher1.Subject = subject1;
            teacher2.Subject = subject2;
            teacher3.Subject = subject3;
            teacher4.Subject = subject4;
            teacher5.Subject = subject5;

            teacher1.Rank = rank1;
            teacher2.Rank = rank2;
            teacher3.Rank = rank3;
            teacher4.Rank = rank4;
            teacher5.Rank = rank5;

            student1.Marks = new List<Mark> { mark1, mark2, mark3 };
            student2.Marks = new List<Mark> { mark3, mark1, mark5 };
            student3.Marks = new List<Mark> { mark2, mark5, mark3 };
            student4.Marks = new List<Mark> { mark1, mark2, mark3 };
            student5.Marks = new List<Mark> { mark4, mark2, mark3 };

            //Adding Students

            this.context.Add(student1);
            this.context.Add(student2);
            this.context.Add(student3);
            this.context.Add(student4);
            this.context.Add(student5);

            //Adding Teachers

            this.context.Add(teacher1);
            this.context.Add(teacher2);
            this.context.Add(teacher3);
            this.context.Add(teacher4);
            this.context.Add(teacher5);

            this.context.SaveChanges();
        }
    #endregion

    #region Create Database
        [HttpPost("create Db")]
        public void CreateDb()
        {
            this.context.Database.EnsureCreated();
        }
    #endregion
        
    #region Delete Database
        [HttpPost("delete Db")]
        public void DeleteDb()
        {
            this.context.Database.EnsureDeleted();
        }
    #endregion
    }
}
