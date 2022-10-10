using ECatalogueApi.DTO;
using ProjectOnlineCatalogue.Models;
using ProjectOnlineCatalogueData.Models;

namespace ECatalogueApi.Extensions
{
    public static class DtoToEntityExtension
    {
        public static Subject ToEntity(this SubjectToCreate subjectToCreate) =>
        new Subject
        {
            Name = subjectToCreate.Name,
            TeacherId = subjectToCreate.TeacherId
        };
        public static Student ToEntity(this StudentToCreate studentToCreate) =>
        new Student
        {
            FirstName = studentToCreate.FirstName,
            LastName = studentToCreate.LastName,
            Age = studentToCreate.Age
        };

        public static Student ToEntity(this StudentToUpdate studentToUpdate) =>
        new Student
        {
            FirstName = studentToUpdate.FirstName,
            LastName = studentToUpdate.LastName,
            Age = studentToUpdate.Age
        };

        public static Address ToEntity(this AddressToUpdate addressToUpdate) =>
        new Address
        {
            City = addressToUpdate.City,
            Street = addressToUpdate.Street,
            Number = addressToUpdate.Number
        };
        public static Teacher ToEntity(this TeacherToCreate teacherToCreate) =>
        new Teacher
        {
            Name = teacherToCreate.Name,
            Rank = teacherToCreate.Rank,
        };
    }
}


