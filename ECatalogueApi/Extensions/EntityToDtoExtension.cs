using ECatalogueApi.DTO;
using ProjectOnlineCatalogue.Models;
using ProjectOnlineCatalogueData.Models;

namespace ECatalogueApi.Extensions
{
    public static class EntityToDtoExtension
    {
        public static StudentToGet ToDto(this Student student)
        {
            if (student == null)
            {
                return null;
            }
            StudentToGet dto = new StudentToGet();
            dto.Id = student.Id;
            dto.FirstName = student.FirstName;
            dto.LastName = student.LastName;
            dto.Age = student.Age;
            dto.Address = student.Address;

            return dto;
        }
        public static AddressToGet ToDto(this Address address)
        {
            if (address == null)
            {
                return null;
            }
            return new AddressToGet
            {
                City = address.City,
                Street = address.Street,
                Number = address.Number
            };
        }
        public static SubjectToGet ToDto(this Subject subject)
        {
            if(subject == null)
                return null;

            SubjectToGet dto = new SubjectToGet();
            {
                dto.Id = subject.Id;
                dto.Name = subject.Name;
                dto.TeacherId = subject.TeacherId;

                return dto;
            };
        }
        public static MarkToGet ToDto(this Mark mark)
        {
            if (mark == null)
                return null;

            return new MarkToGet
            {
                Id = mark.Id,
                CreationDate = mark.CreationDate,
                StudentId = mark.StudentId,
                SubjectId = mark.SubjectId,
                Value = mark.Value
            };
        }
        public static TeacherToGet ToDto(this Teacher teacher)
        {
            if(teacher == null)
            {
                return null;
            }
            TeacherToGet dto = new TeacherToGet();
            dto.Id = teacher.Id;
            dto.Name = teacher.Name;
            dto.Rank = teacher.Rank;
            dto.Address = teacher.Address;
            dto.Subject = teacher.Subject;

            return dto;
        }
    }
}
