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
            dto.Address = student.Address.ToDto();

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
            if (subject.TeacherId == null)
            {
                return new SubjectToGet { Name = subject.Name, TeacherId = null };
            }
            return new SubjectToGet { Name = subject.Name, TeacherId = (int)subject.TeacherId };
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
            if (teacher.Address == null)
            {
                return new TeacherToGet
                {
                    Name = teacher.Name,
                    Rank = teacher.Rank,
                    Subject = teacher.Subject,
                    City = null,
                    Street = null,
                    Number = null
                };
            }

            return new TeacherToGet
            {
                Name = teacher.Name,
                Rank = teacher.Rank,
                Subject = teacher.Subject,
                City = teacher.Address.City,
                Street = teacher.Address.Street,
                Number = teacher.Address.Number
            };
        }
        public static MarksByTeacherToGet ToDtoMarksByTeacher(this Mark mark)
        {
            return new MarksByTeacherToGet
            {
                Value = mark.Value,
                StudentId = mark.StudentId,
                CreationDate = mark.CreationDate.ToString(),
            };
        }
    }
}
